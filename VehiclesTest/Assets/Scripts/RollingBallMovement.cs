using UnityEngine;
using System.Collections;

public class RollingBallMovement : MonoBehaviour {

	private Rigidbody rigid; 
	public static float speed = 0.0f;// speed variable is the speed
	private float moveVertical = 0.0f;
	private float moveHorizontal = 0.0f;
	private float rollinglerp = 0.0f;
	public float jump = 3f;
	public GameObject camera;
	public GameObject[] allObject = null;
	private float size = 1000.0f;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();

		for (int i = 0; i < 200; i++) {

			Vector3 pos = new Vector3(Random.Range (-size, size), 10.0f ,Random.Range (-size, size));
			GameObject a = (GameObject) Instantiate(allObject [Random.Range (0, allObject.Length - 1)], pos, Quaternion.identity);

			Vector3 scale = new Vector3 (Random.Range (1.0f, 30.0f), Random.Range (1.0f, 30.0f), Random.Range (1.0f, 30.0f));
			a.transform.localScale = scale;
			a.GetComponent<MeshRenderer> ().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("space")) 
		{
			
			//print ("space");
//			this.transform.rotation = Quaternion.Euler(0, 0, 0);
//
//			Vector3 look = new Vector3 (0.0f, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
//			this.transform.LookAt(Camera.main.gameObject.transform.position,Camera.main.gameObject.transform.forward);
//

			//jump
			//rigid.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
			////rigid.AddForce(Vector3.up * jump);
		}
	}
	void FixedUpdate ()
	{
			BallMovement ();

	}

	void BallMovement(){
		
		speed = 200f;
	
		moveHorizontal = Input.GetAxis ("Horizontal"); 
		moveVertical = Input.GetAxis ("Vertical");

		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical) * (speed * 10) * Time.deltaTime;
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f) * (speed * 10) * Time.deltaTime;

		//rigid.AddForce(Camera.main.gameObject.transform.forward * (moveVertical * 10) * Time.deltaTime);
		rigid.AddForce((camera.transform.forward ) * moveVertical );
		//rigid.AddForce(movement);
		//print (rigid.velocity);

		if (moveHorizontal == 0 && moveVertical == 0 && this.transform.position.y <= 5f ) {

			//rigid.velocity = Vector3.zero;
			//rigid.angularVelocity = Vector3.zero;
			//rigid.Sleep ();

			if (rollinglerp < 1.0f) {

				rollinglerp += Time.deltaTime * (1.0f / 10.0f);
			}

			rigid.velocity = Vector3.Lerp (rigid.velocity, new Vector3(0,0,0), rollinglerp);
			rigid.angularVelocity = Vector3.Lerp (rigid.angularVelocity, Vector3.zero, rollinglerp);

		} else {
			rollinglerp = 0;
		}

		print ("vel: "+rigid.velocity + "   vert: " + moveVertical + "   hor" + moveHorizontal+"   lerp: "+rollinglerp);





	}
}
