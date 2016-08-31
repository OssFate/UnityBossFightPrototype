using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CollisionInfo collisions;
    private BoxCollider _collider;
    private float distToGroud;

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

        transform.Translate(velocity);
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
