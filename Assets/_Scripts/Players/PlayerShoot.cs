using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerShoot : NetworkBehaviour {

    private Camera playerCamera;

    void Start() {
        playerCamera = transform.GetChild(0).GetComponent<Camera>();
        Debug.Log(playerCamera);
    }

    void Update () {
	    CheckIfShooting();
	}

    private void CheckIfShooting() {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
        }
    }

    private void Shoot() {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.transform.tag);
            Debug.Log(hit.point);
            Debug.Log(hit.distance);
        }
    }
}
