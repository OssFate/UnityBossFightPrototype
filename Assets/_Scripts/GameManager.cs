using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour {

	public GameObject BossType1;

    public void Spawn() {
        GameObject CubeBoss = (GameObject)Instantiate(BossType1, new Vector3(0, 5, 0), Quaternion.identity);
        NetworkServer.Spawn(CubeBoss);
    }

    public override void OnStartServer() {
        base.OnStartServer();
        Spawn();
    }

}
