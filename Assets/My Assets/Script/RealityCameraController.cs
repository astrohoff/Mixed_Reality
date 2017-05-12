using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class RealityCameraController : MonoBehaviour {
	public Camera cam;
	public MeshRenderer webcamFeed;

	// Use this for initialization
	void Start () {
		if (!VRSettings.isDeviceActive && WebCamTexture.devices.Length > 0) {
			cam.enabled = true;
			cam.aspect = (4f / 3);
			Material webcamMat = new Material ("Unlit/Transparent Cutout");
			webcamFeed.material = webcamMat;
			webcamMat.mainTexture = new WebCamTexture ();
		} else {
			cam.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
