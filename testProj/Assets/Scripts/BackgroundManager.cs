using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
	public Sprite grasslands;
	public Sprite caves;

	// Use this for initialization
	public void SetBackground (string tileName) {
		switch (tileName) {

		case "hexart_1_3": //hills
		case "hexart_1_4": //grassland
			this.GetComponent<SpriteRenderer>().sprite = grasslands;
			break;
		case "hexart_1_6": // Hidden Cave
			this.GetComponent<SpriteRenderer>().sprite = caves;
			break;
		case "hexart_1_7": //Volcanoes
			break;
		case "hexart_1_8": //dungeon
			break;
		case "hexart_1_9": //forest
			break;
		case "hexart_1_10": //mountains
			break;
		default:
			break;
		}
	}

}
