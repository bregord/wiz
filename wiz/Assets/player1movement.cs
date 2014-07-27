using UnityEngine;
using System.Collections;

public class player1movement : MonoBehaviour {

	/*
	// Use this for initialization
	void Start () {
		//GameObject player1 = GameObject.FindWithTag("Player1");
	}

	public float speed;
	public float jumpspeed;
	public float fallspeed;
	public float rollspeed;
	public GameObject player1;

	public CharacterController control;



	void FixedUpdate () {
		//C# case statement for movement?

		//set a wand or no wand state. i.e. wand = tru


		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical =- fallspeed;//Input.GetAxis ("Vertical");



		//Jump
		if (Input.GetKey ("v") ) {

			if (player1.transform.position.y < 0.1) {
				moveVertical = jumpspeed;
			}
			//moveVertical = jumpspeed;//();
			//rigidbody2D.velocity = jumpspeed *  
			//	rigidbody2D
			//jump ();
		}

	
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * speed;


	}

	float jump() {
		//If the character is grounded, make its upward velocity equal to its jumpspeed.
	if (player1.transform.position.y == 0) {
						return jumpspeed;
				} else {
						return 0;
				}
	}

	//roll method
		//if a character is moving, and press down, then roll. Their speed will decrease slightly

	//shoot method
	//

	//if a character is hit by a spell


*/


	/*
	// ====== TRYING WITH CHARACTER CONTROLLER ===
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	void Update() {
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
*/

}
