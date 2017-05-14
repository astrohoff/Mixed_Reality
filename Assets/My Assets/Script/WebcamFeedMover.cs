using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamFeedMover : MonoBehaviour {
	private string objectName = "Camera Model";

	private RealityCameraController camController;
	// Use this for initialization
	void Start () {
		camController = GameObject.Find (objectName).GetComponent<RealityCameraController>();
	}
	
	// Update is called once per frame
	void Update () {
		camController.SetDepth (transform.position);
	}
}
