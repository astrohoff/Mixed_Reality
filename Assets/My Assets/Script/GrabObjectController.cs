using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectController : MonoBehaviour {
	Transform ungrabbedParent;
	bool grabbed = false;

	// Use this for initialization
	void Start () {
		ungrabbedParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool Grab(Transform grabber){
		if (!grabbed) {
			transform.parent = grabber;
			grabbed = true;
			return true;
		}
		return false;
	}

	public void Release(){
		transform.parent = ungrabbedParent;
		grabbed = false;
	}
}
