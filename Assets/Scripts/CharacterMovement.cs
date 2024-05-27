using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float speed = 4;
    [SerializeField] float pushPower = 4.0f;
    [SerializeField, Range(1, 10)] float jumpHeight = 2;
    [SerializeField] float mouseSensitivity = 2.0f;
    [SerializeField] Transform view;
    [SerializeField] Animator animator;
    [SerializeField] Rig rig;

    private CharacterController controller;
    private Vector3 velocity;
    private bool onGround;
    private float verticalRotation = 0f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        onGround = controller.isGrounded;
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // get input for the horizontal and vertical axis movement and move the player
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        //move = Camera.main.transform.rotation * move;
        move = transform.TransformDirection(move);
        controller.Move(move * Time.deltaTime * speed);

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //move = Vector3.ClampMagnitude(move, 1);
        //move = transform.TransformDirection(move);
        //controller.Move(move * Time.deltaTime * speed);

        // Get input for mouse movement and rotate the player
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

        // rotate the character horizontally based on the mouse movement
        transform.Rotate(Vector3.up * mouseX);


        // rotate the camera vertically based on the mouse movement
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Equipped", !animator.GetBool("Equipped"));
            rig.weight = (animator.GetBool("Equipped") ? 1 : 0);
        }
        // this does the same thing as the above if statement regarding the rig weight
        //if (animator.GetBool("Equipped"))
        //{   // if the player is equipped, set the weight to 1
        //    rig.weight = 1;
        //}
        //else
        //{
        //    rig.weight = 0;
        //}   

        // animations
        animator.SetFloat("Speed", move.magnitude);
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", move.magnitude * speed);
        animator.SetBool("Jumping", !onGround);
        animator.SetFloat("VelocityY", velocity.y);
        animator.SetBool("OnGround", onGround); 
        animator.SetFloat("VerticalSpeed", velocity.y);
        animator.SetFloat("HorizontalSpeed", move.magnitude);
        animator.SetBool("Running", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("Crouching", Input.GetKey(KeyCode.LeftControl));
        animator.SetBool("Aiming", Input.GetMouseButton(1));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}