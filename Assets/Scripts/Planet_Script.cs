using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet_Script : MonoBehaviour {

    public float _rewarededCurrencyFromMining;

	// Use this for initialization
	void Start () {
        _rewarededCurrencyFromMining = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown() {
        Debug.Log("Clicked " + transform.name);
    }

    public void RewardCurrencyFromMining(float value) {
        _rewarededCurrencyFromMining += value;
    }
}
