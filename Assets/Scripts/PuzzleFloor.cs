using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFloor : MonoBehaviour {

	public GameObject puzzleObject;
	public GameObject portalObject;
	public int puzzleSolvedCoutn;
	public bool isPuzzle;
	int puzzleCount;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if(isPuzzle){
			if(other.tag=="floorRed"){
				puzzleObject.SetActive(false);			
			}

			if(other.tag=="floorBlu"){
				puzzleCount++;
				
				other.GetComponent<Renderer>().material.color = new Color(0,0,255,255);

				Destroy(other);

				if(puzzleCount!=int.Parse(other.name)){
					puzzleObject.SetActive(false);	
				}
				if(puzzleCount==puzzleSolvedCoutn){
					portalObject.SetActive(true);
				}
			}
		}
		else{
			if(other.tag=="floorRed"){
				other.GetComponent<Renderer>().material.color = new Color(255,0,0,255);
				StartCoroutine(DestroyFloor(other.gameObject));				
			}
		}		
	}

	IEnumerator DestroyFloor(GameObject _object){
		yield return new WaitForSeconds(1);
		Destroy(_object);
	}
}
