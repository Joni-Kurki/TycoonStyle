using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_VehicleSelectButton_Script : MonoBehaviour {

    GameObject _gameObjectReference;
    int _index;
    Vehicle_Controller_Script _vehicleControllerReference;
    Text _childText;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetGameObjectReference(GameObject reference) {
        _gameObjectReference = reference;
        if(_gameObjectReference != null) {
            Debug.Log("Referenced succesfully!");
        }
    }

    public void SetIndexAndTexts(int value) {
        _index = value;
        // Let's get parent script
        UI_UnitSelectionPanel parentUI = transform.parent.gameObject.GetComponent<UI_UnitSelectionPanel>();
        // Let's get the vehicle go by index;
        GameObject refGo = parentUI.FindVehicleByIndex(_index);
        _vehicleControllerReference = refGo.GetComponent<Vehicle_Controller_Script>();
        _childText = transform.GetChild(0).GetComponent<Text>();
        _childText.text = _vehicleControllerReference.name;
    }

    public void ThisClick() {
        _vehicleControllerReference.VehicleToggleButton_Click();
    }
}
