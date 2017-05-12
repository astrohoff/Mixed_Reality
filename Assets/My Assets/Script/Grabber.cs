using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {
	public bool isLeft = true;

	bool grabbing = false;
	private GrabObjectController grabTarget = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckGrabDown ()) {
			if (!grabbing) {
				if (grabTarget != null) {
					grabbing = grabTarget.Grab(transform);
				}
			}
		}
		if (CheckGrabUp ()) {
			if (grabbing) {
				if (grabTarget != null) {
					grabTarget.Release();
					grabbing = false;
				}
			}
		}
	}

	private bool CheckGrabDown(){
		if (isLeft) {
			return OVRInput.GetDown (OVRInput.Button.PrimaryHandTrigger);
		} else {
			return OVRInput.GetDown (OVRInput.Button.SecondaryHandTrigger);
		}
	}

	private bool CheckGrabUp(){
		if (isLeft) {
			return OVRInput.GetUp (OVRInput.Button.PrimaryHandTrigger);
		} else {
			return OVRInput.GetUp (OVRInput.Button.SecondaryHandTrigger);
		}
	}


	private void OnTriggerStay(Collider c){
		if (!grabbing) {
			if (grabTarget == null) {
				grabTarget = c.GetComponent<GrabObjectController> ();
			}
		}
	}

	private void OnTriggerExit(Collider c){
		if (!grabbing) {
			if (grabTarget != null) {
				if (c.gameObject.GetComponent<GrabObjectController>() == grabTarget) {
					grabTarget = null;
				}
			}
		}
	}
}
