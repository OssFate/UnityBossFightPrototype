using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour {

    GameObject sceneCamera;

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();

        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (isLocalPlayer) {
            GetComponent<PlayerController>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        sceneCamera.SetActive(false);
    }

    public void OnDestroy() {
        sceneCamera.SetActive(true);
    }

}
