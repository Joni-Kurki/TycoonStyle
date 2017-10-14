using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversumCreator : MonoBehaviour {

    public GameObject _asteroid_ContainerAndSpawnerPrefab;
    private Asteroid_Pooler_Script _asteroidPoolerScript;
    public List<GameObject> _asteroidSpawnerList;
    public List<Vector3> _asteroidSpawnerInitialLocations;

	// Use this for initialization
	void Start () {
        _asteroidSpawnerList = new List<GameObject>();
        SpawnSomeSpawnersAndShit(4, _asteroidSpawnerInitialLocations);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Here we can spawn n amount of asteroid poolers at desired locations, which each hold n amount of asteroids. 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="spawnLocations"></param>
    void SpawnSomeSpawnersAndShit(int value, List<Vector3> spawnLocations) {
        for(int i = 0; i < spawnLocations.Count; i++) {
            GameObject go = Instantiate(_asteroid_ContainerAndSpawnerPrefab, this.transform.position, _asteroid_ContainerAndSpawnerPrefab.transform.rotation, this.transform);
            
            _asteroidSpawnerList.Add(go);
            _asteroidPoolerScript = go.GetComponent<Asteroid_Pooler_Script>();
            go.transform.Translate(spawnLocations[i]);
            
            _asteroidPoolerScript.SetInitialValues(spawnLocations[i], 6f+i*4, 10f+i*4, -1.5f, 1.5f, 250, 300);
        }
        
    }

}
