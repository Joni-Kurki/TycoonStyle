using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlanetSnapshot_ChildPrefab_Script : MonoBehaviour {

    public GameObject _menuPrefab;
    bool _menuBarHasBeenInstantiated;
    bool _menuBarToggled;
    GameObject _refMenuBar;
    float _lastUiRefreshTime;
    const float UI_REFRESH_RATE = 0.1f;

    const int UI_SUB_MENU_SIZE = 32;
    const int UI_SUB_SPACING = 3;

    // Use this for initialization
    void Start () {
        _menuBarHasBeenInstantiated = false;
        _menuBarToggled = false;
        _lastUiRefreshTime = 0;
    }

    /// <summary>
    /// Instantes menu prefabs if it's first click. Then scales them accordingly. 
    /// </summary>
    public void PlanetClick() {
        if (!_menuBarHasBeenInstantiated) {
            _refMenuBar = Instantiate(_menuPrefab, transform);
            RectTransform rect = _refMenuBar.GetComponent<RectTransform>();
            //rect.localScale = new Vector3(1, 2, 1);
            rect.sizeDelta = new Vector2(UI_SUB_MENU_SIZE + UI_SUB_SPACING, (3 * UI_SUB_MENU_SIZE) + 3 * UI_SUB_SPACING);
            _menuBarHasBeenInstantiated = true;
            _menuBarToggled = true;
            ToggleMenuBar();
        }
        if(_lastUiRefreshTime < Time.time + UI_REFRESH_RATE) {
            ToggleMenuBar();
        }
    }

    /// <summary>
    /// Hides all other menus, and toggles clicked menu open
    /// </summary>
    void ToggleMenuBar() {
        GameObject[] goList = GameObject.FindGameObjectsWithTag("UI_Planet_Submenus");
        for(int i=0; i<goList.Length; i++) {
            goList[i].SetActive(false);
        }
        _refMenuBar.SetActive(true);
    }
}
