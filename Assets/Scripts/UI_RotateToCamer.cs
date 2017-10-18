using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RotateToCamer : MonoBehaviour {

    Transform _mainCameraTransform;

	// Use this for initialization
	void Start () {
        _mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Debug.Log("Camera @ "+ _mainCameraTransform.position);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(_mainCameraTransform);
        
        //Debug.DrawRay(transform.position, _mainCameraTransform.position, Color.yellow, 10);
	}
}
