using UnityEngine;
using System.Collections;

public class MoverEnemy1 : MonoBehaviour {
    private float speed = 5;

    void Start(){
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }

}
