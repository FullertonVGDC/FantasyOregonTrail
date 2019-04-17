using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Fungus; // Try from other script

public class GameManager_1 : MonoBehaviour {

	public Flowchart flowchart;

	public int gameTime = 0;
	public Vector3Int startPos = new Vector3Int (0, 0, 0);

	public Grid grid;
	public GameObject playerObj;

	private GridScript gridinfo;
	public Tilemap hiddenTiles;
	protected PlayerScript playerinfo;

	public GameObject lvlLoad;
	public GameObject pauseMenu;
	public Text PauseText;
	List<string> logList;
	public Text log_whole;
	public Button enterTownBTN;
	public Button potionBTN;

	private BattleManager battleMGR;

	public Camera mainCam;
	public int camSpeed = 5;

	private bool battleOver = true;
	private bool canMove = true;

	public Animator battleTranAnim;


	void Awake() {
		battleMGR = this.GetComponent<BattleManager> ();
		gridinfo = grid.GetComponent<GridScript> ();
		playerinfo = playerObj.GetComponent<PlayerScript> ();

		SaveStateScript.saveControl.Load(playerinfo);
		//playerinfo.currPos = startPos; // replace with last saved location
		playerObj.transform.SetPositionAndRotation (grid.CellToWorld(playerinfo.currPos), Quaternion.identity);
		mainCam.transform.position = playerObj.transform.position + new Vector3(0,0,-1);
	}
	// Use this for initialization
	void Start () {
		logList = new List<string>(); // refresh List
		//SaveStateScript.saveControl.Load(playerinfo.setMaxHealth(),playerinfo.setMaxStamina(),playerinfo.setStrength(),playerinfo.setSpeed(),playerinfo.setRenown(),startPos,playerinfo.upgrades);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Y)) {battleTranAnim.SetTrigger("BattleTranTrigger");}

		if (battleOver && Input.GetMouseButtonUp (0)) {
			gridinfo.getTile ();
			movePlayer ();
			//checkHiddenTiles();
		}

		if (Input.GetKeyUp (KeyCode.Escape)) {
			setPauseMenu ();				
		}

		//move camera left
		if (Input.GetKey (KeyCode.A) && mainCam.transform.position.x  > -2.75) {
			mainCam.transform.transform.position -= (new Vector3(camSpeed * Time.deltaTime,0,0));
		}
		if (Input.GetKey (KeyCode.D) && mainCam.transform.position.x  < 17.75) {
			mainCam.transform.transform.position += (new Vector3(camSpeed * Time.deltaTime,0,0));
		}
		if (Input.GetKey (KeyCode.W) && mainCam.transform.position.y  < 8.75) {
			mainCam.transform.transform.position += (new Vector3(0,camSpeed * Time.deltaTime,0));
		}
		if (Input.GetKey (KeyCode.S) && mainCam.transform.position.y  > -4.5) {
			mainCam.transform.transform.position -= (new Vector3(0,camSpeed * Time.deltaTime,0));
		}

		if (playerinfo.getHealth () <= 0) {
			Debug.Log ("Game Over!");
			playerinfo.addHealth (100);
		}

	}

