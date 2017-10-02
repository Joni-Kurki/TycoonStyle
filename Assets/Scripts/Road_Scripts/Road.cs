using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Tile  {

	public int _x { get; set; }
	public int _y { get; set; }
	Road_Tile_Type _roadTileType;

	enum Road_Tile_Type {
		Dirt = 0,
		Asphalt = 1
	}

	public Road_Tile(int x, int y, int roadTileTypeIndex){
		this._x = x;
		this._y = y;
		this._roadTileType = ConvertIntToRoadTileType(roadTileTypeIndex);
	}

	private Road_Tile_Type ConvertIntToRoadTileType(int value){
		Road_Tile_Type returnType;
		switch (value) {
			case 0:
				return returnType = Road_Tile_Type.Dirt;
			case 1:
				return returnType = Road_Tile_Type.Asphalt;
			default:
				return returnType = Road_Tile_Type.Dirt;
		}
	}
}

public class Road_Branch {
	List<Road_Tile> _roadTileList;
	public Vector3 _start;
	public Vector3 _end;

	public Road_Branch(Vector3 start, Vector3 end){
		this._start = start;
		this._end = end;
		_roadTileList = new List<Road_Tile> ();
	}

	public int CalculateX(){
		return (int)(_start.x -_end.x);
	}

	public int CalculateZ(){
		return (int)(_start.z - _end.z);
	}

	public void AddRoadTile(Vector3 position, int tileType){
		Road_Tile tile = new Road_Tile ((int)position.x, (int)position.z, tileType);
	}
}
