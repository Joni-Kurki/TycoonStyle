using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woods_Production_Structure_Controller : MonoBehaviour {

	Wood_Production_Structure wood_production_structure;
	public string _name;
	public int _currentResourceCount;
	private float _lastCheck;
	public bool _isConnectingInProgress;
	public bool _isConnected;

	// Use this for initialization
	void Start () {
		wood_production_structure = new Wood_Production_Structure ("Test: Wood Production");
		_name = wood_production_structure._structureName;
		_lastCheck = 0;
		_isConnected = false;
	}

	// Update is called once per frame
	void Update () {
		_currentResourceCount = wood_production_structure.GetCurrentResources ();
	}

	public void StartConnecting(){
		_isConnectingInProgress = true;
	}

	void OnMouseDown(){
		_isConnectingInProgress = true;
		Debug.Log ("Mouse! and "+ _isConnectingInProgress  + this.name);
	}
}
