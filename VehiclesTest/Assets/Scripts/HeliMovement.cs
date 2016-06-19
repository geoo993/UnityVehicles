using UnityEngine;
using System.Collections;

public class HeliMovement : MonoBehaviour {




	public GameObject mainRotorGameObject;            
	public GameObject tailRotorGameObject;            
	
	public float  maxRotorForce = 22241.1081f;  
	private float maxRotorVelocity = 7200;           
	private float rotorVelocity = 0.0f;         
	private float rotorRotation = 0.0f;        
	
	public float  maxtailRotorForce = 15000.0f;       
	public float  maxTailRotorVelocity = 2200.0f;        
	private float tailRotorVelocity = 0.0f;      
	private float tailRotorRotation = 0.0f;         
	
	public float forwardRotorTorqueMultiplier = 0.5f;//0.5f;         
	public float sidewaysRotorTorqueMultiplier = 0.5f;          
	
	private bool mainRotorActive = true;      
	private bool tailRotorActive = true;      

	private Vector3 torqueValue;

	private Vector3 controlTorque;

	public float speed = 0.0f;  
	public float rotationSpeed = 5.0f;

	public GameObject[] heliObjects;

	void Start ()
	{
		Color heliColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		
		for (int i = 0; i < heliObjects.Length; i ++) {
			heliObjects[i].GetComponent<MeshRenderer> ().material.color = heliColor;
		}
	}

	void FixedUpdate () {

		// right analog(rotate left and right)   //horizontal2(tilt left and tilt right)
		Vector3 controlTorque = new Vector3(Input.GetAxis("PS4_RightAnalogVertical") * forwardRotorTorqueMultiplier,1.0f,-Input.GetAxis( "Horizontal2" ) * sidewaysRotorTorqueMultiplier);

		if (mainRotorActive == true) {
			torqueValue = (controlTorque * maxRotorForce * rotorVelocity);////////////////

			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.up * maxRotorForce * rotorVelocity);
		}
		if ( Vector3.Angle( Vector3.up, transform.up ) < 80 ) {
				transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( 0.0f, transform.rotation.eulerAngles.y, 0.0f ), Time.deltaTime * rotorVelocity * 2.0f );
				//transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,new Vector3(0,transform.rotation.eulerAngles.y,0),Time.deltaTime * rotor_Velocity * 2);

		}

		if ( tailRotorActive == true ) {
			torqueValue -= (Vector3.up * maxtailRotorForce * tailRotorVelocity);
		}
		GetComponent<Rigidbody>().AddRelativeTorque( torqueValue );/////////////

	}
	void Update () {


		if (Input.GetKey (KeyCode.Joystick1Button0))
		{
			Debug.Log(" Square");
		}
		if (Input.GetKey (KeyCode.Joystick1Button1))
		{
			Debug.Log(" X");
		}
		if (Input.GetKey (KeyCode.Joystick1Button2))
		{
			Debug.Log(" O");
		}
		if (Input.GetKey (KeyCode.Joystick1Button5))
		{
			Debug.Log(" R1");
		}
		if (Input.GetKey (KeyCode.Joystick1Button4))
		{
			Debug.Log(" L1");
		}
		if (Input.GetKey (KeyCode.Joystick1Button7))
		{
			Debug.Log(" R2");
		}
		if (Input.GetKey (KeyCode.Joystick1Button6))
		{
			Debug.Log(" L2");
		}
	
		//Debug.Log ("right analog horizontal: "+ Input.GetAxis("PS4_RightAnalogHorizontal")
		//           +", right analog vertical: "+ Input.GetAxis("PS4_RightAnalogVertical")
		//           +", horizontal2: "+ Input.GetAxis("Horizontal2")
		//           +", vertical2: "+ Input.GetAxis("Vertical2"));

//		Debug.Log (("vertical - forward rotor: "+ Input.GetAxis ("Vertical") * forwardRotorTorqueMultiplier)
//		           +", horizontal2 - side ways rotor: "+(-Input.GetAxis( "Horizontal2" ) * sidewaysRotorTorqueMultiplier)
//		           +",  Verticle2 -  rotor velocity: "+ (rotorVelocity += Input.GetAxis( "Vertical2" ) * 0.001f)
//		           +",Horizontal:  "+Input.GetAxis( "Horizontal" ));


		//OnGUI ();
	}

	public void HelicopterMove ()
	{
		GetComponent<AudioSource>().pitch = rotorVelocity;

		//left&&right
		if ( mainRotorActive == true ) {
			
			mainRotorGameObject.transform.rotation = transform.rotation * Quaternion.Euler( 0.0f, rotorRotation, 0.0f );
			
		}
		if ( tailRotorActive == true ) {
			tailRotorGameObject.transform.rotation = transform.rotation * Quaternion.Euler( tailRotorRotation, 0.0f, 0.0f );
			
		}
		
		rotorRotation += maxRotorVelocity * rotorVelocity * Time.deltaTime;
		tailRotorRotation += maxTailRotorVelocity * rotorVelocity * Time.deltaTime;

		float hoverRotorVelocity = (GetComponent<Rigidbody>().mass * Mathf.Abs( Physics.gravity.y ) / maxRotorForce);
		float hoverTailRotorVelocity = (maxRotorForce * rotorVelocity) / maxtailRotorForce;

		//up&&down
		if ( Input.GetAxis( "Vertical2" ) != 0.0f ) 
		{
			rotorVelocity += Input.GetAxis( "Vertical2" ); //* 0.001f;
		}else{
			rotorVelocity = Mathf.Lerp( rotorVelocity, hoverRotorVelocity, Time.deltaTime * Time.deltaTime * 5);
		}

		tailRotorVelocity = hoverTailRotorVelocity - Input.GetAxis( "PS4_RightAnalogHorizontal" ) * rotationSpeed;
		
		if ( rotorVelocity > 1.0f ) {
			rotorVelocity = 1.0f;
		}else if ( rotorVelocity < 0.0f ) {
			rotorVelocity = 0.0f;
		}


		if ( speed < 400 ) 
		{
			speed += Time.deltaTime * 140;  //accelerate
		}
		if ( speed > 200 ) 
		{
			speed -= Time.deltaTime * 140;   //deacclerate
		}
		if ( speed < 0 )
		{ 
			speed = 0; 
		}	
//		if ( !Input.GetAxis( "PS4_RightAnalogHorizontal" ) && ( speed > 90.0f ) && ( speed < 100.0f ) )
//		{
//			speed = 95.0f;
//		}

		//transform.Translate( 0.0f, 0.0f, Input.GetAxis( "PS4_RightAnalogHorizontal" ) * speed );

		//print (Input.GetAxis ("PS4_RightAnalogHorizontal") * speed * Time.deltaTime);
	}


