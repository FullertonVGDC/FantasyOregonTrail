using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager_1 : MonoBehaviour {

	int gameTime = 0;

	public Grid grid;
	public GameObject playerObj;

	private GridScript gridinfo;
	private PlayerScript playerinfo;

	void Awake() {
		gridinfo = grid.GetComponent<GridScript> ();
		playerinfo = playerObj.GetComponent<PlayerScript> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			gridinfo.getTile ();
			movePlayer ();
		}
	}

	void movePlayer() {
		Vector3 cellPoint = grid.CellToWorld (gridinfo.clickedPos);
		if (checkPossibleMove(gridinfo.clickedPos)) {
			playerObj.transform.SetPositionAndRotation (cellPoint, Quaternion.identity);
			playerinfo.currPos = gridinfo.clickedPos;
			gameTime += gridinfo.getTileTime ();
			Debug.Log ("gameTime = " + gameTime);
		} else
			Debug.Log ("Invalid Movement");

	}

	bool checkPossibleMove(Vector3Int loc) {
		//even y
		if (playerinfo.currPos.y % 2 == 0 || playerinfo.currPos.y == 0) {
			if ( (loc.x == playerinfo.currPos.x - 1 && (loc.y == playerinfo.currPos.y + 1 || loc.y == playerinfo.currPos.y - 1 || loc.y == playerinfo.currPos.y))
				 || (loc.x == playerinfo.currPos.x && (loc.y == playerinfo.currPos.y + 1 || loc.y == playerinfo.currPos.y - 1))
				 || (loc.x == playerinfo.currPos.x + 1 && (loc.y == playerinfo.currPos.y)) )
				return true;
			 else
				return false;
		} //else odd 
		else {
			if ( (loc.x == playerinfo.currPos.x + 1 && (loc.y == playerinfo.currPos.y + 1 || loc.y == playerinfo.currPos.y - 1 || loc.y == playerinfo.currPos.y))
				 || (loc.x == playerinfo.currPos.x && (loc.y == playerinfo.currPos.y + 1 || loc.y == playerinfo.currPos.y - 1))
				 || (loc.x == playerinfo.currPos.x - 1 && (loc.y == playerinfo.currPos.y)) )
				return true;
			else
				return false;
		}
	}//end checkPossibleMove



}
