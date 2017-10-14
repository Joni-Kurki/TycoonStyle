using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Controller_Script : MonoBehaviour {

    Vehicle _vehicle;
    War_Vehicle _War_vehicle;
    SpaceMiner_Vehicle _SpaceMiner_vehicle;
    public int _storageNumber;
    public int _weaponNumber;
    public int _toolNumber;
    public int _vehicleType;
    public string _vehicleName;

    // Use this for initialization
    void Start () {
        _vehicle = new Vehicle();
        switch (_vehicleType) {
            case 1:
                _vehicleName = "War Vehicle";
                _War_vehicle = new War_Vehicle(1, 2, 0);
                _storageNumber = _War_vehicle._numberOfStorageModules;
                _weaponNumber = _War_vehicle._numberOfWeaponModules;
                _toolNumber = _War_vehicle._numberOfToolModules;
                break;
            case 2:
                _vehicleName = "Miner Vehicle";
                _SpaceMiner_vehicle = new SpaceMiner_Vehicle(2, 0, 1);
                _storageNumber = _SpaceMiner_vehicle._numberOfStorageModules;
                _weaponNumber = _SpaceMiner_vehicle._numberOfWeaponModules;
                _toolNumber = _SpaceMiner_vehicle._numberOfToolModules;
                break;
        }
        
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
