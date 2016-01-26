using UnityEngine;
using System.Collections;

public class BoosBehavior : MonoBehaviour {

    private float speed_z = 0.5f;
    private float speed_x = 2;
    private Rigidbody _rigidbody;
    private float current_speed_z;
    private float current_speed_x;
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameController gameController;
    public Transform spawLeft;
    public Transform spawRight;
    public GameObject bolt;
    private float fireNext = 0;

    void Start(){
        this._rigidbody = GetComponent<Rigidbody>();
        current_speed_z = speed_z;
        current_speed_x = speed_x;

		GameObject gameConrollerObject = GameObject.FindWithTag ("GameController");
		if (gameConrollerObject != null) {
			gameController = gameConrollerObject.GetComponent<GameController> ();
		}

		if (gameController == null) {
			Debug.Log ("Connot find 'GameController' script");
		}
    }

    void Update()
    {
        
        if (transform.position.z < 10) {
            current_speed_z = speed_z;
        } else if (transform.position.z > 14) {
            current_speed_z = -speed_z;
        }

        if (transform.position.x > 5)
        {
            current_speed_x = -speed_x;
        } else if (transform.position.x < -5)
        {
            current_speed_x = speed_x;
        }

        this._rigidbody.velocity = new Vector3(current_speed_x,0,current_speed_z);

        if (Time.time > fireNext)
        {
            fireNext = Time.time + 3;
			Instantiate (bolt, spawLeft.position, spawLeft.rotation);
			Instantiate (bolt, spawRight.position, spawRight.rotation);
			GetComponent<AudioSource> ().Play ();
        }
    }

    private int left = 20;
	void OnTriggerEnter(Collider other) {
		if (other.tag == "boundary" || other.tag == "enemy" || other.tag == "bossBolt") {
			return;
		}

		//handle boss
        if (--left < 0)
        {
		Instantiate (explosion, transform.position, transform.rotation);
		Instantiate (explosion, transform.position + new Vector3(1,0,1), transform.rotation);
		Instantiate (explosion, transform.position + new Vector3(-1,0,1), transform.rotation);
		Instantiate (explosion, transform.position + new Vector3(1,0,-1), transform.rotation);
		Instantiate (explosion, transform.position + new Vector3(-1,0,-1), transform.rotation);
		Destroy (gameObject);
		gameController.AddScore (100);
            gameController.BossOver();
        }
        else
        {
		Instantiate (explosion, other.transform.position, other.transform.rotation);
        }
		
		//destory other
		Destroy (other.gameObject);
		//if other is player, show playerExplosion;
		if (other.tag == "player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
	}
}
