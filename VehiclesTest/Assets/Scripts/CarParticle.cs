using UnityEngine;
using System.Collections;

public class CarParticle : MonoBehaviour {

	public GameObject car = null;
	private float carSpeed = 0;
	private float exhaustRate = 0.2f;
	
	ParticleSystem exhaust;
	
	
	void Start () {
		exhaust = GetComponent<ParticleSystem>();
		 
	}
	
	
	void Update () {

		carSpeed = car.GetComponent<CarMovement> ().power;
		exhaust.emissionRate = carSpeed * exhaustRate;

		//Debug.Log (" "+carSpeed+"  "+exhaust.emissionRate);
	}


}
