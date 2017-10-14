using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Pooler_Script : MonoBehaviour {

    public GameObject _asteroidPrefab;

    public Vector3 _spanwLocation;
    public float _distanceOffsetMin;
    public float _distanceOffsetMax;
    public float _yOffsetMin;
    public float _yOffsetMax;
    public int _numberOfAsteroidsMin;
    public int _numberOfAsteroidsMax;

    // Use this for initialization
    void Start () {
        InstantiateAsteroids(_spanwLocation);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Instantiates n amount of asteroids in a donut shaped system. 
    /// </summary>
    /// <param name="location"></param>
    /// <param name="randomMin"></param>
    /// <param name="randomMax"></param>
    void InstantiateAsteroids(Vector3 location) {

        int randomN = Random.Range(_numberOfAsteroidsMin, _numberOfAsteroidsMax);
        for (int i = 0; i < randomN; i++) {
            // Let's random the angle
            float angle = Random.Range(0f, 360f);
            // Random the distances and y offset
            float randomDistance = Random.Range(_distanceOffsetMin, _distanceOffsetMax);
            float randomYOffset = Random.Range(_yOffsetMin, _yOffsetMax);
            // Instantiate @ desired location
            var obj = Instantiate(_asteroidPrefab, _spanwLocation, Quaternion.identity, this.transform);
            // Now let's rotate the object and move it to desired angle with randomized distance
            obj.transform.Rotate(new Vector3(0, angle, 0));
            obj.transform.Translate(new Vector3(randomDistance, randomYOffset, randomDistance));
            //Debug.DrawLine(Vector3.zero, obj.transform.position, Color.red, 30);
        }
    }
}
