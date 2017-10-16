using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UnitSelectionPanel : MonoBehaviour {

    public GameObject uiPrefab;

	// Use this for initialization
	void Start () {
        InstantiateTexts();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    GameObject[] FindVehicles() {
        return GameObject.FindGameObjectsWithTag("Vehicle");
    }

    public GameObject FindVehicleByIndex(int index) {
        return FindVehicles()[index];
    }

    void InstantiateTexts() {
        for(int i=0; i< FindVehicles().Length; i++) {
            GameObject go = Instantiate(uiPrefab, transform);
            go.name = "ReferencedUIButton " + i;
            UI_VehicleSelectButton_Script uiButtonReference = go.GetComponent<UI_VehicleSelectButton_Script>();
            uiButtonReference.SetGameObjectReference(go);
            uiButtonReference.SetIndexAndTexts(i);
        }
    }
}
