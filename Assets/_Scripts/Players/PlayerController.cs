using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CollisionInfo collisions;
    private BoxCollider _collider;
    private float distToGroud;

    private Rigidbody playerRigidBody;
    private Vector3 velocity;
    
    public float rotationSpeed;
    public float moveSpeed;
    public float jumpSpeed;

    void Start () {
        playerRigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        distToGroud = _collider.bounds.extents.y;
    }

	void Update () {
        UpdateRayCast();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.up * (moveHorizontal * rotationSpeed * Time.deltaTime);
        transform.Rotate(rotation);

        Vector3 translation = Vector3.forward * (moveVertical * moveSpeed * Time.deltaTime);
        transform.Translate(translation);

        if (Input.GetButtonDown("Jump") && collisions.below) {
            Vector3 jumpTranslation = Vector3.up * jumpSpeed * Time.deltaTime;
            transform.Translate(jumpTranslation, Space.World);
        }
    }

    private void UpdateRayCast() {
        collisions.below = Physics.Raycast(transform.position, -Vector3.up, distToGroud + 0.1f) ? true : false;
    }

    public struct CollisionInfo {
        public bool below;

        public void Reset() {
            below = false;
        }
    }
}
