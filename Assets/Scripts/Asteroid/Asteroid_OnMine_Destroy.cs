﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_OnMine_Destroy : MonoBehaviour {

    private bool _pickedUp;
	// Use this for initialization
	void Start () {
        _pickedUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_pickedUp) {
            Asteroid_Pooler_Script a = transform.parent.GetComponent<Asteroid_Pooler_Script>();
            a._currentAmountOfAsteroids--;
            Destroy(gameObject);
        }
	}

    public void PickUp() {
        _pickedUp = true;
    }
}
