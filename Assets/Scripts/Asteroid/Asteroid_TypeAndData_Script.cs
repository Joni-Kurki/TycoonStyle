using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_TypeAndData_Script : MonoBehaviour {

    private int _typeInInt;
    /// <summary>
    /// C - dark carbonaceous
    /// S - silicaceous
    /// M - metallic
    /// </summary>
    protected enum _asteroidType {
        C = 0,
        S = 1,
        X = 2,
        E = -1
    }


	// Use this for initialization
	void Start () {
        _typeInInt = RandomizeType();
    }
	
    public int GetAsteroidType() {
        return _typeInInt;
    }

    /// <summary>
    /// Randomizes the asteroid type
    /// </summary>
    /// <returns></returns>
    int RandomizeType() {
        float r = Random.value;
        

        if(r >= 0 && r < .69f) {
            return 0;
        }
        else if(r >= .7f && r < .9f) {
            return 1;
        }
        else if(r >= .9f) {
            return 2;
        }
        return -1;
    }

    /// <summary>
    /// Converts int to _asteroidType enum
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected _asteroidType ConvertIntToEnum(int value) {
        switch (value) {
            case 0:
                return _asteroidType.C;
            case 1:
                return _asteroidType.S;
            case 2:
                return _asteroidType.X;
        }
        return _asteroidType.E;
    }

    protected int ConvertEnumToInt(_asteroidType a) {
        Debug.Log("Returning " + a);
        switch (a) {
            case _asteroidType.C:
                return 0;
            case _asteroidType.S:
                return 1;
            case _asteroidType.X:
                return 2;
        }
        return -1;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
