using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure {

	public string _structureName;

	public enum _buildingType {
		Production = 0,
		Resource = 1,
		Storage = 2
	}
}

public class Wood_Resource_Structure : Structure {
	float resourceInterval;
	const int RESOURCE_MAX_CAPACITY = 100;
	int currentCapacity;

	public Wood_Resource_Structure(string name){
		_structureName = name;
		_buildingType bType = _buildingType.Resource;
		resourceInterval = 1.0f;
		currentCapacity = 0;
	}

	public float GetResourceInterval(){
		return resourceInterval;
	}
    /// <summary>
    /// Returns current amount of wood this structure has.
    /// </summary>
    /// <returns></returns>
	public int GetCurrentCapacity(){
		return currentCapacity;
	}

	public void AddResourceTick(){
		currentCapacity++;
	}
}

public class Wood_Production_Structure : Structure{

	private int _currentWoodPlankCount;
	private int _currentResourceCount;
	private float _productionInterval;
	private const int WOOD_PLANK_MANUFACTORE_COST = 3;
	private bool isManufactoring;

	public Wood_Production_Structure(string name){
		_structureName = name;
		_buildingType bType = _buildingType.Production;
		_currentWoodPlankCount = 0;
		_currentResourceCount = 0;
		_productionInterval = 2.5f;
		isManufactoring = false;
	}

	public int GetCurrentResources(){
		return _currentResourceCount;
	}

	public int GetCurrentWoodPlanks(){
		return _currentWoodPlankCount;
	}

	public void TransportArrivedWithResources(int value){
		_currentResourceCount += value;
	}

	public void ManufactoreWoodPlanks(int quantity){
		isManufactoring = true;
		float totalManufactoringTime = quantity * _productionInterval;
		float productsReady = Time.time + totalManufactoringTime;

		if (quantity * WOOD_PLANK_MANUFACTORE_COST > _currentResourceCount) {
			Debug.Log ("Not enough resources!");
		} else {
			_currentWoodPlankCount += quantity;
			_currentResourceCount -= (quantity * WOOD_PLANK_MANUFACTORE_COST);
		}
	}

	// IENumeratorilla products ready
}

public class Wood_Storage_Structure : Structure {
	private const int MAX_STORAGE_CAPACITY = 500;
	private int _currentWood;

	public Wood_Storage_Structure(string name){
		_buildingType bType = _buildingType.Storage;
		_currentWood = 0;
		_structureName = name;
	}

	public void FillStorage(int value){
		if (_currentWood + value > MAX_STORAGE_CAPACITY) {
			_currentWood = MAX_STORAGE_CAPACITY;
		}
		_currentWood += value;

	}

	public int GetStorage(){
		return _currentWood;
	}
}