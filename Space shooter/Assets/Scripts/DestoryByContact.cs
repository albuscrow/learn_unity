using UnityEngine;
using System.Collections;

public class DestoryByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start() {
		GameObject gameConrollerObject = GameObject.FindWithTag ("GameController");
		if (gameConrollerObject != null) {
			gameController = gameConrollerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Connot find 'GameController' script");
		}
			
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "boundary" || other.tag == "enemy") {
			return;
		}
		//destory astroid
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
		
		//destory other
		Destroy (other.gameObject);
		//if other is player, show playerExplosion;
		if (other.tag == "player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		gameController.AddScore (scoreValue);
	}
}
