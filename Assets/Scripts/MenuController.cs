using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject panelMenu;
	public GameObject panelPause;
	public GameObject panelGameOver;
	public GameObject panelTutorial;
	public GameObject mainCamera;
	public GameObject followCamera;
	public GameObject dialogObject;
	public Text skorText;
	public Image starImage;
	public Image gameOverImage;
	public Sprite[] gameOverSprite;
	public Sprite[] tutorialSprite;
	public Slider volumeSound;
	public AudioSource gameOverSound;
	public AudioSource bgSound;
	public bool isPuzzle;
	public bool isPlayGame = false;
	public bool isPause;
	public static MenuController instance;
	bool isGameOver;
	public int stage;
	int tutorialCount;
	public float skor;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		instance = this;

		if(PlayerPrefs.GetInt("frist")==1){
			AudioListener.volume = PlayerPrefs.GetFloat("sound");
		}else{
			AudioListener.volume = 1;
		}
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{					
		if(PlayerPrefs.GetInt("restart")==1||stage>1){
			PlayGameStart();			
		}

		PlayerPrefs.SetInt("frist",1);

		if(stage==2){
			skor = PlayerPrefs.GetFloat("skor");
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(isGameOver){
			if(Input.GetKeyUp(KeyCode.Space)){
				RestartGame(1);
			}
		}
	}

	public void PlayGame(){
		mainCamera.SetActive(false);
		followCamera.SetActive(true);		
		panelMenu.SetActive(false);
		ShowTutorial(true);
	}

	public void PlayGameStart(){
		mainCamera.SetActive(false);
		followCamera.SetActive(true);		
		panelMenu.SetActive(false);
		ShowTutorial(false);
	}

	public void ShowTutorial(bool isShow){				
		if(!isShow){
			isPlayGame = true;
			PlayerController.instance.InitializeGame();
		}

		if(stage<=1){
			if(tutorialCount<tutorialSprite.Length-1){
				tutorialCount++;
				panelTutorial.SetActive(isShow);
				panelTutorial.GetComponent<Image>().sprite = tutorialSprite[tutorialCount];	
			}
			else
			{
				panelTutorial.SetActive(!isShow);
				isPlayGame = true;
				PlayerController.instance.InitializeGame();
			}
		}
	}

	public void ExitGame(){
		PlayerPrefs.DeleteKey("restart");
		Application.Quit();
	}

	public void RestartGame(int restartValue){
		Time.timeScale = 1;
		PlayerPrefs.SetInt("restart",restartValue);
		SceneManager.LoadScene(stage);
	}

	public void GoToMainMenu(){
		Time.timeScale = 1;
		PlayerPrefs.DeleteKey("restart");
		SceneManager.LoadScene(1);
	}

	public void OpenPuzzle(){
		if(isPuzzle && !PuzzleScript.instance.isFinishPuzzle){
			PuzzleScript.instance.puzzlePanle.SetActive(true);
			PlayerController.instance.StopMoving();
		}
	}

	public void OnPause(){
		isPause =! isPause;
		if(isPause){
			panelPause.SetActive(true);
			Time.timeScale = 0;
		}
		else{
			panelPause.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void OnChangeVolume(){
		AudioListener.volume = volumeSound.value;
		PlayerPrefs.SetFloat("sound",volumeSound.value);
	}

	public void GameOver(int indexSprite){		

		if(indexSprite==0){
			bgSound.Stop();
			gameOverSound.Play();
			isGameOver=true;
			PlayerPrefs.DeleteKey("skor");
		}
		else
		{
			if(stage==4){
				PlayerPrefs.DeleteKey("restart");
				PlayerPrefs.DeleteKey("skor");
				StartCoroutine(NextStage(0));				
			}else{
				StartCoroutine(NextStage(stage+1));
			}
		}

		gameOverImage.sprite = gameOverSprite[indexSprite];
		panelGameOver.SetActive(true);
		starImage.fillAmount=skor/5;
	}

	IEnumerator NextStage(int _stage){
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(_stage);
	}

	/// <summary>
	/// Callback sent to all game objects before the application is quit.
	/// </summary>
	void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("restart");
		PlayerPrefs.DeleteKey("skor");
	}

	public void EasterEgg(Text secret){
		if(secret.text=="secret"){
			SceneManager.LoadScene(5);
		}
	}
}
