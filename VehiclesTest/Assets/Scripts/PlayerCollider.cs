using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {


	public GameObject player = null;
	public GameObject wings = null;

	private Animator animator;
	
	
	void Awake ()
	{
		animator = wings.GetComponent<Animator> ();
	}


	void OnTriggerEnter (Collider other)
	{
		//Debug.Log (" Object: "+ other.name + " entered trigger.");
		//player.GetComponent<Movement>().changeColor ();

		animator.enabled = false;
		
	}
	void OnTriggerExit (Collider other)
	{
		animator.enabled = true;
		animator.Play ( "WingsAnimation" );
	}
}
