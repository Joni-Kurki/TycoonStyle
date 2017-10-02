using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Controller : MonoBehaviour {

	Road_Branch _roadBranch;
	public Vector3 _start;
	public Vector3 _end;
	public GameObject roadTilePrefab;
	Vector3 _cursor;
	// wood_res/prod.x/2 +  oma.x/2
	private const float ROAD_OFFSET = 0.5f;
	private const float ROAD_SIZE = 0.1f;
	private const float ROAD_TILE_BUILD_TIME = 0.25f;
	public bool xDone, zDone, xzDone;
	float _lastCheck;

	void Start () {
		_roadBranch = new Road_Branch (_start, _end);
		MakeRoadStart ();
		MakeRoadEnd ();
		_cursor = _start;
		xDone = false;
		zDone = false;
		xzDone = false;
		_lastCheck = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (_start, _end, Color.blue);
		if (Time.time > _lastCheck + ROAD_TILE_BUILD_TIME ) {
			if (!xzDone) {
				MakeRoadBetweenPoints (xDone, zDone);
			}
			_lastCheck = Time.time;
		} 
	}
	void MakeRoadStart(){
		var offsetX = _start.x > _end.x ? -ROAD_OFFSET : ROAD_OFFSET;
		var position = new Vector3 (offsetX, 0, 0) + _start;
		_cursor = position;
		_start = position;
		_roadBranch.AddRoadTile (position,1);

		Instantiate (roadTilePrefab, position, Quaternion.identity, this.transform);
	}
	void MakeRoadEnd(){
		var offsetZ = _end.z > _start.z ? -ROAD_OFFSET : ROAD_OFFSET;
		var position = new Vector3 (0, 0, offsetZ) + _end;
		_end = position;

		_roadBranch.AddRoadTile (position,1);
		Instantiate (roadTilePrefab, position, Quaternion.identity, this.transform);
	}
	void MakeRoadBetweenPoints(bool xDone, bool zDone){
		int x = (int)_cursor.x;
		if (!xDone) {
			if (_cursor.x +ROAD_SIZE < _end.x) {
				if (_cursor.x >= _end.x) {
					xDone = true;
				}
				_cursor.x += ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, this.transform);
			} else if (_cursor.x > _end.x) {
				if (_cursor.x >= _end.x) {
					xDone = true;
				}
				_cursor.x -= ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, this.transform);
			} else {
				xDone = true;
			}
		}
		if (xDone && !zDone) {
			if (_cursor.z < _end.z) {
				if (_cursor.z >= _end.z) {
					zDone = true;
					xzDone = true;
				}
				_cursor.z += ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, this.transform);
			} else if (_cursor.z > _end.z) {
				if (_cursor.z >= _end.z) {
					zDone = true;
					xzDone = true;
				}
				_cursor.z -= ROAD_SIZE;
				Instantiate (roadTilePrefab, _cursor, Quaternion.identity, this.transform);
			} else {
				zDone = true;
				xzDone = true;
			}
		}
	}
}
