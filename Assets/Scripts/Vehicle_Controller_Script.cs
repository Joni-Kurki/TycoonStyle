using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Controller_Script : MonoBehaviour {

    SpaceMiner_Vehicle _SpaceMiner_vehicle;
    public int _storageNumber;
    public int _weaponNumber;
    public int _toolNumber;

    // Use this for initialization
    void Start () {
        _SpaceMiner_vehicle = new SpaceMiner_Vehicle(2, 0, 1);
        _storageNumber = _SpaceMiner_vehicle._numberOfStorageModules;
        _weaponNumber = _SpaceMiner_vehicle._numberOfWeaponModules;
        _toolNumber = _SpaceMiner_vehicle._numberOfToolModules;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
