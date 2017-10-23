using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet_Script : MonoBehaviour {

    public float _rewarededCurrencyFromMining;
    public GameObject _uiStatsPanelPrefab;
    bool _uiToggled;
    bool _uiInitiated;
    public int _planetIndex;

	// Use this for initialization
	void Start () {
        _rewarededCurrencyFromMining = 0;
        _uiToggled = false;
        _uiInitiated = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        // If we have not Instantiated panel. Do it once. After that enable / disable it by click
        if (!_uiInitiated) {
            Planet_ShowUIPanel();
            _uiInitiated = true;
        }
        if (_uiToggled) {
            transform.GetChild(0).gameObject.SetActive(true);
        } else {
            transform.GetChild(0).gameObject.SetActive(false);
        }
	}

    private void OnMouseDown() {
        //Debug.Log("Clicked " + transform.name);
        _uiToggled = !_uiToggled;
    }

    // We get currency from space station to planet instance
    public void RewardCurrencyFromMining(float value) {
        _rewarededCurrencyFromMining += value;
    }

    // Instantiate UI panel. Also links UI panel to Child of planetPrefab.UI_Panel_Container
    private void Planet_ShowUIPanel() {
        var instantiatedUIPanelContainer = Instantiate(_uiStatsPanelPrefab, transform.position, _uiStatsPanelPrefab.transform.rotation, transform);
        instantiatedUIPanelContainer.transform.parent = instantiatedUIPanelContainer.transform.parent.GetChild(0);
        instantiatedUIPanelContainer.transform.localScale = transform.parent.localScale / 5; 
        Vector3 offset = new Vector3(0, (instantiatedUIPanelContainer.transform.parent.parent.localScale.y / 2) + (transform.localScale.y / 8), 0);
        instantiatedUIPanelContainer.transform.Translate(transform.parent.position + offset);
    }

    public void SetPlanetIndex(int index) {
        _planetIndex = index;
    }
}
