using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure_Linker_Script : MonoBehaviour {
	Structure _structureBase;
	public Wood_Resource_Structure_Controller _resController;
	public Wood_Production_Structure_Controller _prodController;
	bool isConnected = false;
    public bool AdminDebug = false;

    // Use this for initialization
    void Start () {
		_structureBase = new Structure();
		_structureBase._structureName = "Linker";
	}
	
	// Update is called once per frame
	void Update () {
		if (isConnected) {
			LinkWoodResourceAndProduction ();
		} else {
			GetAllWoods ();
		}
	}

	public void GetAllWoods(){
		GameObject[] list = GameObject.FindGameObjectsWithTag ("Woods_Resource_Structure"); 
		GameObject[] list2 = GameObject.FindGameObjectsWithTag ("Woods_Production_Structure");
		bool foundOpenRes = false;
		bool foundOpenProd = false;

		for (int i = 0; i < list.Length; i++) {
			_resController = list [i].GetComponent<Wood_Resource_Structure_Controller> ();
			if (!_resController._isConnected) {
				_resController.StartConnecting ();
				foundOpenRes = true;
				break;
			}
		}

		for (int i = 0; i < list2.Length; i++) {
			_prodController = list2 [i].GetComponent<Wood_Production_Structure_Controller> ();
			if (!_prodController._isConnected) {
				_prodController.StartConnecting ();
				foundOpenProd = true;
				break;
			}
		}

		if (foundOpenProd && foundOpenRes) {
			_resController._isConnected = true;
			_prodController._isConnected = true;
			isConnected = true;
		}
	}

	private void LinkWoodResourceAndProduction(){
        if (AdminDebug) {
            Debug.DrawLine(_resController.transform.position, _prodController.transform.position, Color.red, 1);
        }
	}

}
