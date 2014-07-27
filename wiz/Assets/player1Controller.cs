using UnityEngine;
using System.Collections;

public class player1Controller : MonoBehaviour {
	
	/*
	public BoxCollider2D hitbox;

	public float maxSpeed;
	private bool faceRight = true;

	Animator anim;

	bool grounded = false;

	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public float jumpForce = 10f;


	public int wandpos = 0;
	public GameObject wand;
*/
	public BoxCollider2D frontHitbox; //Our front hitbox
	public BoxCollider2D backHitbox;
	
	
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
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	
	
	//max speed the player can run at and their jump height
	public float maxSpeed;
	public float jumpForce;
	
	//The wand stuff goes here
	public int wandpos = 0; //Low is -1, mid is 0, high is 1;
	//There are other options if you are up in the air
	public bool holdingWand = true; //You start he game with a wand. If you drop it, this becomes false
	public GameObject wand;
	
	
	//shoot spells
	public GameObject shot;
	//public GameObject disarm;
	//public GameObject beam;
	//public GameObject shield;
	
	
	public Transform shotSpawn;
	
	public float fireRate;
	private float nextFire;
	
	
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround); //generates a circle at a position, with a radious
		//and tells you if there is a thing it collides with there. If yes, then true
		
		anim.SetBool ("Ground", grounded); //This tells the animator if we are on the ground or not
		
		//anim.SetBool ("Roll", false);
		
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(move)); //Connects the animator paramater Speed to movement.
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); 
		
		
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		//
		
		
		//This flips us around if we turn left. Eliminates the need for extra animations
		if (move > 0 && !faceRight) {
			Flip ();
		} else if (move < 0 && faceRight) {
			Flip();
		}
		
	}
	
	void Update(){
		//anim.SetBool ("Roll", false);
		
		float move = Input.GetAxis ("Horizontal");
		
		
		if (grounded &&  rigidbody2D.velocity.x < 5 &&(Input.GetKeyDown ("down") && wandpos != -1)){
			
			wandpos = wandpos -1;
			wandMove(wandpos);
		}
		
		if (grounded &&  rigidbody2D.velocity.x < 5 &&(Input.GetKeyDown ("up") && wandpos != 1)){
			wandpos = wandpos +1;
			wandMove(wandpos);
			
		}
		
		
		if (grounded && Input.GetKeyDown ("space")) {
			
			
			//If we are grounded, and pressing the spacebar, add the jumpforce
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
		
		
		if (grounded && Input.GetKeyDown ("down")) {
			
			anim.SetBool ("Roll", true);
			rigidbody2D.velocity = new Vector2 (move * maxSpeed * Time.deltaTime, rigidbody2D.velocity.y); 
			frontHitbox.size = new Vector2(0.5f, 0.5f);
			
			
		} else if (grounded && !Input.GetKeyDown ("down")) {
			anim.SetBool ("Roll", false);		
			frontHitbox.size = new Vector2(0.7f, 1.0f);
		}
		
		
		if (Input.GetButton("Fire") && Time.time > nextFire && move >= 0)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//audio.Play ();
		}
		
	}
	
	
	void wandMove (int pos){
		
		switch(wandpos){
			
		case 1:
			
			wand.transform.position = new Vector2 (wand.transform.position.x, wand.transform.position.y + 1);
			return;
		case 0:
			
			wand.transform.position = new Vector2 (wand.transform.position.x, wand.transform.position.y + 0);
			return;
		case -1:
			
			wand.transform.position = new Vector2 (wand.transform.position.x, wand.transform.position.y - 1);
			return;
			
		}
		
		
	}
	
	//The flip method
	void Flip(){
		faceRight = !faceRight;
		
		Vector3 theScale = transform.localScale;
		
		theScale.x *= -1;
		
		transform.localScale = theScale;
	}
}
