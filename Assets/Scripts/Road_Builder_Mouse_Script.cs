using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Builder_Mouse_Script : MonoBehaviour {

	Road_Controller road_controller;
	public GameObject roadContainerPrefab;
	bool _toggleStart;
	bool _toggleEnd;
	bool _canBuildRoad;
	bool _hasBuilt;
	Vector3 _togglePointStart;
	Vector3 _togglePointEnd;

	// Use this for initialization
	void Start () {
		//road_controller = GetComponent<Road_Controller> ();
		_toggleStart = false;
		_toggleEnd = false;
		_canBuildRoad = false;
		_hasBuilt = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			// jos osutaan johonki gameobjektiin
			if (hit) {
				Debug.Log ("Hit " + hitInfo.transform.gameObject.name);

				if (hitInfo.transform.gameObject.name == "Plane") {
					//Debug.Log (">>> >> > " + transform.gameObject.name);
					if (!_toggleStart && !_toggleEnd) {
						Debug.Log("Toggled Start!");
						_togglePointStart = hitInfo.point;
						_toggleStart = true;
					} else if (_toggleStart && !_toggleEnd) {
						Debug.Log("Toggled End!");
						_togglePointEnd = hitInfo.point;
						_toggleEnd = true;
					}
				}
			}
		}
		if (_toggleStart && _toggleEnd && !_hasBuilt) {
			Debug.Log("Both toggled!");
			_canBuildRoad = true;
		}
		if (_canBuildRoad && !_hasBuilt) {
			var instantiatedRoad = Instantiate (roadContainerPrefab, transform.position, roadContainerPrefab.transform.rotation, transform.parent);
			road_controller = instantiatedRoad.GetComponent<Road_Controller> ();
			road_controller.SetPoints (_togglePointStart, _togglePointEnd);
			_hasBuilt = true;
		}
	}
}
