using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour {

	public Tilemap map2;
	Grid grid;
	public Vector3Int clickedPos;
	public string tileName;
	[HideInInspector]
	public Vector3Int prevPos;

	// Use this for initialization
	void Start () {
		grid = GetComponent<Grid> ();
		if (map2.HasTile (new Vector3Int (0, 0, 0))) {
			//Debug.Log(map2.GetTile (new Vector3Int (0, 0, 0)));
			//Debug.Log(map2.GetTile (new Vector3Int (0, 0, 0)).name);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void getTile() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Vector3 worldPoint = ray.GetPoint (-ray.origin.z / ray.direction.z);
		Vector3Int position = grid.WorldToCell (worldPoint);
		//Debug.Log ("position = " + position);
		//Debug.Log(map2.GetTile(position).name);
		prevPos = clickedPos;
		clickedPos = position;
		tileName = map2.GetTile (position).name;
	}

	public void resetPrevTile(){
		clickedPos = prevPos;
	}

	// returns int value that represents time it takes to travel across
	public int getTileTime () {
		float rand = Random.value;
		switch (tileName) {
		case "hexart_1_3": // grasslands
		case "hexart_1_4": //tall grass
			return 1;
		case "hexart_1_9": // forest
			return 2;
		case "hexart_1_7": //volcanoes
			return 5;
		case "hexart_1_10": // mountains
			return 3;
		default:
			return 0;
		}
	}

	Vector3 getTilePos () {
		return clickedPos;
	}
}
