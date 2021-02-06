using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class braking : MonoBehaviour {
    public WheelCollider backl;
    public WheelCollider backr;

	float myforwardfriction;
	float mysidewayfriction;
	public float slipforwardfriction;
	public float slipsidewayfriction;
    bool braked = false;

	Rigidbody rg;
	// Use this for initialization
	void Start () {

		rg = GetComponent<Rigidbody> ();
		setvalues ();
        }
	
	// Update is called once per frame

	void setvalues()
	{
		myforwardfriction = backl.forwardFriction.stiffness;
		mysidewayfriction = backl.sidewaysFriction.stiffness;
		slipforwardfriction = 0.6f;
		slipsidewayfriction = 0.6f;



	}

	 public void setcurrentvalues(float currentforwardfriction,float currentsidewayfriction)
	{
		//backr forward friction
		WheelFrictionCurve brf = backr.forwardFriction;
		brf.stiffness = currentforwardfriction;
		backr.forwardFriction = brf;

		//backl forward friction
		WheelFrictionCurve blf=backl.forwardFriction;
		blf.stiffness = currentforwardfriction;
		backl.forwardFriction = blf;

		//backr sideway friction
		WheelFrictionCurve brs=backr.sidewaysFriction;
		brs.stiffness = currentsidewayfriction;
		backr.sidewaysFriction = brs;

		//backl sideway friction
		WheelFrictionCurve bls=backl.sidewaysFriction;
		bls.stiffness = currentsidewayfriction;
		backl.sidewaysFriction = bls;


	}

	void Update () {
        brakingsystem();
	}

    public void brakingsystem()
    {
		if (Input.GetButtonDown("Jump")) {
			braked = true;

            
		}


        else
        {

            braked = false;
        }
		if(braked)
        {
			backl.brakeTorque =34500;
			backr.brakeTorque =34500;
			backl.motorTorque = 0;
			backr.motorTorque = 0;
			if (rg.velocity.magnitude > 1) {
				setcurrentvalues (slipforwardfriction, slipsidewayfriction);


			}

			else {

				setcurrentvalues (1, 1);
			}


           

		




            }
        else
        {

			setcurrentvalues (myforwardfriction, mysidewayfriction);
            backl.brakeTorque = 0;
            backr.brakeTorque = 0;
            backl.motorTorque = 250;
            backr.motorTorque = 250;
		






        }

        if(Input.GetAxis("Vertical")!=0)
        {

            backl.brakeTorque = 0;
            backr.brakeTorque = 0;


        }
        else
        {

            backl.brakeTorque = 1400;
            backr.brakeTorque = 1400;
            backr.motorTorque = 0;
            backl.motorTorque = 0;


        }


    }


        
    }

