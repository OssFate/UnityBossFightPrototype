using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour {

    PlayerController playerController;
    MeshRenderer playerRender;

    void Start () {
        playerController = GetComponent<PlayerController>();
        if (!isLocalPlayer) {
            playerController.enabled = false;
        }
	}

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();

        playerRender = GetComponent<MeshRenderer>();

        playerRender.material.color = Color.blue;
    }

}
