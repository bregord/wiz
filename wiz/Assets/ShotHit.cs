using UnityEngine;
using System.Collections;

public class ShotHit : MonoBehaviour {

	public float hitForceX;
	public float hitForceY;


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.gameObject.tag == "Shot" || other.collider.gameObject.tag == "DisarmShot") {
			Destroy (other.gameObject);
			Destroy (gameObject);

			print ("SHOTS HIT");
		}

		/*
		if (other.collider.gameObject.tag == "Player1" ||other.collider.gameObject.tag == "Player2" ) {


			Vector2 direction = new Vector2 (hitForceX, hitForceY);

			//other.rigidbody.AddForce (direction, ForceMode2D.Impulse);

			other.rigidbody.velocity = direction;

			print ("Collision detected");
			Destroy (gameObject);



			//Destroy (other.gameObject);
		}

		/*
		if(other.gameObject.name == "Right Shot" || other.gameObject.name == "LeftShot L" || other.gameObject.name == "testSprite"){
			Destroy(this.gameObject);
			//Destroy (other.gameObject);

		}
*/



	}





	/*
		void OnTriggerEnter2D(Collider2D other)
		{
			
			
		if (other.tag == "Shot") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
		
		
		if (other.tag == "Player1" || other.tag == "Player2" ) {
			
			print ("Collision detected");
			Destroy (gameObject);

		}

	}

*/
	
}
