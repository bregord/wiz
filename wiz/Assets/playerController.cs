﻿using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//Public Variables
	public BoxCollider2D frontHitbox; //Our front hitbox
	public BoxCollider2D backHitbox;
	public BoxCollider2D tempHitBox = null;
	

	private bool faceRight = true; //tells us we are facing right or not
	
	//There are three idle states. Idle high, idle mid, idle low
	public bool idleHigh;
	public bool idleMid;
	public bool idleLow;
	
	
	//There are several movement states. Rolling, Crouched, and Grounded
	
	bool rolling = false;
	bool crouched = false;
	
	//The animator object
	Animator anim;
	
	//For ground detection
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	
	
	//max speed the player can run at and their jump height
	public float maxSpeed;
	public float maxRollSpeed;
	public float jumpForce;
	public float maxCrouchSpeed;
	
	//The wand stuff goes here
	public int wandpos = 0; //Low is -1, mid is 0, high is 1;
	//There are other options if you are up in the air
	public bool holdingWand = true; //You start he game with a wand. If you drop it, this becomes false
	public GameObject wand;
	
	
	//shoot spells
	public GameObject rightShot;
	public GameObject leftShot;

	public GameObject leftDisarmShot;
	public GameObject rightDisarmShot;
	//public GameObject disarm;
	//public GameObject beam;

	public GameObject shield;
	bool shielded = false;
	
	
	public Transform shieldSpawn;
	public Transform shotSpawn;
	
	
	public float fireRate;
	private float nextFire;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); 

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround); //generates a circle at a position, with a radious
		//and tells you if there is a thing it collides with there. If yes, then true
		
		anim.SetBool ("Ground", grounded); //This tells the animator if we are on the ground or not

		anim.SetFloat("Speed", Mathf.Abs(move)); //Connects the animator paramater Speed to movement.
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); 

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);



		if (move > 0 && !faceRight) {
			Flip ();
		} else if (move < 0 && faceRight) {
			Flip();
		}




		//Getting hit by stuff.



		//Getting Disarmed



		
	}


// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Horizontal");


		//Jump Code =====================
		if (grounded && Input.GetKeyDown (".")) {
			
			
			//If we are grounded, and pressing the spacebar, add the jumpforce
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		//Roll COde=========================
		if (grounded && Input.GetKey ("down") && Mathf.Abs(rigidbody2D.velocity.x) > 3) {



			rolling = true;
			anim.SetBool ("Roll", true);
			rigidbody2D.velocity = new Vector2 (move * maxSpeed * Time.deltaTime , rigidbody2D.velocity.y);
				} else {
			rolling = false;
			anim.SetBool ("Roll", false);

				}

		//Crouch code
		if (grounded && Input.GetKey ("down") && Mathf.Abs(rigidbody2D.velocity.x) <= 3 ) {

			rigidbody2D.drag += 2;
			rigidbody2D.velocity = new Vector2 (move * maxCrouchSpeed * Time.deltaTime , rigidbody2D.velocity.y);
			crouched = true;
			anim.SetBool ("Crouch", true);
			
		} else {
			rigidbody2D.drag = 0;
			crouched = false;
			anim.SetBool ("Crouch", false);
			
		}


		if(grounded && Input.GetKey("down") && move > 3 ){
			
		}

		//Bolt shot code=====================
		if (Input.GetKeyDown(",") && Time.time > nextFire & grounded)
		{
			nextFire = Time.time + fireRate;
			
			if(faceRight){
				Instantiate(rightShot, shotSpawn.position, shotSpawn.rotation);
				//print (shotSpawn.position);
				
			} else {
				
				Instantiate(leftShot, shotSpawn.position, shotSpawn.rotation);
			}
			//print (shotSpawn.position);
			//audio.Play ();
		}


		//Shield Code ===========

		if (grounded && Input.GetKey("/") && move ==0) {
			//if(!shielded){
			//Instantiate(tempShield, shieldSpawn.position, shieldSpawn.rotation);
			shield.SetActive(true);
			
			//}
		} else {
			
			shield.SetActive(false);
		}


		//Disarm Code============

		if (Input.GetKeyDown("/") && Time.time > nextFire && grounded && (Input.GetKey("left") || Input.GetKey("right")))
		{
			nextFire = Time.time + fireRate;
			
			if(faceRight){
				Instantiate(rightDisarmShot, shotSpawn.position, shotSpawn.rotation);
				//print (shotSpawn.position);
				
			} else {
				
				Instantiate(leftDisarmShot, shotSpawn.position, shotSpawn.rotation);
			}
			//print (shotSpawn.position);
			//audio.Play ();
		}











	}



void Flip(){

	
	faceRight = !faceRight;
	
	tempHitBox = backHitbox;
	backHitbox = frontHitbox;
	frontHitbox = tempHitBox;
	
	
	Vector3 theScale = transform.localScale;
	
	theScale.x *= -1;

	transform.localScale = theScale;
}

}
