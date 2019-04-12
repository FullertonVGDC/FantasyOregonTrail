using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
	public Sprite grasslands;
	public Sprite caves;
	public Sprite towns;
	public Sprite volcanoes;
	public Sprite dungeon;
	public Sprite forest;
	public Sprite mountains;

	// Use this for initialization
	public void SetBackground (string tileName) {
		switch (tileName) {

		case "hexart_1_1": //towns
			this.GetComponent<SpriteRenderer> ().sprite = towns;
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
		case "hexart_1_8": //dungeon
			this.GetComponent<SpriteRenderer>().sprite = dungeon;
			break;
		case "hexart_1_9": //forest
			this.GetComponent<SpriteRenderer>().sprite = forest;
			break;
		case "hexart_1_10": //mountains
			this.GetComponent<SpriteRenderer>().sprite = mountains;
			break;
		default:
			break;
		}
	}

}
