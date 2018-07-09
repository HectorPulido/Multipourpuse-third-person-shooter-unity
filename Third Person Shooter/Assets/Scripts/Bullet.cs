using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed;
	
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);	
	}
	
}
