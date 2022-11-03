using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScript : MonoBehaviour {

	public Sprite[] puzzleImage;
	public Sprite[] truePuzzleSprite;
	public Image[] truePuzzleImage;
	public Text moveCountText;
	public GameObject finishPuzzle;
	public GameObject puzzlePanle;
	public GameObject portal;
	public static PuzzleScript instance;
	public bool isFinishPuzzle;
	bool isSolved;
	int puzzleMove;


	void Start()
	{
		instance = this;
		Initialize();		
	}

	public void Initialize(){
		isFinishPuzzle = false;
		isSolved = false;
		puzzleMove=9;
		moveCountText.text = puzzleMove.ToString();
		for (int i = 0; i < truePuzzleImage.Length; i++)
		{
			truePuzzleImage[i].sprite = puzzleImage[Random.RandomRange(0,puzzleImage.Length)];
			truePuzzleImage[i].GetComponent<Button>().interactable = true;
		}
	}

	void Update(){
		if(!isSolved){
			if((truePuzzleImage[0].sprite.name=="1" && truePuzzleImage[3].sprite.name=="3" && truePuzzleImage[8].sprite.name=="3" && truePuzzleImage[4].sprite.name=="2" && truePuzzleImage[5].sprite.name=="1")){
				PuzzleSolved(1);
			}else if((truePuzzleImage[0].sprite.name=="1" && truePuzzleImage[3].sprite.name=="3" && truePuzzleImage[8].sprite.name=="2" && truePuzzleImage[4].sprite.name=="1" && truePuzzleImage[7].sprite.name=="3")){
				PuzzleSolved(0);
			}else if((truePuzzleImage[0].sprite.name=="2" && truePuzzleImage[1].sprite.name=="1" && truePuzzleImage[4].sprite.name=="3" && truePuzzleImage[5].sprite.name=="1" && truePuzzleImage[8].sprite.name=="3")){
				PuzzleSolved(2);
			}
			else{
				isFinishPuzzle=false;
			}
		}

		if(puzzlePanle.activeInHierarchy){
			PlayerController.instance.transform.position = PlayerController.instance.positionStart;
		}
	}

	public void ChangePuzzle(Image puzzleObj){
		puzzleMove--;
		int index = int.Parse(puzzleObj.sprite.name);
		index+=1;

		if(index>puzzleImage.Length){
			index=1;
		}

		puzzleObj.sprite = puzzleImage[index-1];

		if(puzzleMove==0){
			puzzlePanle.SetActive(false);
			MenuController.instance.GameOver(0);
		}

		moveCountText.text = puzzleMove.ToString();
	}

	void PuzzleSolved(int index){
		isSolved=true;
		isFinishPuzzle=true;

		if(index==1){
			truePuzzleImage[0].sprite = truePuzzleSprite[0];
			truePuzzleImage[3].sprite = truePuzzleSprite[2];
			truePuzzleImage[5].sprite = truePuzzleSprite[0];
			truePuzzleImage[8].sprite = truePuzzleSprite[2];
			truePuzzleImage[4].sprite = truePuzzleSprite[1];
		}
		else if(index==2){
			truePuzzleImage[0].sprite = truePuzzleSprite[1];
			truePuzzleImage[1].sprite = truePuzzleSprite[0];
			truePuzzleImage[4].sprite = truePuzzleSprite[2];
			truePuzzleImage[5].sprite = truePuzzleSprite[0];
			truePuzzleImage[8].sprite = truePuzzleSprite[2];
		}
		else{
			truePuzzleImage[0].sprite = truePuzzleSprite[0];
			truePuzzleImage[3].sprite = truePuzzleSprite[2];
			truePuzzleImage[7].sprite = truePuzzleSprite[2];
			truePuzzleImage[8].sprite = truePuzzleSprite[1];
			truePuzzleImage[4].sprite = truePuzzleSprite[0];
		}

		for (int i = 0; i < truePuzzleImage.Length; i++)
		{
			truePuzzleImage[i].GetComponent<Button>().interactable = false;
		}

		finishPuzzle.SetActive(true);		
		StartCoroutine(WaitFinishPuzzle());

		if(portal!=null){
			portal.SetActive(true);
		}
	}

	IEnumerator WaitFinishPuzzle(){
		PlayerController.instance.puzzleSign.SetActive(false);
		yield return new WaitForSeconds(2);
		PlayerController.instance.InitializeGame();		
		finishPuzzle.SetActive(false);
		puzzlePanle.SetActive(false);
	}
}
