using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woods_Resource_Structure_Controller : MonoBehaviour {

	Wood_Resource_Structure wood_resource_structure;
	public string _name;
	public int _currentResourceCount;
	private float _lastCheck;
	public bool _isConnectingInProgress;
	public bool _isConnected;

	// Use this for initialization
	void Start () {
		wood_resource_structure = new Wood_Resource_Structure ("Test: Wood Resource");
		_name = wood_resource_structure._structureName;
		_currentResourceCount = wood_resource_structure.GetCurrentCapacity ();
		_lastCheck = 0;
		_isConnectingInProgress = false;
		_isConnected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > _lastCheck + wood_resource_structure.GetResourceInterval () ) {
			wood_resource_structure.AddResourceTick ();
			_lastCheck = Time.time;
		} 
		_currentResourceCount = wood_resource_structure.GetCurrentCapacity ();
	}

	public void StartConnecting(){
		_isConnectingInProgress = true;
	}

	void OnMouseDown(){
		_isConnectingInProgress = true;
		Debug.Log ("Mouse! and "+ _isConnectingInProgress  + this.name);
	}
}
