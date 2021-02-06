using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carsteeringai : MonoBehaviour {
	public WheelCollider fl;
	public WheelCollider fr;
	public WheelCollider br;
	public WheelCollider bl;
	public Transform path;


	float topspeed=40f;
	float maxbraketorque=5000f;
	int currentnode=0;
	private List<Transform> nodes = new List<Transform> ();
	float steering;
	float maxsteerangle=40f;
	public float currentspeed;
	public bool isbraking=false;
	Rigidbody rg;
	public float maxtorque=700f;

	[Header("Sensors")]
	 float frontstraightsensorstart = 1.8f;
	 float sensorlength=10.2f;
	float frontsensorsidedistance=0.73f;
	private bool avoid=false;
	float angletomaintain=27.0f;
	float targetsteer=0;
	float speedrot=14f;
	//float angletomaintain2=120.0f;
	// Use this for initialization
	void Start () {

		Transform[] pathnodes = path.GetComponentsInChildren<Transform> ();
		nodes = new List<Transform> ();

		for (int i = 0; i < pathnodes.Length; i++) {
		
			if (pathnodes [i] != path.transform) {
			
			
				nodes.Add (pathnodes [i]);
			
			
			
			}
		
		
		
		}


	 rg = transform.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		applysteer ();
		drive ();
		checkwaypointdistance ();
		braking ();
		sensors ();
		steerattarget ();

	}




		

	void sensors()
	{   
		RaycastHit hit;

		float avoidmultiplier = 0.0f;


		//front middle sensor
		Vector3 position= transform.position + transform.forward * frontstraightsensorstart;






		//front right sensor
		position += transform.right * frontsensorsidedistance;
	
		if(Physics.Raycast(position,transform.forward,out hit,sensorlength))
		{
			if (!hit.collider.CompareTag ("floor")) 
			{
				Debug.DrawLine (position, hit.point,Color.green);
				Debug.Log (hit.point);
				avoid = true;
				avoidmultiplier -= 1.5f;
			}


		}


		//front right angle sensor
	
		else if(Physics.Raycast(position,Quaternion.AngleAxis(angletomaintain,transform.up)*transform.forward,out hit,sensorlength))
		{
			if (!hit.collider.CompareTag ("floor")) 
			{
				Debug.DrawLine (position, hit.point,Color.green);
				Debug.Log (hit.point);
				avoid = true;
				avoidmultiplier -= 0.5f;
			
			}

		}




		//front left sensor
		position= transform.position + transform.forward * frontstraightsensorstart;
		position -= transform.right * frontsensorsidedistance;
		if(Physics.Raycast(position,transform.forward,out hit,sensorlength))
		{

			if (!hit.collider.CompareTag ("floor")) {
			

				Debug.DrawLine (position, hit.point,Color.green);
				Debug.Log (hit.point);
				avoid = true;
				avoidmultiplier += 1.5f;
			}

		}



		//front left angle sensor

		else if(Physics.Raycast(position,Quaternion.AngleAxis(-angletomaintain,transform.up)*transform.forward,out hit,sensorlength))
		{
			if (!hit.collider.CompareTag ("floor")) {
				Debug.DrawLine (position, hit.point,Color.green);
				Debug.Log (hit.point);
				avoid = true;
				avoidmultiplier += 0.5f;
			
			
			}

		}


		if(Physics.Raycast(position,transform.forward,out hit,sensorlength))
		{
			Debug.DrawLine (position, hit.point,Color.green);
			Debug.Log (hit.point);
			if (!hit.collider.CompareTag ("floor")) {
			
				avoid = true;
				if (hit.normal.x < 0) {
				
				
					avoidmultiplier -= 1.5f;
				}
				else {
					avoidmultiplier += 1.5f;
				
				}

			
			}


		}
		if (avoid)
		{
			targetsteer = -maxsteerangle*avoidmultiplier;


		
		}
		avoid = false;

	}

	void applysteer()
	{
		
			
		if (!avoid) {
			Vector3 relativevector = transform.InverseTransformPoint (nodes [currentnode].position);
			steering = (relativevector.x / relativevector.magnitude) * maxsteerangle;
			targetsteer= -steering;

		}
	}

	void drive()
	{

		currentspeed=Mathf.RoundToInt(rg.velocity.magnitude*3.6f);
		if (currentspeed <= topspeed) {
			fl.motorTorque = maxtorque;
			fr.motorTorque = maxtorque;
		}

		else {
			fl.motorTorque = 0;
			fr.motorTorque = 0;
		
		}


	}

	void checkwaypointdistance()
	{

		float dist = Vector3.Distance (transform.position, nodes [currentnode].position);
		if (dist < 10.5f) {
		
			if (currentnode == nodes.Count - 1) {
				currentnode = 0;
			} 
			else {
				currentnode++;
			}
		
		}


	}

	void braking()
	{

		if (isbraking) {
		
			br.brakeTorque = maxbraketorque;
			bl.brakeTorque = maxbraketorque;
			transform.transform.GetComponent<Rigidbody>().drag = 1;
		
		
		
		
		
		}
		else {

			transform.transform.GetComponent<Rigidbody>().drag = 0;
			br.brakeTorque = 0;
			bl.brakeTorque = 0;
		
		}




	}

	void steerattarget()
	{

		fl.steerAngle = Mathf.Lerp (fl.steerAngle, targetsteer, Time.deltaTime * speedrot);
		fr.steerAngle = Mathf.Lerp (fr.steerAngle, targetsteer, Time.deltaTime * speedrot);




	}
}
