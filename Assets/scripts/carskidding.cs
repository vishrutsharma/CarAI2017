using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carskidding : MonoBehaviour {
	
	WheelCollider wheelcol;
	public float sidefrictionvalue;
	public GameObject skidsound;
	// Use this for initialization
	void Start () {
		wheelcol = GetComponent<WheelCollider> ();
	}
	
	// Update is called once per frame
	void Update () {

		WheelHit hit;
		if (wheelcol.GetGroundHit (out hit)) {
		
			sidefrictionvalue = Mathf.Abs (hit.sidewaysSlip);
			if (sidefrictionvalue > 0.4) 
			{
			
				GameObject gb=Instantiate (skidsound, hit.point, Quaternion.identity);
				Destroy (gb, 1);
			
			}
		
		}





	}
}
