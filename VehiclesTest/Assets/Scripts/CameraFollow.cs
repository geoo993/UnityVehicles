using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform jetPack = null;
	public Transform jetTarget = null;

	public Transform plane = null;

	public Transform heli = null;
	public Transform heliTarget = null;

	public Transform car = null;
	public Transform carTarget = null;

	private Vector3 speed =  new Vector3 (3.0f, 5.0f, 2.0f);

	private Vector3 cameraOriginalPosition = Vector3.zero;
	private Vector3 nextPosition = Vector3.zero;

	private float distance = 30.0f; //10.0f;
	private float height = 2.0f; //5.0f;
	
	private float heightDamping = 4.0f;  //2.0f;
	private float rotationDamping = 3.0f;

	private Vector3 positionVelocity;
	[Range(-50.0f, 50.0f)]public float distanceUP, distanceBack, minimumHeight =  1.0f;

	public enum PlayerPrefs { jet, airPlane, helicopter, car };
	public PlayerPrefs playerPrefs = PlayerPrefs.jet;


	void Awake ()
	{
		cameraOriginalPosition = this.transform.position;

	}

	void Update ()
	{
		switch (playerPrefs) {
			
		case PlayerPrefs.jet:  jetPack.GetComponent<JetMovement>().JetMove();   break;
		case PlayerPrefs.airPlane: plane.GetComponent<Airplane> ().PlaneMove (); break;
		case PlayerPrefs.helicopter: heli.GetComponent<HeliMovement>().HelicopterMove(); break;
		case PlayerPrefs.car:  car.GetComponent<CarMovement>().carMove(); break;

		}

	}
	void LateUpdate () {

		switch (playerPrefs) {
			
			case PlayerPrefs.jet:  FollowJetTarget();   LookAtJet ();   break;
			case PlayerPrefs.airPlane:  FollowPlaneTarget();  LookAtPlane();  break;
			case PlayerPrefs.helicopter:  FollowHelicopterTarget();  LookAtHeli();  break;
			case PlayerPrefs.car:  FollowCarTarget();  LookAtCar();  break;
		}

	}



	void FollowJetTarget ()
	{
		//jet
		nextPosition.x = Mathf.Lerp (transform.position.x, jetTarget.position.x, speed.x * Time.deltaTime);
		nextPosition.y = Mathf.Lerp (transform.position.y, jetTarget.position.y, speed.y * Time.deltaTime);
		nextPosition.z = Mathf.Lerp (transform.position.z, jetTarget.position.z, speed.z * Time.deltaTime);
	
		transform.position = nextPosition;
		
	}

	void FollowPlaneTarget ()
	{

		////calculate a new position to place the camera:
		Vector3 newPosition =  plane.transform.position + (plane.transform.forward * distanceBack);
		float newPosY = Mathf.Max (newPosition.y + distanceUP, minimumHeight);
		newPosition = new Vector3(newPosition.x, newPosY, newPosition.z);


		transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref positionVelocity, 0.18f);


	}

	void FollowHelicopterTarget ()
	{
		//heli
		float wantedRotationAngle = heliTarget.eulerAngles.y;
		float wantedHeight = heliTarget.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		Quaternion currentRotation = Quaternion.Euler (0.0f, currentRotationAngle, 0.0f);
		
		transform.position = heliTarget.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		transform.position = new Vector3(heliTarget.position.x, currentHeight, heliTarget.position.z);
		
	}

	void FollowCarTarget()
	{
		//jet
		nextPosition.x = Mathf.Lerp (transform.position.x, carTarget.position.x, speed.x * Time.deltaTime);
		nextPosition.y = Mathf.Lerp (transform.position.y, carTarget.position.y, speed.y * Time.deltaTime);
		nextPosition.z = Mathf.Lerp (transform.position.z, carTarget.position.z, speed.z * Time.deltaTime);
		
		transform.position = nextPosition;

	}

	void LookAtHeli ()
	{
		
		this.transform.LookAt (heli.position);
	}
	void LookAtPlane ()
	{
		this.transform.LookAt (plane.position + (plane.forward * 5));

	}

	void LookAtJet ()
	{
		this.transform.LookAt (jetPack.position);
	}

	void LookAtCar ()
	{
		this.transform.LookAt (car.position);
		
	}

	void CameraFirstPosition ()
	{
		this.transform.position = cameraOriginalPosition;
	}




}
