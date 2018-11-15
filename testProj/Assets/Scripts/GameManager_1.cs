using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager_1 : MonoBehaviour {

	public int gameTime = 0;

	public Grid grid;
	public GameObject playerObj;

	private GridScript gridinfo;
	protected PlayerScript playerinfo;

	private BattleManager battleMGR;

	public Camera mainCam;
	public int camSpeed = 5;

	private bool battleOver = true;

	void Awake() {
		battleMGR = this.GetComponent<BattleManager> ();
		gridinfo = grid.GetComponent<GridScript> ();
		playerinfo = playerObj.GetComponent<PlayerScript> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (battleOver && Input.GetMouseButtonUp (0)) {
			gridinfo.getTile ();
			movePlayer ();
			//checkHiddenTiles();
		}

		//move camera left
		if (Input.GetKey (KeyCode.A)) {
			mainCam.transform.transform.position -= (new Vector3(camSpeed * Time.deltaTime,0,0));
		}
		if (Input.GetKey (KeyCode.D)) {
			mainCam.transform.transform.position += (new Vector3(camSpeed * Time.deltaTime,0,0));
		}
		if (Input.GetKey (KeyCode.W)) {
			mainCam.transform.transform.position += (new Vector3(0,camSpeed * Time.deltaTime,0));
		}
		if (Input.GetKey (KeyCode.S)) {
			mainCam.transform.transform.position -= (new Vector3(0,camSpeed * Time.deltaTime,0));
		}

		if (gameTime >= 24) {
			Debug.Log ("New Day!");
			gameTime = 0;
		}

		if (playerinfo.getHealth () <= 0) {
			Debug.Log ("Game Over!");
			playerinfo.addHealth (100);
		}
	}

	void movePlayer() {
		Vector3 cellPoint = grid.CellToWorld (gridinfo.clickedPos);
		//make sure tile is land one space away
		if ((gridinfo.tileName != "hexart_1_5") && checkPossibleMove(gridinfo.clickedPos)) {
			//move player
			playerObj.transform.SetPositionAndRotation (cellPoint, Quaternion.identity);
			playerinfo.currPos = gridinfo.clickedPos;
			//add player time
			gameTime += gridinfo.getTileTime ();
			//check for random ecounter
			randomEncounter(gridinfo.tileName);
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

	void checkHiddenTiles()
	{

	}

	void randomEncounter(string tileName){
		float rand = Random.value;

		switch (tileName) {
		case "hexart_1_3": //grassland
			if (rand < 0.1f) //10% chance
			{
				playerinfo.addStamina(-10);
				Debug.Log("You're out of shape. [-10ST]");//do something
			}
			break;
		case "hexart_1_4": //tallgrass
			break;
		case "hexart_1_9": //forest
			if (rand < 0.9f) //10% chance
			{
				StartCoroutine(BattleControl (tileName));
			}
			break;
		case "hexart_1_10": //mountains
			if (rand < 0.1f) //10% chance
			{
				playerinfo.addHealth(-10);
				Debug.Log("You simply suck at this. [-10HP]");//do something
			}
			break;
		default:
			break;
		}
	}

	// Access BattleManager script to control battle flow
	private IEnumerator BattleControl(string battleLoc) {
		Debug.Log("Fight for your right to party!");
		battleOver = false;
		yield return StartCoroutine(battleMGR.SetupBattle (battleLoc)); //Check if stays here
		Debug.Log("The Battle has ended!");
		battleOver = true;
	}

}
