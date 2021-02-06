using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelmeshmovement : MonoBehaviour {

	public WheelCollider wheels;

	Vector3 vectorposition = new Vector3 ();
	Quaternion rotation = new Quaternion ();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		wheels.GetWorldPose (out vectorposition, out rotation);
		transform.position = vectorposition;
		transform.rotation = rotation;











	}
}
