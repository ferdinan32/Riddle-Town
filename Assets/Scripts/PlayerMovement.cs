﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 6.0F;
	
	Animator anime;
	private Quaternion lookLeft;
	private Quaternion lookRight;
	private Vector3 moveDirection = Vector3.zero;

	void Start(){
		anime = GetComponent<Animator>();

		lookRight = transform.rotation;
		lookLeft = lookRight * Quaternion.Euler(0, 180, 0); 
	}
	
	void FixedUpdate() {
		CharacterController controller = GetComponent<CharacterController>();
		
		anime.SetBool ("IsWalking", false);
		moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));			

		if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){				
			transform.rotation = lookLeft;
			moveDirection = transform.TransformDirection(0,0,-moveDirection.z);
			moveDirection *= speed;
			anime.SetBool ("IsWalking", true);
		}

		if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){				
			transform.rotation = lookRight;
			moveDirection = transform.TransformDirection(0,0,moveDirection.z);
			moveDirection *= speed;
			anime.SetBool ("IsWalking", true);
		}		
			
		controller.Move(moveDirection * Time.deltaTime);		
	}

}
