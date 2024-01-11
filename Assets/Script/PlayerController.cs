using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;

    public float moveSpeed = 5f;
    public float gravityScale = 15f;
    public float jumpForce = 20f;
    public Vector2 inputMovement;

    public bool isJumping;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = inputMovement.x * transform.right + inputMovement.y * transform.forward;

        velocity.y -= gravityScale;
        velocity.y += isJumping ? jumpForce : 0f;
        
        characterController.Move(velocity * (moveSpeed * Time.deltaTime));
        
        animator.SetFloat("Speed", velocity.magnitude);
    }

    void OnMove(InputValue input)
    {
        Debug.Log("OnMove Caller  : " + input.Get<Vector2>());
        inputMovement = input.Get<Vector2>();
    }

    void OnJump(InputValue input)
    {

        bool valueJump = input.Get<float>() > Mathf.Epsilon;
        
        if (input.isPressed)
        {
            Debug.Log("Jump is pressed : " + valueJump);
        }
        if(!input.isPressed)
        {
            Debug.Log("Jump is released : " + valueJump);
        }
    }

    // void OnMoveEvent(InputAction.CallbackContext context)
    // {
    //     var value = context.ReadValue<Vector2>();
    // }

}
