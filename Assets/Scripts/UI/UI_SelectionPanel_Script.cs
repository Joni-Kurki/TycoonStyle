using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectionPanel_Script : MonoBehaviour {

    // Holds all the planets we want to show at UI
    public GameObject [] _planets;
    public GameObject _planetChildPrefab;
    public Sprite [] _planetsSprite;

    const int UI_PLANET_SIZE = 64;
    const int UI_OFFSET_SIZE = 4;

	// Use this for initialization
	void Start () {
        _planets = GameObject.FindGameObjectsWithTag("Planet_Linkable");
        SetPlanetsToUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Drwaws planets to UI
    /// </summary>
    void SetPlanetsToUI() {
        GameObject refGo = null;
        for (int i = 0; i < _planets.Length; i++) {
            var go = Instantiate(_planetChildPrefab, transform);
            Planet_Script ps = _planets[i].GetComponent<Planet_Script>();
            ps.SetPlanetIndex(i);
            Image img = go.GetComponent<Image>();
            img.sprite = _planetsSprite[0];

            refGo = go;
        }

        RectTransform rect = refGo.transform.parent.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(UI_PLANET_SIZE * _planets.Length + UI_OFFSET_SIZE, UI_PLANET_SIZE + UI_OFFSET_SIZE);
    }

    private void OnMouseDown() {
        Debug.Log("Click" + this.name);
    }
}
