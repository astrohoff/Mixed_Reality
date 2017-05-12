using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class PlayerSpawnController : MonoBehaviour {
	public GameObject vrPlayerPrefab;

	void Start(){
		if (VRSettings.isDeviceActive) {
			Instantiate (vrPlayerPrefab, Vector3.zero, Quaternion.identity);
		}
	}
}
