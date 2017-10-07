using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetTopBarValuesScript : MonoBehaviour {

    const float REFRESH_INTERVAL = .2f;
    float _lastCheck;
    Text _woodTopBarText;
    int _woodAmount = 0;
    public GameObject TopBar;

    public bool AdminDebug = false;
    
	// Use this for initialization
	void Start () {
        // Gets component from child.
        _woodTopBarText = TopBar.GetComponentInChildren<Text>();
        _woodTopBarText.text = _woodAmount + string.Empty;
        _lastCheck = 0;
        setTopBarLocationAndScale();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > _lastCheck + REFRESH_INTERVAL) {
            GetAllWoodResourceStructureValues();
            _lastCheck = Time.time;
        }
	}
     
    /// <summary>
    /// Gets wood values from all "Wood_Resource_Structure" and set the woodTopBarText.
    /// </summary>
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
        _woodTopBarText.text = _woodAmount + string.Empty;
    }

    /// <summary>
    /// This is dynamically supposed to scale topbar ui element depending on the resolution.
    /// TODO: make it work dynamically, now it only has fixed value.
    /// like, get windows res .. get image res .. wRes / iRes -> scaling factor
    /// </summary>
    void setTopBarLocationAndScale() {
        try {
            Image topBarImage = TopBar.GetComponent<Image>();
            topBarImage.rectTransform.localScale = new Vector3(2.5f, 1, 1);
            if (AdminDebug) {
                Debug.Log("Succesfully loaded image! " + topBarImage.name);
            }
        } catch (Exception e) {
            if (AdminDebug) {
                Debug.Log(e.Message);
            }
        }
    }
}
