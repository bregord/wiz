using UnityEngine;
using System.Collections;

public class wandDisarmed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		gameObject.rigidbody2D.AddForce (new Vector2 (Random.Range (-10.0F, 10.0F), Random.Range (30.0f, 60.0F)), ForceMode2D.Impulse);
		gameObject.rigidbody2D.AddTorque(100);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){



	}
}
