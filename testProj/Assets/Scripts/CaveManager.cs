using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour {

	public string selectCaveStart(Vector3Int currPos){
		// 6 possible Cave locations
		if (currPos.x == 1 && currPos.y == -2) {
			return "CheckCave_1";
		} else if (currPos.x == 9 && currPos.y == -6) {
			return "CheckCave_2";
		} else if (currPos.x == 5 && currPos.y == 11) {
			return "CheckCave_3";
		} else if (currPos.x == 9 && currPos.y == 14) {
			return "CheckCave_4";
		} else if (currPos.x == 7 && currPos.y == 24) {
			return "CheckCave_5";
		} else if (currPos.x == -2 && currPos.y == 25) {
			return "CheckCave_6";
		} else
			return "";
	}

	public string selectCaveEnd(Vector3Int currPos){
		// 6 possible Cave locations
		if (currPos.x == 1 && currPos.y == -2) {
			return "EndCave_1";
		} else if (currPos.x == 9 && currPos.y == -6) {
			return "EndCave_2";
		} else if (currPos.x == 5 && currPos.y == 11) {
			return "EndCave_3";
		} else if (currPos.x == 9 && currPos.y == 14) {
			return "EndCave_4";
		} else if (currPos.x == 7 && currPos.y == 24) {
			return "EndCave_5";
		} else if (currPos.x == -2 && currPos.y == 25) {
			return "EndCave_6";
		} else
			return "";
	}

}
