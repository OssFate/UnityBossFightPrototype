using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

    private Rigidbody playerRigidBody;
    private Vector3 velocity;

    public float speed;

    void Start () {
        playerRigidBody = GetComponent<Rigidbody>();
    }

	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        velocity = new Vector3(moveHorizontal, 0, moveVertical);

        transform.Translate(velocity*speed*Time.deltaTime);
	}
}
