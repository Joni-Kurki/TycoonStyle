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
        Production = 0,
        War,
        Gatherer,
        Transport
    }

    public Vehicle() {
    }
}

public class Space_Miner_Vehicle : Vehicle {

    Vehicle_Module_List _moduleList;

    public Space_Miner_Vehicle(int numberOfStorageModules, int numberOfWeaponModules, int numberOfToolModules) {
        _canFly = true;
        _numberOfStorageModules = numberOfStorageModules;
        _numberOfWeaponModules = numberOfWeaponModules;
        _numberOfToolModules = numberOfToolModules;
        _moduleList = new Vehicle_Module_List();
    }
    
    private void InitModules() {
        for(int i=0; i< _numberOfStorageModules; i++) {
            Storage_Vehicle_Module s = new Storage_Vehicle_Module("Storage" + i, 1);
            _moduleList.AddStorageModule(s);
        }
        for (int i = 0; i < _numberOfWeaponModules; i++) {
            Weapon_Vehicle_Module w = new Weapon_Vehicle_Module("Weapon" + i, 1);
            _moduleList.AddWeaponModule(w);
        }
        for (int i = 0; i < _numberOfToolModules; i++) {
            Tool_Vehicle_Module t = new Tool_Vehicle_Module("Tool" + i, 1);
            _moduleList.AddToolModule(t);
        }
    }
}

public class War_Vehicle : Vehicle {

    Vehicle_Module_List _moduleList;

    public War_Vehicle(int numberOfStorageModules, int numberOfWeaponModules, int numberOfToolModules) {
        _canFly = true;
        _numberOfStorageModules = numberOfStorageModules;
        _numberOfWeaponModules = numberOfWeaponModules;
        _numberOfToolModules = numberOfToolModules;
        _moduleList = new Vehicle_Module_List();
    }

    private void InitModules() {
        for (int i = 0; i < _numberOfStorageModules; i++) {
            Storage_Vehicle_Module s = new Storage_Vehicle_Module("Storage" + i, 1);
            _moduleList.AddStorageModule(s);
        }
        for (int i = 0; i < _numberOfWeaponModules; i++) {
            Weapon_Vehicle_Module w = new Weapon_Vehicle_Module("Weapon" + i, 1);
            _moduleList.AddWeaponModule(w);
        }
        for (int i = 0; i < _numberOfToolModules; i++) {
            Tool_Vehicle_Module t = new Tool_Vehicle_Module("Tool" + i, 1);
            _moduleList.AddToolModule(t);
        }
    }
}

public class Space_Passanger_Vehicle : Vehicle{
    Vehicle_Module_List _moduleList;

    public Space_Passanger_Vehicle(int numberOfStorageModules, int numberOfWeaponModules, int numberOfToolModules) {
        _canFly = true;
        _numberOfStorageModules = numberOfStorageModules;
        _numberOfWeaponModules = numberOfWeaponModules;
        _numberOfToolModules = numberOfToolModules;
        _moduleList = new Vehicle_Module_List();
    }

    private void InitModules() {
        for (int i = 0; i < _numberOfStorageModules; i++) {
            Storage_Vehicle_Module s = new Storage_Vehicle_Module("Storage" + i, 1);
            _moduleList.AddStorageModule(s);
        }
        for (int i = 0; i < _numberOfWeaponModules; i++) {
            Weapon_Vehicle_Module w = new Weapon_Vehicle_Module("Weapon" + i, 1);
            _moduleList.AddWeaponModule(w);
        }
        for (int i = 0; i < _numberOfToolModules; i++) {
            Tool_Vehicle_Module t = new Tool_Vehicle_Module("Tool" + i, 1);
            _moduleList.AddToolModule(t);
        }
    }
}