	void movePlayer() {
		Vector3 cellPoint = grid.CellToWorld (gridinfo.clickedPos);
		bool stop = false;
		if (gridinfo.tileName == "hexart_1_8") {
			flowchart.ExecuteBlock ("LoadVariables");
			int prog = flowchart.GetIntegerVariable ("StoryProg");
			if (prog < 3) {
				startConversation ("WL_notReady");
				//moveToPrev ();
				stop = true;
			}
		}
		//make sure tile is land one space away
		if (canMove && (gridinfo.tileName != "hexart_1_11") && (gridinfo.tileName != "hexart_1_12") && checkPossibleMove(gridinfo.clickedPos) && !stop) {
			//remove enter town button (will reappear if moving to a town)
			if(enterTownBTN.IsActive()) { 
				enterTownBTN.gameObject.SetActive(false); 
			}
			//move player
			Debug.Log("Cell Point: " + cellPoint);
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

	void checkHiddenTiles(Vector3Int loc)
	{
		// Also handled in PlayerScript.cs
		hiddenTiles.SetTile (loc, null);
	}

	void randomEncounter(string tileName){
		float rand = Random.Range(1,100);

		switch (tileName) {
		case "hexart_1_1": //town
			playerinfo.setStamina (playerinfo.getMaxStamina ());
			playerinfo.setHealth (playerinfo.getMaxHealth ());
			potionBTN.interactable = true;
			SaveStateScript.saveControl.Save(playerinfo.getMaxHealth(),playerinfo.getMaxStamina(),playerinfo.getStrength(),playerinfo.getSpeed(),playerinfo.getRenown(),playerinfo.currPos,playerinfo.upgrades);
			enterTownBTN.gameObject.SetActive(true);
			break;
		case "hexart_1_2": // Ice Castle
			flowchart.ExecuteBlock ("LoadVariables");
			int prog2 = flowchart.GetIntegerVariable("StoryProg");
			if(prog2 < 5){
				startConversation("WL_notReady");
				moveToPrev();
			}
			else
				startConversation("Final_1");
			break;
		case "hexart_1_3": //hills
		case "hexart_1_4": //grassland
			if (rand < 10f) //10% chance
			{
				//playerinfo.addStamina(-10);
				AddLogItem("You're out of shape. [-10ST]\n");
			}
			else if (rand < 20f) //10% chance
			{
				//playerinfo.addStamina(10);
				//playerinfo.addHealth (5);
				AddLogItem("You find a relaxing spot to rest. [+10Stm, +5Hp]\n");
			}
			else if (rand < 30f) //10% chance
			{	StartCoroutine (BattleControl (tileName)); }
			break;
		case "hexart_1_6": // Hidden Cave
			//reveal hidden space
			AddLogItem ("You find a hidden Cave!\n");
			string cave = this.GetComponent<CaveManager>().selectCaveStart(playerinfo.currPos);
			startConversation(cave);
			//StartCoroutine (BattleControl (tileName)); //handled in fightConversation
			checkHiddenTiles(playerinfo.currPos);
			break;
		case "hexart_1_7": //Volcanoes
			if (rand < 50f)
			{
				//playerinfo.addHealth(-20);
				//playerinfo.addStamina(-20);
				AddLogItem("The Volcano erupts sending lava and molten rock everywhere. [-20Hp, -40Stm]\n");
			}
			else if (rand < 80f) //30% chance
			{	StartCoroutine(BattleControl (tileName)); }
			break;
		case "hexart_1_8": //dungeon
			flowchart.ExecuteBlock ("LoadVariables");
			int prog = flowchart.GetIntegerVariable("StoryProg");
			if(prog < 3){
				startConversation("WL_notReady");
				moveToPrev();
			}
			else if (prog == 3)
				startConversation("WL_start");
				//StartCoroutine(BattleControl (tileName)); // will auto trigger from flowchart block
			break;
		case "hexart_1_9": //forest
			if (rand < 10f) //10% chance
			{	StartCoroutine(BattleControl (tileName));  }
			break;
		case "hexart_1_10": //mountains
			if (rand < 10f) //10% chance
			{
				//playerinfo.addHealth(-10);
				AddLogItem("You simply suck at this. [-10HP]\n");
			}
			else if (rand < 30f) //20% chance
			{StartCoroutine(BattleControl (tileName)); }
			break;
		default:
			break;
		}
	}

	// Access BattleManager script to control battle flow
	private IEnumerator BattleControl(string battleLoc, bool isStory = false) {
		AddLogItem("Fight for your right to party!\n");
		battleTranAnim.SetTrigger("BattleTranTrigger"); // Battle Animation
			//yield return StartCoroutine(WaitForAnimation(battleTranAnim)); // wait for Animation to Finish
		log_whole.transform.parent.gameObject.SetActive (false);
		battleOver = false;
		yield return new WaitForSeconds(2f); // wait for Animation to Finish
		yield return StartCoroutine(battleMGR.SetupBattle (battleLoc)); //Check if stays here
		//log_whole.transform.parent.gameObject.SetActive (true);
		AddLogItem("The Battle has ended!\n");
		battleOver = true;

		if (isStory) // Possibly use Enums to determine which story
			StartCoroutine(storyProgression ());
	}

	public IEnumerator storyProgression(){
		flowchart.ExecuteBlock ("LoadVariables");
		int prog = flowchart.GetIntegerVariable ("StoryProg");
		switch (prog) {
		case 2:
			startConversation ("GC_BattleOver");
			break;
		case 4:
			startConversation ("WL_AfterBattle");
			break;
		case 6:
			startConversation ("Final_2");
			break;
		case 7:
			startConversation ("Final_3");
			break;
		case 8:
			startConversation ("Final_4");
			break;
		default:
			break;
		}
		yield return null;
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

	public void EnterTownOnClick(){
		Vector3Int location = playerinfo.currPos;
		if (location.x==0 && location.y==0) {
			lvlLoad.GetComponent<SwitchScenes> ().LoadScene ("TownScene_1");
		}
		else if (location.x == -4 && location.y == -5) {
			lvlLoad.GetComponent<SwitchScenes> ().LoadScene ("TownScene_2");
		}
		else if (location.x == 8 && location.y == -7) {
			lvlLoad.GetComponent<SwitchScenes> ().LoadScene ("TownScene_3");
		}
		else if (location.x == 2 && location.y == 5) {
			lvlLoad.GetComponent<SwitchScenes> ().LoadScene ("TownScene_4");
		}
		else if (location.x == -4 && location.y == 23) {
			lvlLoad.GetComponent<SwitchScenes> ().LoadScene ("TownScene_5");
		}
	}
		

	private IEnumerator CampFireScene(){
		Debug.Log ("Campfire Scene Happens");
		yield return null;
	}
		
	public void startConversation(string block){ canMove = false; flowchart.ExecuteBlock (block);	}
	public void endConversation(){ Debug.Log ("convo ended"); canMove = true; }
	public void fightConversation(string tileName, bool isStory = false){
		canMove = true;
		StartCoroutine (BattleControl (tileName, isStory));
	}

	public void setPauseMenu(){
		if (pauseMenu.activeSelf) {
			pauseMenu.SetActive (false);
			canMove = true;
		}
		else {
			canMove = false;
			PauseText.text = "Stats:\nHealth: " + playerinfo.getHealth () + " / " + playerinfo.getMaxHealth ()
				+ "\nStamina: " + playerinfo.getStamina () + " / " + playerinfo.getMaxStamina ()
				+ "\nSpeed: " + playerinfo.getSpeed () + "\nStrength: " + playerinfo.getStrength ()
				+ "\nRenown: " + playerinfo.getRenown ();
			pauseMenu.SetActive (true);
		}
	}

	public void moveToPrev(){
		Vector3 cellPoint = grid.CellToWorld (gridinfo.prevPos);
		playerObj.transform.SetPositionAndRotation (cellPoint, Quaternion.identity);
		playerinfo.currPos = gridinfo.prevPos;
		gridinfo.resetPrevTile ();
	}

	public void usePotionOnClick(){
		playerinfo.addHealth (20);
		potionBTN.interactable = false;
	}

	private IEnumerator WaitForAnimation(Animation anim){
		//Animation temp = anim.GetComponent<Animation> ();
		do {
			yield return null;
		} while(  anim.isPlaying);
	}
}
