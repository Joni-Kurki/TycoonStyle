using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure {

	public string _structureName;

	public enum _buildingType {
		Production = 0,
		Resource = 1
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