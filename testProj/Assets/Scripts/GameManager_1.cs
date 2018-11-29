using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager_1 : MonoBehaviour {

	public int gameTime = 0;

	public Grid grid;
	public GameObject playerObj;

	private GridScript gridinfo;
	protected PlayerScript playerinfo;

	public GameObject pauseMenu;
	public Text PauseText;
	List<string> logList;
	public Text log_whole;

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
		logList = new List<string>(); // refresh List
	}
	
	// Update is called once per frame
	void Update () {
		if (battleOver && Input.GetMouseButtonUp (0)) {
			gridinfo.getTile ();
			movePlayer ();
			//checkHiddenTiles();
		}

		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (pauseMenu.activeSelf)
				pauseMenu.SetActive (false);
			else {
				PauseText.text = "Stats:\nHealth: " + playerinfo.getHealth () + " / " + playerinfo.getMaxHealth ()
				+ "\nStamina: " + playerinfo.getStamina () + " / " + playerinfo.getMaxStamina ()
				+ "\nSpeed: " + playerinfo.getSpeed () + "\nStrength: " + playerinfo.getStrength ();
				pauseMenu.SetActive (true);
			}
				
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
			if (gameTime >= 24) {
				StartCoroutine(CampFireScene());
				AddLogItem ("New Day!\n");
				gameTime = 0;
			} else {
				//check for random ecounter
				randomEncounter(gridinfo.tileName);
				Debug.Log ("location = " + gridinfo.tileName);
			}

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
		float rand = Random.Range(1,100);

		switch (tileName) {
		case "hexart_1_1": //town
			playerinfo.setStamina (100);
			playerinfo.setHealth (100);
			// go to town scene
			break;
		case "hexart_1_3": //hills
		case "hexart_1_4": //grassland
			if (rand < 10f) //10% chance
			{
				playerinfo.addStamina(-10);
				AddLogItem("You're out of shape. [-10ST]\n");
			}
			else if (rand < 20f) //10% chance
			{
				playerinfo.addStamina(10);
				playerinfo.addHealth (5);
				AddLogItem("You find a relaxing spot to rest. [+10Stm, +5Hp]\n");
			}
			break;
		case "hexart_1_6": //cave
			//reveal hidden space
			AddLogItem ("You find a hidden Cave!\n");
			StartCoroutine (BattleControl (tileName));
			break;
		case "hexart_1_7": //Volcanoes
			if (rand < 50f)
			{
				playerinfo.addHealth(-20);
				playerinfo.addStamina(-20);
				AddLogItem("The Volcano erupts sending lava and molten rock everywhere. [-20Hp, -40Stm]\n");
			}
			else if (rand < 70f) //20% chance
			{	StartCoroutine(BattleControl (tileName)); }
			break;
		case "hexart_1_8": //dungeon
			StartCoroutine(BattleControl (tileName));
			break;
		case "hexart_1_9": //forest
			if (rand < 10f) //10% chance
			{	StartCoroutine(BattleControl (tileName));  }
			break;
		case "hexart_1_10": //mountains
			if (rand < 10f) //10% chance
			{
				playerinfo.addHealth(-10);
				AddLogItem("You simply suck at this. [-10HP]\n");
			}
			else if (rand < 30f) //20% chance
			{	StartCoroutine(BattleControl (tileName)); }
			break;
		default:
			break;
		}
	}

	// Access BattleManager script to control battle flow
	private IEnumerator BattleControl(string battleLoc) {
		AddLogItem("Fight for your right to party!\n");
		log_whole.transform.parent.gameObject.SetActive (false);
		battleOver = false;
		yield return StartCoroutine(battleMGR.SetupBattle (battleLoc)); //Check if stays here
		log_whole.transform.parent.gameObject.SetActive (true);
		AddLogItem("The Battle has ended!\n");
		battleOver = true;
	}

	public void AddLogItem(string txt=""){
		logList.Add (txt);
		Debug.Log (txt);
		log_whole.text = "";
		if (logList.Count > 5)
			logList.RemoveAt (0);
		for(int i=0; i < logList.Count; i++){
			log_whole.text += logList [i];
		}
	}


	private IEnumerator CampFireScene(){
		Debug.Log ("Campfire Scene Happens");
		yield return null;
	}
}
