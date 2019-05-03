using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
	public Sprite grasslands; // & hillands
	public Sprite caves;
	public Sprite towns;
	public Sprite volcanoes;
	public Sprite dungeon;
	public Sprite forest;
	public Sprite mountains;
	public Sprite palace;
	public Sprite insidePalace;
	public GameObject foreground;

	// Use this for initialization
	public void SetBackground (string tileName, bool isBack = true) {
		if (isBack)
			foreground.GetComponent<BackgroundManager> ().SetBackground (tileName, false);
		
		switch (tileName) {

		case "hexart_1_1": //towns
			this.GetComponent<SpriteRenderer> ().sprite = towns;
			break;
		case "PreBossFight2":
		case "hexart_1_2": //palace
			this.GetComponent<SpriteRenderer> ().sprite = palace;
			break;
		case "hexart_1_3": //hills
		case "hexart_1_4": //grassland
			this.GetComponent<SpriteRenderer>().sprite = grasslands;
			break;
		case "hexart_1_6": // Hidden Cave
			this.GetComponent<SpriteRenderer>().sprite = caves;
			break;
		case "hexart_1_7": //Volcanoes
			this.GetComponent<SpriteRenderer>().sprite = volcanoes;
			break;
		case "PreBossFight":
		case "hexart_1_8": //dungeon
			this.GetComponent<SpriteRenderer>().sprite = dungeon;
			break;
		case "hexart_1_9": //forest
			this.GetComponent<SpriteRenderer>().sprite = forest;
			break;
		case "hexart_1_10": //mountains
			this.GetComponent<SpriteRenderer>().sprite = mountains;
			break;
		case "undead":
		case "insidePalace":
			this.GetComponent<SpriteRenderer>().sprite = insidePalace;
			break;
		default:
			break;
		}
	}

}
