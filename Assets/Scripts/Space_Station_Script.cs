using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Station_Script : MonoBehaviour {

    public GameObject _linkedPlanet;
    public List<GameObject> _linkedVehicles;
    const int MAX_CONNECTED_SHIPS = 3;

    const int MAX_PASSANGERS = 1000;
    const float PASSANGER_ARRIVE_RATE = 2.5f;
    public int _currentPassangerCount;
    float _lastPassangerCheck;


	// Use this for initialization
	void Start () {
        _linkedPlanet = FindClosestPlanet();
        _linkedVehicles = new List<GameObject>();
        _currentPassangerCount = 0;
        _lastPassangerCheck = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // If stations doesnt have max passangers.
		if(_currentPassangerCount < MAX_PASSANGERS) {
            if(_lastPassangerCheck < Time.time + PASSANGER_ARRIVE_RATE) {
                _currentPassangerCount++;
            }
        }
	}

    /// <summary>
    /// Returns closest Gameobject with tag "Planet_Linkable"
    /// </summary>
    /// <returns></returns>
    private GameObject FindClosestPlanet() {
        GameObject[] planetList = GameObject.FindGameObjectsWithTag("Planet_Linkable");
        GameObject closestPlanet = null;

        float closestDistance = Mathf.Infinity;

        foreach(GameObject go in planetList) { 
            if(Vector3.Distance(transform.position, go.transform.position) <= closestDistance ){
                closestDistance = Vector3.Distance(transform.position, go.transform.position);
                Debug.Log(Vector3.Distance(transform.position, go.transform.position) + " <" + closestDistance);
                Debug.Log("Linked to " + go.name);
                closestPlanet = go;
            }
        }
        return closestPlanet;
    }

    public void TakePassangers(int value) {
        if (_currentPassangerCount < 0) {
            _currentPassangerCount = 0;
        } else {
            _currentPassangerCount -= value;
        }
    }

    public void Unload_Passangers(int number) {
        Planet_Script ps = _linkedPlanet.GetComponent<Planet_Script>();
        ps.RewardCurrencyFromMining(number);
    }

    /// <summary>
    /// Links gameobject to this station. Has max number of linked items.
    /// </summary>
    /// <param name="go"></param>
    public void Space_Station_Link_Vehicle(GameObject go) {
        if (_linkedVehicles.Count-1 < MAX_CONNECTED_SHIPS) {
            _linkedVehicles.Add(go);
        }
    }

    /// <summary>
    /// Unloads vehicles cargo here and this station sents it to linked planet.
    /// </summary>
    /// <param name="value"></param>
    public void Unload_Cargo(float value) {
        Planet_Script ps = _linkedPlanet.GetComponent<Planet_Script>();
        ps.RewardCurrencyFromMining(value);
    }
}
