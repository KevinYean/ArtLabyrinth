using UnityEngine;
using System.Collections;


// The GameObject is made to bounce using the space key.
// Also the GameOject can be moved forward/backward and left/right.
// Add a Quad to the scene so this GameObject can collider with a floor.

public class PlayerControl : MonoBehaviour {

    public Rigidbody rb;


    public float speedH = 2.0f;
    private float yaw = 0f;

    private float moveSpeed = 3.5f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        //Set Cursor to not be visible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // pressing esc toggles between hide/show
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate() {
        //Rotation of Object
        yaw += speedH * Input.GetAxis("Mouse X");
        rb.rotation = Quaternion.Euler(new Vector3(0.0f, yaw, 0.0f));
        //Debug.DrawRay(transform.position, transform.forward, Color.green);

        //Movement
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = moveDirection.normalized; //Normalize
        moveDirection = moveDirection * moveSpeed * Time.deltaTime;
        
        rb.MovePosition(transform.position + (transform.forward * moveDirection.z) + 
                (transform.right * moveDirection.x));

        if (Input.GetButton("Jump") && IsGrounded()) {
            rb.AddForce(transform.up * 60f);
        }
        //Debug.DrawRay(transform.position, -transform.up * 1.5f, Color.red);
    }

    /// <summary>
    /// Function returns if object is touching the ground
    /// </summary>
    /// <returns></returns>
    bool IsGrounded(){
        //Debug.Log(Physics.Raycast(transform.position, -transform.up, 1.5f));
        return Physics.Raycast(transform.position, -transform.up, 1.5f);
    }
}