using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Pooler_Script : MonoBehaviour {

    public GameObject _asteroidPrefab;
    public int _asteroidNumber;
    const int MAX_ASTEROID_NUMBER = 100;
    public Vector3 _spanwLocation;

	// Use this for initialization
	void Start () {
        InstantiateAsteroids(_spanwLocation, -2,2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InstantiateAsteroids(Vector3 location, float randomMin, float randomMax) {
        int randomN = Random.Range(0, MAX_ASTEROID_NUMBER);
        for (int i = 0; i < randomN; i++) {
            Vector3 offset = new Vector3(Random.Range(randomMin, randomMax), Random.Range(randomMin / Mathf.PI, randomMax / Mathf.PI), Random.Range(randomMin, randomMax));
            Instantiate(_asteroidPrefab, _spanwLocation + offset, Quaternion.identity, this.transform);
        }
    }
}
