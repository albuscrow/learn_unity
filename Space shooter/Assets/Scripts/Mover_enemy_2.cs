using UnityEngine;
using System.Collections;

public class MoverEnemy2: MonoBehaviour {
    private float speed_z = -2;
    private float abs_max_speed_x = 15;
    private Rigidbody _rigidbody;
    private float a;
    private float v;

    void Start(){
        this._rigidbody = GetComponent<Rigidbody>();
        v = abs_max_speed_x;
    }

    void Update()
    {
        
        if (v >= abs_max_speed_x)
        {
            a = -0.2f;
        } else if (v <= -abs_max_speed_x)
        {
            a = 0.2f;
        }
        v += a;
        this._rigidbody.velocity = new Vector3(v,0,speed_z);
    }

}
