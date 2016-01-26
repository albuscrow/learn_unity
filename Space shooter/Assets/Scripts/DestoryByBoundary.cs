using UnityEngine;
using System.Collections;

public class DestoryByBoundary : MonoBehaviour {
	void OnTriggerExit(Collider other) {
        if (other.transform.position.z > 15 || other.transform.position.z < -5)
        {
    		Destroy (other.gameObject);
        }
	}
}
