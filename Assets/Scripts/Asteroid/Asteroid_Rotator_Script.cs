using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Rotator_Script : MonoBehaviour {

    float _rotationSpeed;
    bool _isInitiated;
    Rigidbody _rb;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        _isInitiated = false;
        _rotationSpeed = Random.Range(0.01f, 3f);
    }

    private void FixedUpdate() {
        if (!_isInitiated) {
            _rb.angularVelocity = Random.insideUnitSphere * _rotationSpeed;
            _isInitiated = true;
        }
    }
}
