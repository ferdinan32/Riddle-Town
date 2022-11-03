using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {
	
	Animator animasiObject;
	public int timeAnim;
	// Use this for initialization
	void Awake () {
		animasiObject = GetComponent<Animator>();		
	}

	void Start() {
		InvokingAnimation();
	}

	void InvokingAnimation() {
		Invoke("InvokingAnimation",timeAnim);
		animasiObject.SetTrigger("Hit");
	}
}
