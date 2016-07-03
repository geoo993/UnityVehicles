using UnityEngine;
using System.Collections;

public class JetMovement : MonoBehaviour {

	public float playerSpeed = 10.0f;
	public float rotateSpeed = 80.0f;
	public float hoveringSpeed = 40.0f;

	private bool playerFlying = false;

	public GameObject[] jetObjects;

	private Vector3 moveForward;


	void Start ()
	{
		changeColor ();
	}

	void FixedUpdate() {

		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveForward) * playerSpeed * Time.deltaTime);
	}

	void Update ()
	{
		//jetMove ();
	}
	public void JetMove ()
	{
	

		//OnGUI ();

		moveForward = new Vector3 (0, 0, Input.GetAxis ("Vertical")).normalized;
		//moveForward = new Vector3 (0, 0, Input.GetAxis("Vertical2")).normalized;

		if (playerFlying) {
			GetComponent<Rigidbody> ().useGravity = true;
			GetComponent<Rigidbody>().AddForce(transform.up * (-2.0f));
		}
		if (!playerFlying) {
			GetComponent<Rigidbody> ().useGravity = false;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.eulerAngles = new Vector3(Mathf.LerpAngle(0, 10f, Time.time * 1f), transform.eulerAngles.y, transform.eulerAngles.z);

		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
		
			transform.eulerAngles = new Vector3(Mathf.LerpAngle(0, 0f, Time.time * 1f), transform.eulerAngles.y, transform.eulerAngles.z);
		}


		if (Input.GetKey (KeyCode.DownArrow)) {

			transform.eulerAngles = new Vector3(Mathf.LerpAngle(0, -10f, Time.time * 1f), transform.eulerAngles.y, transform.eulerAngles.z);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			transform.eulerAngles = new Vector3(Mathf.LerpAngle(0, 0f, Time.time * 1f), transform.eulerAngles.y, transform.eulerAngles.z);
		}


		//this.transform.Rotate (0.0f, (rotateSpeed) * (Input.GetAxis ("PS4_RightAnalogHorizontal")), 0.0f);

		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.Joystick1Button5)) {

			this.transform.Rotate ( new Vector3( 0, rotateSpeed * Time.deltaTime, 0));

		}
		else if (Input.GetKey (KeyCode.LeftArrow)|| Input.GetKey (KeyCode.Joystick1Button4)) {
		
			this.transform.Rotate (new Vector3 (0, -rotateSpeed * Time.deltaTime, 0));

		}

		if (Input.GetKey (KeyCode.W)|| Input.GetKey (KeyCode.Joystick1Button1)) {

			playerFlying = false;
			GetComponent<Rigidbody>().AddForce(transform.up * hoveringSpeed);

		} 
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.Joystick1Button1)) {
			
			playerFlying = true;

		}


	}

	public void changeColor ()
	{
		Color playerColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		
		GetComponent<MeshRenderer> ().material.color = playerColor;
		
		for (int i = 0; i < jetObjects.Length; i ++) {
			jetObjects[i].GetComponent<MeshRenderer> ().material.color = playerColor;
		}
		
	}
	
//	void OnGUI() {
//		
//		GUI.contentColor = Color.green; 
//		GUI.Label(new Rect(10, 10, 200, 20), ("Jet X: "+ this.transform.position.x).ToString());
//		GUI.Label(new Rect(10, 30, 200, 20), ("Jet Y: "+ this.transform.position.y).ToString());
//		GUI.Label(new Rect(10, 50, 200, 20), ("Jet Z: "+ this.transform.position.z).ToString());
//		GUI.Label(new Rect(10, 70, 200, 20), ("Jet X: "+ this.transform.rotation.x).ToString());
//		GUI.Label(new Rect(10, 90, 200, 20), ("Jet Rotation Y: "+ this.transform.rotation.y).ToString());
//		GUI.Label(new Rect(10, 110, 200, 20), ("Jet Rotation Z: "+ this.transform.rotation.z).ToString());
//	}


}
