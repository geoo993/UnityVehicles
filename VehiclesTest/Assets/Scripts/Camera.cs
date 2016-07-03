using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public Transform player = null;
	public Transform playerTarget = null;


	private float cameraDistance = 30.0f; //10.0f;
	private float cameraHeight = 2.0f; //5.0f;

	private float heightDamping = 4.0f;  //2.0f;
	private float rotationDamping = 3.0f;
	
	private float cameraMoveSpeed = 3;
	private float cameraTurnSpeed = 1; 
	private float cameraRollSpeed = 0.2f;
	private bool cameraFollowVelocity = false;
	private bool cameraFollowTilt = true; 
	private float cameraSpinTurnLimit = 90;
	private float cameraTargetVelocityLowerLimit = 4f;
	private float cameraSmoothTurnTime = 0.2f;
	
	private float cameraLastFlatAngle;
	private float cameraCurrentTurnAmount;
	private float cameraTurnSpeedVelocityChange;
	private Vector3 cameraRollUp = Vector3.up;

	Rigidbody targetRigidbody = null; 

	void Awake ()
	{

		targetRigidbody = playerTarget.GetComponent<Rigidbody> ();

	}
	void Update ()
	{
			
		player.GetComponent<Airplane> ().PlaneMove ();
			
	}
	void LateUpdate () {

		//LookAtTarget ();
		Follow ();
	}
	



	void LookAtTarget ()
	{
		this.transform.LookAt (player.position);

	}
	
		
	void Follow()
	{

		if (!(Time.deltaTime > 0) || playerTarget == null)
		{
			return;
		}

		var targetForward = playerTarget.forward;
		var targetUp = playerTarget.up;
		
		if (cameraFollowVelocity && Application.isPlaying)
		{
			if (targetRigidbody.velocity.magnitude > cameraTargetVelocityLowerLimit)
			{
				targetForward = targetRigidbody.velocity.normalized;
				targetUp = Vector3.up;
			}
			else
			{
				targetUp = Vector3.up;
			}
			cameraCurrentTurnAmount = Mathf.SmoothDamp(cameraCurrentTurnAmount, 1, ref cameraTurnSpeedVelocityChange, cameraSmoothTurnTime);
		}
		else
		{
			var currentFlatAngle = Mathf.Atan2(targetForward.x, targetForward.z)*Mathf.Rad2Deg;
			if (cameraSpinTurnLimit > 0)
			{
				var targetSpinSpeed = Mathf.Abs(Mathf.DeltaAngle(cameraLastFlatAngle, currentFlatAngle))/Time.deltaTime;
				var desiredTurnAmount = Mathf.InverseLerp(cameraSpinTurnLimit, cameraSpinTurnLimit * 0.75f, targetSpinSpeed);
				var turnReactSpeed = (cameraCurrentTurnAmount > desiredTurnAmount ? .1f : 1f);
				if (Application.isPlaying)
				{
					cameraCurrentTurnAmount = Mathf.SmoothDamp(cameraCurrentTurnAmount, desiredTurnAmount,
					                                       ref cameraTurnSpeedVelocityChange, turnReactSpeed);
				}
				else
				{
					cameraCurrentTurnAmount = desiredTurnAmount;
				}
			}
			else
			{
				cameraCurrentTurnAmount = 1;
			}
			cameraLastFlatAngle = currentFlatAngle;
		}

		transform.position = Vector3.Lerp(transform.position, playerTarget.position, Time.deltaTime * cameraMoveSpeed);


		if (!cameraFollowTilt)
		{
			targetForward.y = 0;
			if (targetForward.sqrMagnitude < float.Epsilon)
			{
				targetForward = transform.forward;
			}
		}

		var rollRotation = Quaternion.LookRotation(targetForward, cameraRollUp);

		//cameraRollUp = cameraRollSpeed > 0 ? Vector3.Slerp(cameraRollUp, targetUp, cameraRollSpeed * Time.deltaTime) : Vector3.up;
		transform.rotation = Quaternion.Lerp(transform.rotation, rollRotation, cameraTurnSpeed * cameraCurrentTurnAmount * Time.deltaTime);
	}


}


