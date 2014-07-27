using UnityEngine;
using System.Collections;

public class shotMover : MonoBehaviour {

	public float speed;
	// Use this for initialization

	public bool rightShot;

	void Start(){
	}
	


	void FixedUpdate(){

		if (rightShot) {
						
			rigidbody2D.velocity = transform.right * speed;
				} else {
			rigidbody2D.velocity = transform.right * -speed;
		}
		}



	// Update is called once per frame
	void Update () {
	


	}
}
