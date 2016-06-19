using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {



	private float transitionSpeed = 0.5f;
	
	public GameObject rightWing = null;
	public GameObject leftWing = null;

	public bool transState = false;

	private float scaleX;
	private float scaleY;

	private Vector3 carRot = new Vector3(0,0,270);
	private Vector3 planeLeftRot = new Vector3(0,0,180);
	private Vector3 planeRightRot = new Vector3(0,0,0);


	void Awake ()
	{
		scaleX = leftWing.transform.localScale.x;
		scaleY = leftWing.transform.localScale.y;

	}
	void Update () {


		if (!transState) {

			scaleX += transitionSpeed * Time.deltaTime;
			if (scaleX >= 2)
			{
				scaleX = 2;
			}

			scaleY -= transitionSpeed * Time.deltaTime;
			if (scaleY <= 0.5f)
			{
				scaleY = 0.5f;
			}

			leftWing.transform.localScale = new Vector3(Mathf.Clamp(scaleX, 1.0F, 2.0F), Mathf.Clamp(scaleY, 0.5F, 1.0F), 1);
			rightWing.transform.localScale = new Vector3(Mathf.Clamp(scaleX, 1.0F, 2.0F), Mathf.Clamp(scaleY, 0.5F, 1.0F), 1);

			rightWing.transform.localRotation = Quaternion.Lerp(rightWing.transform.localRotation, Quaternion.Euler(carRot), transitionSpeed * Time.deltaTime );
			leftWing.transform.localRotation = Quaternion.Lerp(leftWing.transform.localRotation, Quaternion.Euler(carRot), transitionSpeed * Time.deltaTime );


		}
		if (transState ) {

			scaleX -= transitionSpeed * Time.deltaTime;
			if (scaleX <= 1)
			{
				scaleX = 1;
			}
			scaleY += transitionSpeed * Time.deltaTime;
			if (scaleY >= 1f)
			{
				scaleY = 1f;
			}
			leftWing.transform.localScale = new Vector3(Mathf.Clamp(scaleX, 1.0F, 2.0F), Mathf.Clamp(scaleY, 0.5F, 1.0F), 1);
			rightWing.transform.localScale = new Vector3(Mathf.Clamp(scaleX, 1.0F, 2.0F), Mathf.Clamp(scaleY, 0.5F, 1.0F), 1);

			rightWing.transform.localRotation = Quaternion.Lerp(rightWing.transform.localRotation, Quaternion.Euler(planeRightRot), transitionSpeed * Time.deltaTime );
			leftWing.transform.localRotation = Quaternion.Lerp(leftWing.transform.localRotation, Quaternion.Euler(planeLeftRot), transitionSpeed * Time.deltaTime );

		}


		if (transform.localPosition.y > 2) {
			transState = true;
		} else {
			transState = false;
		}
		if( Input.GetKeyDown (KeyCode.Space)){

			transState = !transState;
		}

	}


}
