using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectionPanel_Script : MonoBehaviour {

    // Grabs all the planets we want to show at UI
    public GameObject [] _planets;
    public GameObject _planetChildPrefab;
    public Sprite [] _planetsSprite;

	// Use this for initialization
	void Start () {
        _planets = GameObject.FindGameObjectsWithTag("Planet_Linkable");
        SetPlanetsToUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetPlanetsToUI() {

        for (int i = 0; i < _planets.Length; i++) {
            var go = Instantiate(_planetChildPrefab, transform);
            Image img = go.GetComponent<Image>();
            img.sprite = _planetsSprite[0];
        }
    }

    public void TestClick() {

    }

    private void OnMouseDown() {
        Debug.Log("Click" + this.name);
    }
}
