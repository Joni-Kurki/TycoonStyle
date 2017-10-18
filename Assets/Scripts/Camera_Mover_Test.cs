using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mover_Test : MonoBehaviour {

    Rigidbody _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            Debug.Log("W");
            _rb.AddForce(Vector3.left);
        }
    }
}
