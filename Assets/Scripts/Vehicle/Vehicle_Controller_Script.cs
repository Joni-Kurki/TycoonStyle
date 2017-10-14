using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Controller_Script : MonoBehaviour {

    // Vehicle class and subclasses. TODO: needs to be some smarter system here.
    Vehicle _vehicle;
    War_Vehicle _War_vehicle;
    SpaceMiner_Vehicle _SpaceMiner_vehicle;

    // Number of modules
    public int _storageNumber;
    public int _weaponNumber;
    public int _toolNumber;
    public int _vehicleType;
    public string _vehicleName;
    public float _vehicleSpeed;

    // Some distance variables
    GameObject _closestGameObject;
    float _closestDist;
    Rigidbody _rb;

    // Variables that have something to do with mining
    public Vector3 _baseLocation;
    float _miningPickUpDuration;
    float _miningUnloadDuration;
    float _lastTimerTrigger;
    
    // Bool's for controlling phases
    bool _isMoving;
    bool _hasTarget;
    bool _hasLoad;
    bool _isReturning;
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
	}

    /// <summary>
    /// Finds the closest mineable target 
    /// </summary>
    private void FindTarget() {
        GameObject[] goList = GameObject.FindGameObjectsWithTag("Asteroid_Mineable");
        float closestDistance = Mathf.Infinity;
        //Debug.Log(goList.Length);
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
}
