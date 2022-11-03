using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	Animator animasiObject;
	public string Direction;

	void Awake () {
		animasiObject = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Player"){
			animasiObject.SetTrigger(Direction);
			other.GetComponent<Collider>().transform.SetParent(transform);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag=="Player"){
			other.GetComponent<Collider>().transform.SetParent(null);
		}
	}

}
