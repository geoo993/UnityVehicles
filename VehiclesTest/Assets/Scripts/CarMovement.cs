using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public Transform[] wheels;
	public float enginePower = 150.0f;
	public float power = 0.0f;
	private float brake = 0.0f;
	private float steer = 0.0f;
	private float maxSteer = 20.0f;

	bool forwardgear = false;
	bool reversegear = false;

	int keypressed = 0;
	string gearstring = "Neutral";

	public WheelCollider FrontLeft;
	public WheelCollider FrontRight;
	public WheelCollider RearLeft;
	public WheelCollider RearRight;
	

	public GameObject carBody;


	void Start()
	{
		GetComponent<Rigidbody>().centerOfMass = new Vector3(0f,-0.5f,0.3f);
		
		Color carColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		
		carBody.GetComponent<MeshRenderer> ().material.color = carColor;
		
	}
	
	void Update ()
	{
		//carMove ();
		
	}

	public void carMove()
	{
//			if (Input.GetKeyDown("z") || Input.GetKey (KeyCode.Joystick1Button1))
//			{
//				keypressed = keypressed + 1;
//				if (keypressed >= 1)
//					keypressed = 1;
//			}
//			if (Input.GetKeyDown("x") || Input.GetKey (KeyCode.Joystick1Button0))
//			{
//				keypressed = keypressed - 1;
//				if (keypressed <= -1)
//					keypressed = -1;
//			}
//			if (keypressed == -1)
//			{
//				forwardgear = false;
//				reversegear = true;
//				gearstring = "Reverse";
//			}  
//			if (keypressed == 0)
//			{
//				forwardgear = false;
//				reversegear = false;
//				gearstring = "Neutral";
//			}  
//			if (keypressed == 1)
//			{
//				forwardgear = true;
//				reversegear = false;
//				gearstring = "Forward";
//			}  
//			if (forwardgear == true)
//			{
//				power = Input.GetAxis("Vertical");  // throttle"
//				power = (power + 1) * 0.5f;
//				power = power * enginePower * Time.deltaTime * 100;
//				steer = -Input.GetAxis("Horizontal") * maxSteer;  //"steering"
//				//brake = Input.GetKey("space");//Input.GetAxis("brakes");
//				brake = (brake +1) * 0.5f;
//				brake = Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 0.05f: 0.0f;
//				GetCollider(0).steerAngle = steer;
//				GetCollider(1).steerAngle = steer;
//
//				if(brake > 0.0f)
//				{
//					GetCollider(0).brakeTorque = brake;
//					GetCollider(1).brakeTorque = brake;
//					GetCollider(2).brakeTorque = brake;
//					GetCollider(3).brakeTorque = brake;
//					GetCollider(2).motorTorque = 0.0f;
//					GetCollider(3).motorTorque = 0.0f;
//				} else {
//					GetCollider(0).brakeTorque = 0;
//					GetCollider(1).brakeTorque = 0;
//					GetCollider(2).brakeTorque = 0;
//					GetCollider(3).brakeTorque = 0;
//					GetCollider(2).motorTorque = power;
//					GetCollider(3).motorTorque = power;
//				}
//			}
//			if (reversegear == true)
//			{
//				power = Input.GetAxis("Vertical");  //throttle"
//				power = (power + 1) * 0.5f;
//				power = -power;
//				power = power * enginePower * Time.deltaTime * 50;
//				steer = -Input.GetAxis("Horizontal") * maxSteer;  //steering"
//				//brake = Input.GetKey("space");//Input.GetAxis("brakes");
//				
//				brake = (brake +1) * 0.5f;
//				//brake = Input.GetAxis("PS4_R2") ? GetComponent<Rigidbody>().mass * 0.05f: 0.0f;
//				brake = Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 0.05f: 0.0f;
//				GetCollider(0).steerAngle = steer;
//				GetCollider(1).steerAngle = steer;
//				
//				if(brake > 0.0f)
//				{
//					GetCollider(0).brakeTorque = brake;
//					GetCollider(1).brakeTorque = brake;
//					GetCollider(2).brakeTorque = brake;
//					GetCollider(3).brakeTorque = brake;
//					GetCollider(2).motorTorque = 0.0f;
//					GetCollider(3).motorTorque = 0.0f;
//				} else {
//					GetCollider(0).brakeTorque = 0;
//					GetCollider(1).brakeTorque = 0;
//					GetCollider(2).brakeTorque = 0;
//					GetCollider(3).brakeTorque = 0;
//					GetCollider(2).motorTorque = power;
//					GetCollider(3).motorTorque = power;
//				}
//			}

		//part2
//		power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
//		steer = -Input.GetAxis("Horizontal") * maxSteer;
//		brake = Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 0.1f: 0.0f;
//		
//		FrontLeft.steerAngle = steer;
//		FrontRight.steerAngle = steer;
//		
//		if(brake > 0.0f)
//		{
//			FrontLeft.brakeTorque = brake;
//			FrontRight.brakeTorque = brake;
//			RearLeft.brakeTorque = brake;
//			RearRight.brakeTorque = brake;
//			RearLeft.motorTorque = 0.0f;
//			RearRight.motorTorque = 0.0f;
//		}
//		else
//		{
//			FrontLeft.brakeTorque = 0;
//			FrontRight.brakeTorque = 0;
//			RearLeft.brakeTorque = 0;
//			RearRight.brakeTorque = 0;
//			RearLeft.motorTorque = power;
//			RearRight.motorTorque = power;
//		}


		//part3
		power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 50.0f;
		brake = GetComponent<Rigidbody>().mass * 0.05f;
		RearLeft.motorTorque = power;
		RearRight.motorTorque = power;

		RearLeft.brakeTorque = 0;
		RearLeft.brakeTorque = 0;

		steer = -Input.GetAxis("Horizontal") * maxSteer;

		FrontLeft.steerAngle = steer;
		FrontRight.steerAngle = steer;

		if (Input.GetKey ("space")) {
			RearLeft.brakeTorque = brake;
			RearLeft.brakeTorque = brake;
		}

}
	
	WheelCollider GetCollider (int n)  {
		
		return wheels[n].gameObject.GetComponent<WheelCollider>();
	}
	
//	void OnGUI() {
//		
//			GUI.contentColor = Color.green; 
//			GUI.Label(new Rect(410, 10, 200, 20), ("Car X: "+ this.transform.position.x).ToString());
//			GUI.Label(new Rect(410, 30, 200, 20), ("Car Y: "+ this.transform.position.y).ToString());
//			GUI.Label(new Rect(410, 50, 200, 20), ("Car Z: "+ this.transform.position.z).ToString());
//			GUI.Label(new Rect(410, 70, 200, 20), ("Car X: "+ this.transform.rotation.x).ToString());
//			GUI.Label(new Rect(410, 90, 200, 20), ("Car Rotation Y: "+ this.transform.rotation.y).ToString());
//			GUI.Label(new Rect(410, 110, 200, 20), ("Car Rotation Z: "+ this.transform.rotation.z).ToString());
//			GUI.Label(new Rect(410, 130, 200, 20), ("Gear : "+ gearstring).ToString());
//			GUI.Label(new Rect(410, 150, 200, 20), ("Power: "+ power).ToString());
//			GUI.Label(new Rect(410, 170, 200, 20), ("Brake: "+brake).ToString());
//			GUI.Label(new Rect(410, 190, 200, 20), ("Steer: "+steer).ToString());
//	}

}
