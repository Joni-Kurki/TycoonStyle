using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle {
        
    public string _name { get; set; }
    public bool _canFly { get; set; }
    // moduulien lukumäärät
    public int _numberOfStorageModules { get; set; }
    public int _numberOfWeaponModules { get; set; }
    public int _numberOfToolModules { get; set; }

    public enum _vehicleType {
        Production = 1,
        War,
        Gatherer
    }

    public Vehicle() {

    }
}

public class SpaceMiner_Vehicle : Vehicle {

    public SpaceMiner_Vehicle(int numberOfStorageModules, int numberOfWeaponModules, int numberOfToolModules) {
        _canFly = true;
        _numberOfStorageModules = numberOfStorageModules;
        _numberOfWeaponModules = numberOfWeaponModules;
        _numberOfToolModules = numberOfToolModules;
    }

}

public class War_Vehicle : Vehicle {

    public War_Vehicle() {

    }
}

