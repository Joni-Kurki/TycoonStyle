using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Controller_Script : MonoBehaviour {

    // Vehicle class and subclasses. TODO: needs to be some smarter system here.
    Vehicle _vehicle;
    War_Vehicle _War_vehicle;
    Space_Miner_Vehicle _SpaceMiner_vehicle;
    Space_Passanger_Vehicle _SpacePassanger_Vechicle;
    Vehicle_Module_List _vehicle_Module_List;

    // Passanger vehicles waypointings 
    public Vector3[] _passangerRouteWaypoints;
    int _passangerCurrentWaypoint;
    bool _passangerRouteDone;
    bool _passangerToStart;
    bool _passangerToEnd;
    float _passangerVehicleLoadingTime;

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
    bool _minerHasFullLoad;
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
                _vehicleName = "Space Miner Vehicle";
                _SpaceMiner_vehicle = new Space_Miner_Vehicle(2, 0, 1);
                _storageNumber = _SpaceMiner_vehicle._numberOfStorageModules;
                _weaponNumber = _SpaceMiner_vehicle._numberOfWeaponModules;
                _toolNumber = _SpaceMiner_vehicle._numberOfToolModules;

                _vehicle_Module_List = new Vehicle_Module_List();
                Storage_Vehicle_Module mod = new Storage_Vehicle_Module("Cargo module", 3);
                _vehicle_Module_List.AddStorageModule(mod);
                mod = new Storage_Vehicle_Module("Cargo module 2", 5);
                _vehicle_Module_List.AddStorageModule(mod);
                _currentStorageModule = 0;
                _minerHasFullLoad = false;

                _vehicleSpeed = 3.3f;
                _miningPickUpDuration = 3f;
                _miningUnloadDuration = 2f;
                _lastTimerTrigger = Time.time;
                break;
            case 3:
                _vehicleName = "Space Passanger Vehicle";
                _SpacePassanger_Vechicle = new Space_Passanger_Vehicle(3, 0, 0);
                _storageNumber = _SpacePassanger_Vechicle._numberOfStorageModules;
                _weaponNumber = _SpacePassanger_Vechicle._numberOfWeaponModules;
                _toolNumber = _SpacePassanger_Vechicle._numberOfToolModules;

                _vehicle_Module_List = new Vehicle_Module_List();

                Storage_Vehicle_Module passangerModule = new Storage_Vehicle_Module("Passanger module", 3);
                _vehicle_Module_List.AddStorageModule(passangerModule);
                mod = new Storage_Vehicle_Module("Passanger module", 5);
                _vehicle_Module_List.AddStorageModule(passangerModule);
                mod = new Storage_Vehicle_Module("Passanger module", 2);
                _vehicle_Module_List.AddStorageModule(passangerModule);

                _vehicleSpeed = 5f;
                _lastTimerTrigger = Time.time;

                _currentStorageModule = 0;
                _passangerRouteWaypoints = new Vector3[4] { transform.position, transform.position, transform.position, transform.position };
                _passangerCurrentWaypoint = 0;
                // Set starting location to where vehicle was initially.
                Passanger_SetStartTargetLocation(transform.position);
                Passanger_SetEndTargetLocation(new Vector3(98, 0, 98));
                _passangerToStart = false;
                _passangerToEnd = true;
                _passangerVehicleLoadingTime = 6f;

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
        // Space Miner Vehicle actions
        if (_vehicleType == 2) {
            if (Time.time > _lastTimerTrigger) {
                if (!_hasTarget && !_minerHasFullLoad) {
                    if (_debugSelect) {
                        Debug.Log("FindTarget");
                    }
                    Miner_FindTarget();
                } else if (_hasTarget && !_minerHasFullLoad) {
                    Miner_MoveToTarget();
                    if (_debugSelect) {
                        Debug.Log("Moving to target");
                    }
                    if (_debugSelect) {
                        Debug.DrawLine(this.transform.position, _closestGameObject.transform.position, Color.red, 1);
                    }
                } else if (_minerHasFullLoad) {
                    if (_debugSelect) {
                        Debug.Log("Full load, moving home");
                    }
                    Miner_ReturnHomeBase();
                    if (_debugSelect) {
                        Debug.DrawLine(this.transform.position, _baseLocation, Color.green, .2f);
                    }
                }
            }
        }
        // Space Passanger Vehicle actions
        if (_vehicleType == 3) {
            // Let's make waypoints first. TODO: Here boolean check if needs to refresh
            if(_passangerRouteWaypoints[0] != _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1] && !_passangerRouteDone) {
                Passanger_DoRoute();
            }// If routes done, let's start moving.
            else if (_passangerRouteDone) {
                // If we are @ waypoint, let's move to next waypoint, based on the current one.
                if (Vector3.Distance(transform.position, _passangerRouteWaypoints[_passangerCurrentWaypoint]) <= .1f) {
                    if (_passangerToEnd) {
                        _passangerCurrentWaypoint++;
                        if (_passangerCurrentWaypoint > 3) {
                            _lastTimerTrigger = Time.time;
                            _passangerToEnd = false;
                            _passangerToStart = true;
                            _passangerCurrentWaypoint = 2;
                        }
                    } else if (_passangerToStart) {
                        _passangerCurrentWaypoint--;
                        if (_passangerCurrentWaypoint < 0) {
                            _lastTimerTrigger = Time.time;
                            _passangerToEnd = true;
                            _passangerToStart = false;
                            _passangerCurrentWaypoint = 1;
                        }
                    } 
                } // If we are on the way, move ship
                else {
                    if (Time.time >= _lastTimerTrigger + _passangerVehicleLoadingTime) {
                        transform.LookAt(_passangerRouteWaypoints[_passangerCurrentWaypoint]);
                        transform.position = Vector3.MoveTowards(transform.position, _passangerRouteWaypoints[_passangerCurrentWaypoint], _vehicleSpeed * Time.deltaTime);
                        if (_debugSelect) {
                            Debug.DrawLine(transform.position, _passangerRouteWaypoints[_passangerCurrentWaypoint], Color.red);
                        }
                    }
                }
            }
        }
        // Degug selected
        if (_debugSelect) {
            PasteCargoToDebug();
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
    private void Miner_FindTarget() {
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
    private void Miner_MoveToTarget() {
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
                } else {
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
                _minerHasFullLoad = true;
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
    private void Miner_ReturnHomeBase() {
        transform.LookAt(_baseLocation);

        // If we have reached baselocation
        if (Vector3.Distance(this.transform.position, _baseLocation) < .1f) {
            _hasLoad = false;
            _isReturning = false;
            _minerHasFullLoad = false;
            Miner_EmptyCargo();
            _lastTimerTrigger = Time.time + _miningUnloadDuration;
        } 
        else {
            _isReturning = true;
            transform.position = Vector3.MoveTowards(transform.position, _baseLocation, _vehicleSpeed * Time.deltaTime);
        }
    }

    private float Miner_EmptyCargo() {
        float temp = 0;
        for(int i=0; i<_vehicle_Module_List._storageModuleList.Count; i++) {
            temp += _vehicle_Module_List._storageModuleList[i].GetCurrentCargoWeight();
            _vehicle_Module_List._storageModuleList[i].EmptyCargo();
        }
        if (_debugSelect) {
            Debug.Log("@Home, unloading rewarding " + temp);
        }
        return temp;
    }

    /// <summary>
    /// Primitive waypoint making for point start -> near_start -> near_end -> end
    /// </summary>
    private void Passanger_DoRoute() {
        for (int i = 0; i < 2; i++) {
            float x = 0, y = 0, z = 0;
            if (_passangerRouteWaypoints[0].x < _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1].x) {
                if (i == 0) {
                    x += 0.5f;
                } else {
                    x -= 0.5f;
                }
            }
            if (_passangerRouteWaypoints[0].z < _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1].z) {
                if (i == 0) {
                    z += 0.5f;
                } else {
                    z -= 0.5f;
                }
            }
            if (i == 0) {
                _passangerRouteWaypoints[i + 1] = new Vector3(
                    _passangerRouteWaypoints[0].x + x,
                    _passangerRouteWaypoints[0].y,
                    _passangerRouteWaypoints[0].z + z
                );
            } else {
                _passangerRouteWaypoints[i + 1] = new Vector3(
                    _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1].x + x,
                    _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1].y,
                    _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1].z + z
                );
            }
        }
        _passangerRouteDone = true;
    }

    /// <summary>
    /// Sets the start waypoint for passanger vehicle.
    /// </summary>
    /// <param name="start"></param>
    public void Passanger_SetStartTargetLocation(Vector3 start) {
        _passangerRouteWaypoints[0] = start;
    }

    /// <summary>
    /// Sets the end waypoint for passanger vehicle
    /// </summary>
    /// <param name="end"></param>
    public void Passanger_SetEndTargetLocation(Vector3 end) {
        _passangerRouteWaypoints[_passangerRouteWaypoints.Length - 1] = end;
    }

    public void VehicleToggleButton() {
        _debugSelect = !_debugSelect;
    }

}
