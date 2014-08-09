using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//Public Variables
	public BoxCollider2D frontHitbox; //Our front hitbox
	public BoxCollider2D backHitbox;
	public BoxCollider2D tempHitBox = null;
	public BoxCollider2D crouchHitBox;
	

	private bool faceRight = true; //tells us we are facing right or not

	
	//There are several movement states. Rolling, Crouched, and Grounded
	
	bool rolling = false;
	bool crouched = false;
	bool down = false;
	bool hit = false;
	
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

	public float hitForceX;
	float realHitForceX;
	public float hitForceY;
	
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

	public GameObject disarmWand;
	//public GameObject disarm;
	//public GameObject beam;

	//public GameObject shield;
	//bool shielded = false;
	
	
	//public Transform shieldSpawn;
	public Transform shotSpawn;
	
	
	public float fireRate;
	private float nextFire;


	public float flyDist;

	float currentPosition;
	Transform hitPosition;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//anim.SetBool ("Shielded", false);
		anim.SetBool ("Down", false);
	}

	void FixedUpdate(){



		
		float move = Input.GetAxis ("Horizontal");



		//print (move);


		if (!down) {
						rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); 
				
			
			if (move > 0 && !faceRight) {
				Flip ();
			} else if (move < 0 && faceRight) {
				Flip();
			}


		
		} else if (down && grounded) {


			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);

		}




		Vector2 direction = new Vector2 (realHitForceX, hitForceY);



		if (hit && Mathf.Abs(currentPosition - transform.position.x) < flyDist ) {

		

						transform.Translate (direction * Time.fixedDeltaTime);



				} else {
			hit = false;
				}



		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround); //generates a circle at a position, with a radious
		//and tells you if there is a thing it collides with there. If yes, then true
		
		anim.SetBool ("Ground", grounded); //This tells the animator if we are on the ground or not

		anim.SetFloat("Speed", Mathf.Abs(move)); //Connects the animator paramater Speed to movement.
		//rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y); 

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);




		
	}


// Update is called once per frame
	void Update () {
				float move = Input.GetAxis ("Horizontal");


		//MOVEMENT CODE
		if(!down){

				//Jump Code =====================
				if (grounded && Input.GetKeyDown (".")) {
			
			
						//If we are grounded, and pressing the spacebar, add the jumpforce
						anim.SetBool ("Ground", false);
						rigidbody2D.AddForce (new Vector2 (0, jumpForce));
				}

				//Roll COde=========================
				if (grounded && Input.GetKey ("down") && Mathf.Abs (rigidbody2D.velocity.x) > 3) {



						rolling = true;
						anim.SetBool ("Roll", true);
						rigidbody2D.velocity = new Vector2 (move * maxSpeed * Time.deltaTime, rigidbody2D.velocity.y);
						crouchHitBox.enabled = true;
						frontHitbox.enabled = false;
						backHitbox.enabled = false;


				} else {
						rolling = false;
						anim.SetBool ("Roll", false);
						crouchHitBox.enabled = false;
						frontHitbox.enabled = true;
						backHitbox.enabled = true;
			
				}

				//Crouch code=========================
				if (grounded && Input.GetKey ("down") && Mathf.Abs (rigidbody2D.velocity.x) <= 3) {

						rigidbody2D.drag += 2;
						rigidbody2D.velocity = new Vector2 (move * maxCrouchSpeed * Time.deltaTime, rigidbody2D.velocity.y);
						crouched = true;
						anim.SetBool ("Crouch", true);

						crouchHitBox.enabled = true;
						frontHitbox.enabled = false;
						backHitbox.enabled = false;

			
				} else {
						rigidbody2D.drag = 0;
						crouched = false;
						anim.SetBool ("Crouch", false);

						crouchHitBox.enabled = false;
						frontHitbox.enabled = true;
						backHitbox.enabled = true;

			
				}

		}





		//MAGIC CODE

		if(!down && holdingWand){

				//Bolt shot code=====================
				if (Input.GetKeyDown (",") && Time.time > nextFire && grounded) {
						nextFire = Time.time + fireRate;
			
						if (faceRight) {
								Instantiate (rightShot, shotSpawn.position, shotSpawn.rotation);
								//print (shotSpawn.position);
				
						} else {
				
								Instantiate (leftShot, shotSpawn.position, shotSpawn.rotation);
						}
						//print (shotSpawn.position);
						//audio.Play ();
				}





				//Disarm Code============

				if (Input.GetKeyDown ("/") && Time.time > nextFire && grounded) {
						nextFire = Time.time + fireRate;
			
						if (faceRight) {
								Instantiate (rightDisarmShot, shotSpawn.position, shotSpawn.rotation);
								//print (shotSpawn.position);
				
						} else {
				
								Instantiate (leftDisarmShot, shotSpawn.position, shotSpawn.rotation);
						}
						//print (shotSpawn.position);
						//audio.Play ();
				}


				//Wand movement code
				if (Mathf.Abs (rigidbody2D.velocity.x) <= 3 && (!crouched || !rolling)) {
						
						if (wandpos < 1 && Input.GetKeyDown ("up")) {



								wandpos = wandpos + 1;
								wand.transform.position = new Vector3 ((wand.transform.position.x), (wand.transform.position.y + 0.7f), 0);
								anim.SetInteger ("WandIdle", wandpos);

						}
						if (wandpos > -1 && Input.GetKeyDown ("down")) {


								wandpos = wandpos - 1;
								wand.transform.position = new Vector3 (wand.transform.position.x, wand.transform.position.y - 0.7f, 0);
								anim.SetInteger ("WandIdle", wandpos);		
			
						}
				}
	

				//Hit by something code


				//downed


		}

		if (down && Input.GetKeyDown("up") || down && Input.GetKeyDown(".")) {
			down = false;
			hit = false;
			anim.SetBool("Down", false);
			frontHitbox.enabled = true;
			backHitbox.enabled = true;
			crouchHitBox.enabled = false;

			
		}
	}
	

	void OnTriggerEnter2D(){

		}



	void OnCollisionEnter2D(Collision2D other)
	{
				
		if (other.collider.gameObject.tag == "ShotL" || other.collider.gameObject.tag == "ShotR") {


						frontHitbox.enabled = false;
						backHitbox.enabled = false;
						crouchHitBox.enabled = false;

			currentPosition = gameObject.transform.position.x;

						down = true;
						anim.SetBool ("Down", true);
						hit = true;


			if(other.collider.gameObject.tag == "ShotL"){

				realHitForceX = -hitForceX;

			} else if (other.collider.gameObject.tag == "ShotR"){

				realHitForceX = hitForceX;
			}


						Destroy (other.gameObject);
				}


		if (other.collider.gameObject.tag == "DisarmShot") {

			if(holdingWand){
			holdingWand = false;

			wand.SetActive(false);

			print ("DISARM HIT!");
			
				
			Instantiate (disarmWand, shotSpawn.position, shotSpawn.rotation);

			}

			Destroy(other.gameObject);


		}
	
	}




	void OnTriggerStay2D(Collider2D other){

		if (other.tag == "Wand") {

			if(!holdingWand && Input.GetKeyDown("down")){
				print ("This was called");

				holdingWand = true;

				Destroy(other.gameObject);

				wand.SetActive (true);


			}



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
