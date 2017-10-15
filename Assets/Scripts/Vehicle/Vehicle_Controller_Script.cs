using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Controller_Script : MonoBehaviour {

    // Vehicle class and subclasses. TODO: needs to be some smarter system here.
    Vehicle _vehicle;
    War_Vehicle _War_vehicle;
    SpaceMiner_Vehicle _SpaceMiner_vehicle;
    Vehicle_Module_List _vehicle_Module_List;

    // Number of modules
    public int _storageNumber;
    public int _weaponNumber;
    public int _toolNumber;
    public int _vehicleType;
    public string _vehicleName;
    public float _vehicleSpeed;
    int _currentStorageModule;

    // Some distance variables
    GameObject _closestGameObject;
    float _closestDist;
    Rigidbody _rb;

    // Variables that have something to do with mining
    public Vector3 _baseLocation;
    float _miningPickUpDuration;
    float _miningUnloadDuration;
    float _lastTimerTrigger;
    
    // Booleans for controlling phases
    bool _isMoving;
    bool _hasTarget;
    bool _hasLoad;
    bool _isReturning;

    // ForDebugging
    bool _debugSelect = false;

    public string _storageModuleStr;
    // Use this for initialization
    void Start () {
        _baseLocation = transform.position;
        // Let's set vehicle stuff here
        _vehicle = new Vehicle();
        switch (_vehicleType) {
            case 1:
                _vehicleName = "War Vehicle";
                _War_vehicle = new War_Vehicle(1, 2, 0);
                _storageNumber = _War_vehicle._numberOfStorageModules;
                _weaponNumber = _War_vehicle._numberOfWeaponModules;
                _toolNumber = _War_vehicle._numberOfToolModules;
                _vehicleSpeed = .4f;
                break;
            case 2:
                _vehicleName = "Miner Vehicle";
                _SpaceMiner_vehicle = new SpaceMiner_Vehicle(2, 0, 1);
                _storageNumber = _SpaceMiner_vehicle._numberOfStorageModules;
                _weaponNumber = _SpaceMiner_vehicle._numberOfWeaponModules;
                _toolNumber = _SpaceMiner_vehicle._numberOfToolModules;

                _vehicle_Module_List = new Vehicle_Module_List();
                Storage_Vehicle_Module mod = new Storage_Vehicle_Module("Cargo module", 3);
                _vehicle_Module_List.AddStorageModule(mod);
                mod = new Storage_Vehicle_Module("Cargo module 2", 5);
                _currentStorageModule = 0;

                _vehicle_Module_List.AddStorageModule(mod);
                _vehicleSpeed = 3.3f;
                _miningPickUpDuration = 3f;
                _miningUnloadDuration = 2f;
                _lastTimerTrigger = Time.time;
                break;
        }

        _rb = GetComponent<Rigidbody>();

        _isMoving = false;
        _hasTarget = false;
        _hasLoad = false;
        _isReturning = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > _lastTimerTrigger) {
            if (!_hasTarget && !_hasLoad) {
                FindTarget();
            } else if (_hasTarget) {
                MoveToTarget();
                Debug.DrawLine(this.transform.position, _closestGameObject.transform.position, Color.red, 1);
            } else if (!_hasTarget && _hasLoad) {
                ReturnHomeBase();
            }
        }
        PasteCargoToDebug();

        if (_debugSelect) {
            Vector3 v1 = new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z - .1f);
            Vector3 v2 = new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z + .1f);
            Vector3 v3 = new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z + .1f);
            Vector3 v4 = new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z - .1f);
            Debug.DrawLine(v1, v2, Color.red, .1f);
            Debug.DrawLine(v2, v3, Color.red, .1f);
            Debug.DrawLine(v3, v4, Color.red, .1f);
            Debug.DrawLine(v4, v1, Color.red, .1f);
        }
    }

    private void PasteCargoToDebug() {
        for (int i = 0; i < _vehicle_Module_List._storageModuleList.Count; i++) {
            Debug.Log("Cargo["+i+"] "+_vehicle_Module_List._storageModuleList[i].GetCurrentCargoWeight() + "/" + _vehicle_Module_List._storageModuleList[i].GetMaxCargoWeight());
        }
    }

    /// <summary>
    /// Finds the closest mineable target and checks if it is tagged, if so finds next one non tagged.
    /// </summary>
    private void FindTarget() {
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Asteroid_Mineable");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject go in goList) {
            if(Vector3.Distance(this.transform.position, go.transform.position) < closestDistance) {
                Asteroid_Tag_Script at = go.GetComponent<Asteroid_Tag_Script>();
                if (!at._asteroidIsTagged) {
                    _closestGameObject = go;
                    closestDistance = Vector3.Distance(this.transform.position, go.transform.position);
                }
            }
        }
        Asteroid_Tag_Script aTag = _closestGameObject.GetComponent<Asteroid_Tag_Script>();
        aTag._asteroidIsTagged = true;
        _closestDist = (Vector3.Distance(this.transform.position, _closestGameObject.transform.position));
        Debug.DrawLine(this.transform.position, _closestGameObject.transform.position, Color.red, .2f);
        _hasTarget = true;
    }

    /// <summary>
    /// Moves near to desired target
    /// </summary>
    private void MoveToTarget() {
        transform.LookAt(_closestGameObject.transform);
        // If we're close enough, let's mine this shit.
        if (Vector3.Distance(this.transform.position, _closestGameObject.transform.position) < .1f) {
            _hasLoad = true;
            _hasTarget = false;
            // Pickup mineable asteroid and destroy gameobject
            Asteroid_OnMine_Destroy goD = _closestGameObject.GetComponent<Asteroid_OnMine_Destroy>();
            goD.PickUp();
            float overFlowCheck = 0;

            // With this we loop all storage modules and if all of em all full, we cant pick up more cargo.
            bool allStorageFull = false;
            for(int i=0; i<_vehicle_Module_List._storageModuleList.Count; i++) {
                if (_vehicle_Module_List._storageModuleList[i].IsCargoFull()) {
                    allStorageFull = true;
                }else {
                    allStorageFull = false;
                }
            }
            if (!allStorageFull) {
                // Check if storage module is full.
                if (!_vehicle_Module_List._storageModuleList[_currentStorageModule].IsCargoFull()) {
                    // If we get overflow
                    overFlowCheck = _vehicle_Module_List._storageModuleList[_currentStorageModule].AddToCargo(1600);
                    if (overFlowCheck != 0) {
                        Debug.Log("Went over " + overFlowCheck);
                        if (_currentStorageModule < _vehicle_Module_List._storageModuleList.Count) {
                            _currentStorageModule++;
                            _vehicle_Module_List._storageModuleList[_currentStorageModule].AddToCargo(overFlowCheck);
                        } else {
                            Debug.Log("All storages full!");
                        }
                    }
                }
            } else {
                Debug.Log("All cargo used!");
            }
            _lastTimerTrigger = Time.time + _miningPickUpDuration;
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, _closestGameObject.transform.position, _vehicleSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Return vehicle to home base. e.g where it left.
    /// </summary>
    private void ReturnHomeBase() {
        transform.LookAt(_baseLocation);

        if (Vector3.Distance(this.transform.position, _baseLocation) < .1f) {
            _hasLoad = false;
            _isReturning = false;
            _lastTimerTrigger = Time.time + _miningUnloadDuration;
        } 
        else {
            _isReturning = true;
            transform.position = Vector3.MoveTowards(transform.position, _baseLocation, _vehicleSpeed * Time.deltaTime);
        }
        Debug.DrawLine(this.transform.position, _baseLocation, Color.green, .2f);
    }

    public void ClickedMe() {
        _debugSelect = !_debugSelect;
    }
}
