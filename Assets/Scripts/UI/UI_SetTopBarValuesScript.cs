using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetTopBarValuesScript : MonoBehaviour {

    const float REFRESH_INTERVAL = .2f;
    float _lastCheck;
    Text _topBarText;
    int _woodAmount = 0;
    public GameObject TopBarText;

    public bool AdminDebug = false;
    
	// Use this for initialization
	void Start () {
        _topBarText = TopBarText.GetComponent<Text>();
        _topBarText.text = _woodAmount + string.Empty;
        _lastCheck = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > _lastCheck + REFRESH_INTERVAL) {
            GetAllWoodResourceStructureValues();
            _lastCheck = Time.time;
        }
	}

    void GetAllWoodResourceStructureValues() {
        _woodAmount = 0;
        if (AdminDebug) {
            Debug.Log("Getting wood amounts " + Time.time);
        }
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Wood_Resource_Structure");
        foreach (GameObject g in goList) {
            Wood_Resource_Structure_Controller wrsc = g.GetComponent<Wood_Resource_Structure_Controller>();
            _woodAmount += wrsc.GetCurrentCapacity();
        }
        _topBarText.text = _woodAmount + string.Empty;
    }
}
