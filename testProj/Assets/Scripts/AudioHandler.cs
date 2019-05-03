using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {
	public AudioSource musicSrc;
	public AudioSource sndfxSrc;
	// Background Music
	public AudioClip map_msc;
	public AudioClip town_msc;
	public AudioClip boss_msc;
	public AudioClip camp_msc;
	public AudioClip end_msc;

	// Sound Effects
	public AudioClip swordClash_snd;
	public AudioClip goblinKnife_snd;
	public AudioClip gnollSword_snd;
	public AudioClip growl_snd;
	public AudioClip potion_snd;

	public void SetPlayMusic(string type){
		//if (musicSrc.isPlaying)
			//musicSrc.Stop;
		switch (type) {
		case "map":
			musicSrc.clip = map_msc;
			break;
		case "boss":
			musicSrc.clip = boss_msc;
			break;
		case "end":
			musicSrc.clip = end_msc;
			break;
		default:
			break;
		}

		musicSrc.Play ();
	}

	public void PlaySndFx(string type){

		switch (type) {
		case "swordClash":
			sndfxSrc.clip = swordClash_snd;
			break;
		case "goblinKnife":
			sndfxSrc.clip = goblinKnife_snd;
			break;
		case "gnollSword":
			sndfxSrc.clip = gnollSword_snd;
			break;
		case "growl":
			sndfxSrc.clip = growl_snd;
			break;
		case "potion":
			sndfxSrc.clip = potion_snd;
			break;
		default:
			break;
		}

		sndfxSrc.Play ();
	}

}
