using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CollisionInfo collisions;
    private BoxCollider _collider;
    private float distToGroud;
    private float distToFront;

    private Rigidbody playerRigidBody;
    private Vector3 velocity;
    
    public float rotationSpeed = 70;
    public float moveSpeed = 10;
    public float jumpHeight = 10;
    public float timeToJump = 3f;
    public float minJumpVelocity = 10.0f;
    private bool checkBelow = true;
    private float gravity;
    private float initialYPosition = 1;

    void Start () {
        playerRigidBody = GetComponent<Rigidbody>();

        // Required variables to Jump
        _collider = GetComponent<BoxCollider>();
        distToGroud = _collider.bounds.extents.y;
        distToFront = _collider.bounds.extents.z;

        // Find the acceleration with a = 2x/t^2        
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJump, 2);

        // Initial velocity
        velocity = new Vector3(0, 0, 0);
    }

	void Update () {
        UpdateRayCast();

        checkBelow = true;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotation
        transform.Rotate(Vector3.up * (moveHorizontal * rotationSpeed * Time.deltaTime));

        // Translation
        if (Input.GetButtonDown("Jump") && collisions.below) {
            velocity.y = minJumpVelocity;
            checkBelow = false;
        }

        velocity.z = moveVertical * moveSpeed * Time.deltaTime;
        velocity.y += collisions.below ? 0 : gravity * Time.deltaTime;

        if (collisions.below && checkBelow) {
            velocity.y = 0;
        }

        if ((collisions.front && velocity.z > 0) || (collisions.back && velocity.z < 0)) {
            velocity.z = 0;
        }

        transform.Translate(velocity);
    }

    private void UpdateRayCast() {
        collisions.below = Physics.Raycast(transform.position, -Vector3.up, distToGroud + 0.1f) ? true : false;
        collisions.front = Physics.Raycast(transform.position, transform.forward, distToFront + 0.3f) ? true : false;
        collisions.back = Physics.Raycast(transform.position, -transform.forward, distToFront + 0.3f) ? true : false;
        Debug.DrawLine(transform.position, transform.position + (transform.forward * (distToFront + 0.3f)));
        Debug.DrawLine(transform.position, transform.position + (-transform.forward * (distToFront + 0.3f)));
    }

    public struct CollisionInfo {
        public bool below;
        public bool front;
        public bool back;

        public void Reset() {
            below = false;
            front = back = false;
        }
    }
}
