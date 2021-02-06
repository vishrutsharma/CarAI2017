using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour
{

    private WheelCollider[] wheels;
	public ParticleSystem prt1;
	public ParticleSystem prt2;
	AudioSource aud;
    public float aang = 120;
    public float maxTorque = 800;
    public GameObject wheelShape;

    
	public float highestspeed=80;
	public float currentsteerangle;
	public float highspeedsteerangle=1;
	public float lowspeedsteerangle = 20;
	 float speedfactor;
	public float topspeed=100;



  public Rigidbody rg;



    public float currentspeed;

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
    {
		

	
		aud = GetComponent<AudioSource> ();
      rg = GetComponent<Rigidbody>();
		rg.centerOfMass = new Vector3 (0, 0.2f, 0);
        wheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < wheels.Length; ++i)
        {
            var wheel = wheels[i];

            // create wheel shapes only when needed
            if (wheelShape != null)
            {
                var ws = GameObject.Instantiate(wheelShape);
                ws.transform.parent = wheel.transform;
            }
        }
    }

 
	public void Update()
	{
		calculatespeed ();

		enginesound ();


	}

	public void calculatespeed()
	{
		currentspeed = Mathf.RoundToInt (rg.velocity.magnitude * 3.6f);

	}


    // this is a really simple approach to updating wheels
    // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
    // this helps us to figure our which wheels are front ones and which are rear
    
	void enginesound()
	{


		aud.pitch = currentspeed / topspeed+1;
		
		}







	public void FixedUpdate()
    {


		speedfactor = rg.velocity.magnitude / highestspeed;
		currentsteerangle = Mathf.Lerp (lowspeedsteerangle, highspeedsteerangle, speedfactor);


	
			float angle = Input.GetAxis ("Horizontal") * currentsteerangle;
		
            
     
			float torque = maxTorque * Input.GetAxis ("Vertical");
		if (currentspeed < 1) {
		
			prt1.Play ();
			prt2.Play ();
		
		}
		
		else if (currentspeed <= topspeed) {
			
			foreach (WheelCollider wheel in wheels) {
				prt1.Stop ();
				prt2.Stop ();
				// a simple car where front wheels steer while rear ones drive
				if (wheel.transform.localPosition.z > 0)
					wheel.steerAngle = angle;

				if (wheel.transform.localPosition.z < 0)
					wheel.motorTorque = torque;
				


				// update visual wheels if any
				if (wheelShape) {
					Quaternion q;
					Vector3 p;
					wheel.GetWorldPose (out p, out q);

					// assume that the only child of the wheelcollider is the wheel shape
					Transform shapeTransform = wheel.transform.GetChild (0);
					shapeTransform.position = p;
					shapeTransform.rotation = q;
				}

			}
		}


		else if (currentspeed > topspeed) {
			foreach (WheelCollider wheel in wheels) {

				// a simple car where front wheels steer while rear ones drive
				if (wheel.transform.localPosition.z > 0)
					wheel.steerAngle = angle;

				if (wheel.transform.localPosition.z < 0)
					wheel.motorTorque = 0;



				// update visual wheels if any
				if (wheelShape) {
					Quaternion q;
					Vector3 p;
					wheel.GetWorldPose (out p, out q);

					// assume that the only child of the wheelcollider is the wheel shape
					Transform shapeTransform = wheel.transform.GetChild (0);
					shapeTransform.position = p;
					shapeTransform.rotation = q;
				}

			}
		}


			}


    }

    

    

