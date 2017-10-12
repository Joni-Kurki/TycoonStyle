using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Module {

    public string _name { get; set; }
    public int _moduleSize { get; set; }
    public Vehicle_Module_List _moduleList;

    public enum _vehicleModuleType {
        Storage = 1,
        Weapon,
        Tool
    }
}

public class Storage_Vehicle_Module : Vehicle_Module {

    Vehicle_Module._vehicleModuleType _moduleType;

    public Storage_Vehicle_Module(string name, int size) {
        _name = name;
        _moduleType = _vehicleModuleType.Storage;
        _moduleSize = size;
        _moduleList = new Vehicle_Module_List();
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

    public List<Object> _moduleList;

    public Vehicle_Module_List() {
        _moduleList = new List<Object>();
    }

    public List<Object> GetModules() {
        return _moduleList;
    }
    public void AddModule(Object module) {
        _moduleList.Add(module);
    }
}