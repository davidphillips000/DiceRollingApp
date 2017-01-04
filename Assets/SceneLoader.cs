using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public void LoadScene (string levelToLoad) {

		switch (levelToLoad) {
		case "LevelTest" :
			Application.LoadLevelAdditive("LevelTest");

			break;
		}

	}
}
