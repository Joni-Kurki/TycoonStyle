using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Storage_Strucure_Controller : MonoBehaviour {

	Wood_Storage_Structure _wood_storage_structure;
	public string _name;
	public int _currentResourceCount;

	// Use this for initialization
	void Start () {
		_name = "Wood Storage";
		_wood_storage_structure = new Wood_Storage_Structure (_name);
		_currentResourceCount = _wood_storage_structure.GetStorage ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
