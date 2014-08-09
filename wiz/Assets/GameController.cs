using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	bool player1Alive = true; //player1 has advantage
	public GameObject player1;

	bool player2Alive = false; //player 2 has advantage
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (player1Alive && player2Alive) {// stay still.


				} else if (!player1Alive && player2Alive) {// Follow player 2





				} else if (player1Alive && !player2Alive) {// Follow player 1

			gameObject.transform.position = new Vector2(player1.transform.position.x, gameObject.transform.position.y); 


				} else {//Both players are dead, stay still


				}





	}
}
