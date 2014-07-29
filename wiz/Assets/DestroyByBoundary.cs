using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "Player1" && other.tag != "Player2") {
						Destroy (other.gameObject);
				}
	}
}