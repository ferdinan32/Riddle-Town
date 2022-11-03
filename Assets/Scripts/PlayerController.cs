using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameObject character;
	public GameObject puzzleSign;

	public Vector3 positionStart;

	public static PlayerController instance;	

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Awake()
	{
		instance = this;
		StopMoving();		
	}

	public void InitializeGame(){		
		this.GetComponent<Character>().enabled=true;
		IdleAnimation();
	}

	public void StopMoving(){
		positionStart = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Respawn"){			
			MenuController.instance.GameOver(0);			
			Destroy(this.gameObject);
		}else if(other.tag=="Finish"){
			MenuController.instance.GameOver(1);
			Destroy(this.gameObject);
		}else if(other.tag=="Next"){
			SceneManager.LoadScene(MenuController.instance.stage+1);
		}else if(other.tag=="puzzle"){			
			if(!PuzzleScript.instance.isFinishPuzzle){
				MenuController.instance.isPuzzle = true;
				puzzleSign.SetActive(true);
			}else{
				MenuController.instance.isPuzzle = false;								
				Destroy(other.gameObject);
				PuzzleScript.instance.Initialize();
			}
		}else if(other.tag=="skor"){
			MenuController.instance.skor+=1;

			MenuController.instance.skorText.text="x"+(5-MenuController.instance.skor).ToString();
			if(MenuController.instance.stage==1){
				PlayerPrefs.SetFloat("skor",MenuController.instance.skor);
			}

			Destroy(other.gameObject);			
		}else if(other.tag=="dialog"){
			MenuController.instance.dialogObject.SetActive(true);
		}
		
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag=="puzzle"){
			if(!PuzzleScript.instance.isFinishPuzzle){
				MenuController.instance.isPuzzle = false;
				puzzleSign.SetActive(false);
			}else{
				MenuController.instance.isPuzzle = false;								
				Destroy(other.gameObject);
				PuzzleScript.instance.Initialize();
			}
		}else if(other.tag=="dialog"){
			MenuController.instance.dialogObject.SetActive(false);
		}
	}

	public void IdleAnimation(){
		character.GetComponent<Animator>().SetTrigger("idle");
		character.GetComponent<Animator>().SetBool("isRun",false);
	}
}
