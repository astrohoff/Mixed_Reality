using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class RealityCameraController : Photon.PunBehaviour {
	public Camera sceneCam;
	public Transform webcamTranslation;
	public MeshRenderer webcamRenderer;

	private Vector2 xyFov = new Vector2(40, 40);
	private WebCamTexture webcamTexture;
		
	void Start () {
		// If mixed reality is being used.
		if (!VRSettings.isDeviceActive && WebCamTexture.devices.Length > 0) {
			// Setup webcam texture.
			WebCamTexture webcamTexture = new WebCamTexture ();
			webcamRenderer.material.mainTexture = webcamTexture;
			webcamTexture.Play ();
			// Setup scene camera.
			sceneCam.enabled = true;
			sceneCam.aspect = webcamTexture.width / (float)webcamTexture.height;
			// Calculate horizonal and vertical field of view.
			if (sceneCam.aspect > 1) {
				xyFov = new Vector2 (sceneCam.fieldOfView, sceneCam.fieldOfView / sceneCam.aspect);
			} else {
				xyFov = new Vector2 (sceneCam.fieldOfView * sceneCam.aspect, sceneCam.fieldOfView);
			}
			StartCoroutine (SetBackground (webcamRenderer.material));
		} else {
			sceneCam.enabled = false;
		}
	}

	private IEnumerator SetBackground(Material webcamMat){
		yield return new WaitForSeconds (1);
		Texture2D bgTex = new Texture2D (webcamTexture.width, webcamTexture.height);
		bgTex.SetPixels32 (webcamTexture.GetPixels32 ());
		bgTex.Apply ();
		webcamMat.SetTexture ("_BGTex", bgTex);
	}

	public void Update(){
		float depth = (webcamTranslation.position - sceneCam.transform.position).magnitude;
		float xWidth = Mathf.Tan (Mathf.Deg2Rad * xyFov.x) * depth * 2 / sceneCam.transform.localScale.x;
		float yWidth = Mathf.Tan (Mathf.Deg2Rad * xyFov.y) * depth * 2 / sceneCam.transform.localScale.y;
		webcamTranslation.localScale = new Vector3 (xWidth, yWidth, 1);
	}
		
	public void SetDepth(Vector3 headPosition){
		float depth = sceneCam.transform.InverseTransformPoint (headPosition).z;
		webcamTranslation.localPosition = new Vector3 (webcamTranslation.localPosition.x, webcamTranslation.localPosition.y, depth);
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (webcamTranslation.localPosition);
		} else {
			webcamTranslation.localPosition = (Vector3)stream.ReceiveNext ();
		}
	}
}
