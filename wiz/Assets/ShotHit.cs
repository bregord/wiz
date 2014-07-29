using UnityEngine;
using System.Collections;

public class ShotHit : MonoBehaviour {

	/*

	void OnCollisionEnter2D(Collider2D other)
	{
		if (other.tag == "Shot") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}


		if (other.tag == "Player1" ||other.tag == "Player2" ) {

			print ("Collision detected");
			Destroy (gameObject);
		}
	}
	*/


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


}
