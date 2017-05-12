using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTransformView : Photon.PunBehaviour, IPunObservable {
	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			transform.position = (Vector3)stream.ReceiveNext ();
			transform.rotation = (Quaternion)stream.ReceiveNext ();
		}
	}

}
