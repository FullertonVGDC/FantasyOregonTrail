using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

	public Animator animator;

	private string levelName;

	public void LoadScene(string name) {
		levelName = name;
		animator.SetTrigger ("FadeOut");

	}

	public void OnFadeComplete () {
		SceneManager.LoadScene (sceneName: levelName);
	}

	public void QuitToMainMenu(GameObject obj) {
		levelName = "MainMenu_Scene";
		PlayerScript plr = obj.GetComponent<PlayerScript> ();

		SaveStateScript.saveControl.Save (plr.getMaxHealth (), plr.getMaxStamina (), plr.getStrength (), plr.getSpeed (), plr.getRenown (), plr.currPos, plr.upgrades);
		animator.SetTrigger ("FadeOut");

	}	

	public void QuitGame() {
		Application.Quit ();
	}


}
