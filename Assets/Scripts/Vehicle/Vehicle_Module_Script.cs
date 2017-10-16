using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Module {

    public string _name { get; set; }
    public int _moduleSize { get; set; }
    public Vehicle_Module_List _moduleList;

    public enum _vehicleModuleType {
        Storage = 0,
        Weapon,
        Tool
    }
}

public class Storage_Vehicle_Module : Vehicle_Module {

    float _currentCargoWeigth;
    int _maxCargoWeight;
    const int CARGO_SIZE_MULTIPLIER = 1000;
    Vehicle_Module._vehicleModuleType _moduleType;

    public Storage_Vehicle_Module(string name, int size) {
        _name = name;
        _moduleType = _vehicleModuleType.Storage;
        _moduleSize = size;
        _currentCargoWeigth = 0;
        _maxCargoWeight = size * CARGO_SIZE_MULTIPLIER;
    }

    public float GetCurrentCargoWeight() {
        return _currentCargoWeigth;
    }

    public int GetMaxCargoWeight() {
        return _maxCargoWeight;
    }

    /// <summary>
    /// Checks if we can fit loot to storage
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public float AddToCargo(float amount) {
        float temp = 0;
        // No overflow
        if(_currentCargoWeigth + amount <= _maxCargoWeight) {
            _currentCargoWeigth += amount;
            return 0;
        } 
        // Overflow
        else if (_currentCargoWeigth + amount > _maxCargoWeight) {
            temp = (_currentCargoWeigth + amount) - _maxCargoWeight;
            _currentCargoWeigth = _maxCargoWeight;
            return temp;
        }
        return 0;
    }

    public void EmptyCargo() {
        _currentCargoWeigth = 0;
    }

    public bool IsCargoFull() {
        return _currentCargoWeigth == _maxCargoWeight ? true : false;
    }

}

public class Weapon_Vehicle_Module : Vehicle_Module {

    Vehicle_Module._vehicleModuleType _moduleType;

    public Weapon_Vehicle_Module(string name, int size) {
        _name = name;
        _moduleType = _vehicleModuleType.Weapon;
        _moduleSize = size;
    }
}

public class Tool_Vehicle_Module : Vehicle_Module {

    Vehicle_Module._vehicleModuleType _moduleType;

    public Tool_Vehicle_Module(string name, int size) {
        _name = name;
        _moduleType = _vehicleModuleType.Tool;
        _moduleSize = size;
    }
}

public class Vehicle_Module_List {

    public List<Storage_Vehicle_Module> _storageModuleList;
    public List<Weapon_Vehicle_Module> _weaponModuleList;
    public List<Tool_Vehicle_Module> _toolModuleList;

    public Vehicle_Module_List() {
        _storageModuleList = new List<Storage_Vehicle_Module>();
        _weaponModuleList = new List<Weapon_Vehicle_Module>();
        _toolModuleList = new List<Tool_Vehicle_Module>();
    }

    public void AddStorageModule(Storage_Vehicle_Module module) {
        _storageModuleList.Add(module);
    }
    public void AddWeaponModule(Weapon_Vehicle_Module module) {
        _weaponModuleList.Add(module);
    }
    public void AddToolModule(Tool_Vehicle_Module module) {
        _toolModuleList.Add(module);
    }
}