using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody playerRigidBody;
    private Vector3 velocity;
    
    public float rotationSpeed;
    public float moveSpeed;
    public float jumpSpeed;

    void Start () {
        playerRigidBody = GetComponent<Rigidbody>();
    }

	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.up * (moveHorizontal * rotationSpeed * Time.deltaTime);
        transform.Rotate(rotation);

        Vector3 translation = Vector3.forward * (moveVertical * moveSpeed * Time.deltaTime);
        transform.Translate(translation);

        if (Input.GetButtonDown("Jump")) {
            Vector3 jumpTranslation = Vector3.up * jumpSpeed * Time.deltaTime;
            transform.Translate(jumpTranslation, Space.World);
        }
    }
}