//	void OnGUI() {
//		
//		GUI.contentColor = Color.green; 
//		GUI.Label(new Rect(810, 10, 200, 20), ("Heli X: "+ this.transform.position.x).ToString());
//		GUI.Label(new Rect(810, 30, 200, 20), ("Heli Y: "+ this.transform.position.y).ToString());
//		GUI.Label(new Rect(810, 50, 200, 20), ("Heli Z: "+ this.transform.position.z).ToString());
//		GUI.Label(new Rect(810, 70, 200, 20), ("Heli X: "+ this.transform.rotation.x).ToString());
//		GUI.Label(new Rect(810, 90, 200, 20), ("Heli Rotation Y: "+ this.transform.rotation.y).ToString());
//		GUI.Label(new Rect(810, 110, 200, 20), ("Heli Rotation Z: "+ this.transform.rotation.z).ToString());
//		GUI.Label(new Rect(810, 130, 200, 20), ("Control Torque: "+ controlTorque).ToString());
//		GUI.Label(new Rect(810, 150, 200, 20), ("Target Value: "+ torqueValue).ToString());
//	}

	public float ClampAngle(float angle, float min, float max){
		
		if (angle<90 || angle>270){       // if angle in the critic region...
			if (angle>180) angle -= 360;  // convert all angles to -180..+180
			if (max>180) max -= 360;
			if (min>180) min -= 360;
		}  
		angle = Mathf.Clamp(angle, min, max);
		if (angle<0) angle += 360;  // if angle negative, convert to 0..360
		return angle;
	}

}
