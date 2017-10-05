using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Controller : MonoBehaviour {
    public GameObject roadTilePrefab;
    public GameObject emptyRoadContainerPrefab;

    Road_Branch _roadBranch;
	public Vector3 _start;
	public Vector3 _end;
	
	Vector3 _cursor;
	// wood_res/prod.x/2 +  oma.x/2
	private const float ROAD_OFFSET = 0.5f;
	private const float ROAD_SIZE = 0.1f;
	private const float ROAD_TILE_BUILD_TIME = 0.25f;
	public bool xDone, zDone, xzDone;
	float _lastCheck;
    public bool AdminDebug = false;
    GameObject _container;

    void Start () {
		//_roadBranch = new Road_Branch (_start, _end);
		xDone = false;
		zDone = false;
		xzDone = false;
		_lastCheck = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (AdminDebug) {
            Debug.DrawLine(_start, _end, Color.blue);
        }
		if (Time.time > _lastCheck + ROAD_TILE_BUILD_TIME ) {
			
				
				MakeRoadBetweenPoints (xDone, zDone);

			_lastCheck = Time.time;
		} 
	}

	public void SetPoints(Vector3 start, Vector3 end){
        if (AdminDebug) {
            Debug.Log(start);
        }
		_start = new Vector3((float)System.Math.Round(start.x,1),(float)System.Math.Round(start.y,1),(float)System.Math.Round(start.z,1));
		//Debug.Log (" -> "+_start);
		//Debug.Log(new Vector3((float)System.Math.Round(start.x,2),(float)System.Math.Round(start.y,2),(float)System.Math.Round(start.z,2)));
		_cursor = _start;
		_end = new Vector3((float)System.Math.Round(end.x,1),(float)System.Math.Round(end.y,1),(float)System.Math.Round(end.z,1));
		MakeRoadStart ();
		MakeRoadEnd ();
		xzDone = false;
		MakeRoadBetweenPoints (false, false);
	}
		
	void MakeRoadStart(){
		var offsetX = _start.x > _end.x ? -ROAD_OFFSET : ROAD_OFFSET;
		var position = new Vector3 (offsetX, 0, 0) + _start;
		_cursor = position;
		_start = position;
        //_roadBranch.AddRoadTile (position,1);
         _container = Instantiate(emptyRoadContainerPrefab);
		Instantiate (roadTilePrefab, position, Quaternion.identity, _container.transform);
	}
	void MakeRoadEnd(){
		var offsetZ = _end.z > _start.z ? -ROAD_OFFSET : ROAD_OFFSET;
		var position = new Vector3 (0, 0, offsetZ) + _end;
		_end = position;

		//_roadBranch.AddRoadTile (position,1);
		//Instantiate (roadTilePrefab, position, Quaternion.identity, this.transform);
	}
	void MakeRoadBetweenPoints(bool xDone, bool zDone){
		if (Vector3.Distance (_cursor, _end) < ROAD_SIZE) {
			xDone = true;
			zDone = true;
			xzDone = true;
            Destroy(gameObject);
		}
		//Debug.Log ("@Road points");
		int x = (int)_cursor.x;
		if (!xDone) {
			//Debug.Log ("@Road points: !xDone");
			if (_cursor.x +ROAD_SIZE < _end.x) {
				if (_cursor.x >= _end.x) {
					xDone = true;
				}
				_cursor.x += ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, _container.transform);
			} else if (_cursor.x > _end.x) {
				if (_cursor.x >= _end.x) {
					xDone = true;
				}
				_cursor.x -= ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, _container.transform);
			} else {
				xDone = true;
			}
		}
		if (xDone && !zDone) {
			//Debug.Log ("@Road points: !zDone");
			if (_cursor.z < _end.z) {
				if (_cursor.z >= _end.z) {
					zDone = true;
					xzDone = true;
				}
				_cursor.z += ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, _container.transform);
			} else if (_cursor.z > _end.z) {
				if (_cursor.z >= _end.z) {
					zDone = true;
					xzDone = true;
				}
				_cursor.z -= ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, _container.transform);
			} else {
				zDone = true;
				xzDone = true;
			}
		}
	}
}
