using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {

	public Image splashImage;
	public Sprite[] splashSprite;
	StreamVideo videoScript;

	// Use this for initialization
	void Start () {
		videoScript = GetComponent<StreamVideo>();
		Invoke("ScreenOne",3);
	}

	void ScreenOne(){
		Invoke("ScreenTwo",3);
		splashImage.sprite = splashSprite[1];
	}
	void ScreenTwo(){
		Invoke("SceneGame",3);
		splashImage.sprite = splashSprite[2];
	}
	void ScreenVideo(){		
		Invoke("SceneGame",(float)videoScript.videoToPlay.length);
		videoScript.PlayPause();
	}
	void SceneGame(){
		CancelInvoke();
		SceneManager.LoadScene(1);
	}
}
