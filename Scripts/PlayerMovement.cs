using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 5f; // Speed of the player movement

    public float groundDrag = 4f; // Drag applied when the player is on the ground

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player
        rb.freezeRotation = true; // Freeze the rotation of the Rigidbody to prevent it from tipping over

    }

    void Update()
    {
        MyInput(); // Call the MyInput method to get user input

        rb.linearDamping = groundDrag; // Set the drag of the Rigidbody to the ground drag value


    }

    private void FixedUpdate()
    {
        MovePlayer(); // Call the MovePlayer method to move the player
    }

    private void MyInput()
    {
        // Get the horizontal and vertical input from the user
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; // Bakt���n y�ne y�r�meni sa�lar.
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); // Add force to the Rigidbody in the direction of movement

    }
}