using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[NetworkSettings(channel = 0, sendInterval = 0.1f)]
public class PlayerSyncTransform : NetworkBehaviour {

    [SyncVar (hook = "SyncPositionValues")]
    private Vector3 syncPos;
    private Transform myTransform;

    [SerializeField] float lerpRate = 15;
    [SerializeField] float normalLerpRate = 16;
    [SerializeField] float fasterLerpRate = 27;

    private Vector3 lastPos;
    [SerializeField] private float threshold = 0.5f;

    private List<Vector3> syncPositionList = new List<Vector3>();
    [SerializeField] private float closeEnough = 0.11f;

    void Start() {
        myTransform = transform;
    }

    void Update() {
        LerpPosition();
    }

    void FixedUpdate() {
        TransmitPosition();
    }

    void LerpPosition() {
        if (!isLocalPlayer) {
            HistoricalLerping();
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold) {
            CmdProvidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    [Client]
    void SyncPositionValues(Vector3 latestPos) {
        syncPos = latestPos;
        syncPositionList.Add(syncPos);
    }

    void HistoricalLerping() {
        if (syncPositionList.Count > 0) {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPositionList[0], Time.deltaTime * lerpRate);

            if (Vector3.Distance(myTransform.position, syncPositionList[0]) < closeEnough) {
                syncPositionList.RemoveAt(0);
            }

            lerpRate = syncPositionList.Count > 10 ? fasterLerpRate : normalLerpRate;
        }
    }

}
