using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {


	public Vector3Int currPos;
	float health = 50;
	float stamina = 50;
	int speed = 5;

	float max_health = 100;
	float max_stamina = 100;
	int max_speed = 5;

	public float getHealth(){
		return health;
	}
}
