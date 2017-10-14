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
    
    // For Asteroid tracking
    public int _numberOfAsteroidsSpawned;
    public int _currentAmountOfAsteroids;

    // Is the asteroid pooler generated from code or via editor
    private bool _initFromCode;

    int[] _asteroidTypeCount = new int[3] { 0,0,0 };

    // Use this for initialization
    void Start () {
        _initFromCode = false;
        //InstantiateAsteroids(_spanwLocation);
    }

    /// <summary>
    /// This method is used for setting all needed values to make asteroid belt from code and not editor. It's like constructor.
    /// </summary>
    /// <param name="asteroidPrefab"></param>
    /// <param name="spanwLocation"></param>
    /// <param name="distanceOffsetMin"></param>
    /// <param name="distanceOffsetMax"></param>
    /// <param name="yOffsetMin"></param>
    /// <param name="yOffsetMax"></param>
    /// <param name="numberOfAsteroidsMin"></param>
    /// <param name="numberOfAsteroidsMax"></param>
    public void SetInitialValues(   Vector3 spanwLocation, float distanceOffsetMin, float distanceOffsetMax, 
                                    float yOffsetMin, float yOffsetMax, int numberOfAsteroidsMin, int numberOfAsteroidsMax) {
        _spanwLocation = spanwLocation;
        _distanceOffsetMin = distanceOffsetMin;
        _distanceOffsetMax = distanceOffsetMax;
        _yOffsetMin = yOffsetMin;
        _yOffsetMax = yOffsetMax;
        _numberOfAsteroidsMin = numberOfAsteroidsMin;
        _numberOfAsteroidsMax = numberOfAsteroidsMax;
        _initFromCode = true;

        InstantiateAsteroids(_spanwLocation);

        _asteroidTypeCount = new int[3];
    }

    void ClassifyChildAsteroidTypes() {
        Asteroid_TypeAndData_Script a;

        for (int i=0; i<transform.childCount; i++) {
            a = transform.GetChild(i).GetComponent<Asteroid_TypeAndData_Script>();
            //Debug.Log(a.name + " > "+a.GetAsteroidType());
            if (a.GetAsteroidType() == 2) {
                _asteroidTypeCount[2]++;
            }
            if (a.GetAsteroidType() == 1) {
                _asteroidTypeCount[1]++;
            }
            if (a.GetAsteroidType() == 0) {
                _asteroidTypeCount[0]++;
            }
        }
        //Debug.Log("C: " + _asteroidTypeCount[0] + "\tS: " + _asteroidTypeCount[1] + "\tM: " + _asteroidTypeCount[2]);
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
        _numberOfAsteroidsSpawned = randomN;
        _currentAmountOfAsteroids = _numberOfAsteroidsSpawned;
        for (int i = 0; i < randomN; i++) {
            // Let's random the angle
            float angle = Random.Range(0f, 360f);
            // Random the distances and y offset
            float randomDistance = Random.Range(_distanceOffsetMin, _distanceOffsetMax);
            float randomYOffset = Random.Range(_yOffsetMin, _yOffsetMax);
            // Instantiate @ desired location
            var obj = Instantiate(_asteroidPrefab, _spanwLocation, Quaternion.identity, this.transform);
            obj.name = "Asteroid " + i;
            // Now let's rotate the object and move it to desired angle with randomized distance
            obj.transform.Rotate(new Vector3(0, angle, 0));
            obj.transform.Translate(new Vector3(randomDistance, randomYOffset, randomDistance));
            //Debug.DrawLine(Vector3.zero, obj.transform.position, Color.red, 30);
        }

        ClassifyChildAsteroidTypes();
    }
}
