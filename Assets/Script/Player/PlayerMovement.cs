using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float minMoveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTimer = 0;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float minJumpForce = 4f;
    [SerializeField] float maxJumpForce = 1.5f;
    [SerializeField] float jumpForceChange = 0.2f;



    [SerializeField] float xValueClamp;
    [SerializeField] float zValueClamp;
    Vector2 movement;
    bool jump;
    bool isPause = false;
    Rigidbody playerRb;
    Vector3 currentPos;


    void Awake()

    {
        playerRb = GetComponent<Rigidbody>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!jump)
            {
                jump = true;
                jumpTimer = 0;
                currentPos = transform.position;
            }
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPause = !isPause;
            if (isPause)
            {
                GameManagers.ManagerSingleton.ActivePause();
            }
            else
            {
                GameManagers.ManagerSingleton.Countinue();
            }
        }
    }

    void FixedUpdate()
    {
        ProccessMove();
        ProcessJump();
    }

    public void ChangeSpeed(float amount)
    {
        moveSpeed += amount;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    public void ChangeJumpForce(float amount)
    {
        if (amount > 0)
        {
            jumpForce += jumpForceChange;
        }
        else
        {
            jumpForce -= jumpForceChange;
        }

        jumpForce = Math.Clamp(jumpForce, minJumpForce, maxJumpForce);
    }

    void ProcessJump()
    {
        if (jump)
        {
            jumpTimer += Time.deltaTime;
            float duration = jumpTimer / jumpDuration;
            if (duration >= 1f)
            {
                jump = false;
                transform.position = currentPos;
            }
            else
            {
                float jumpOffset = Mathf.Sin(duration * MathF.PI) * jumpForce;
                transform.position = new Vector3(currentPos.x, currentPos.y + jumpOffset, currentPos.z);
            }
        }
    }

    void ProccessMove()
    {
        Vector3 currentPosition = playerRb.position;
        Vector3 direction = new Vector3(movement.x, 0f, movement.y);
        Vector3 movePosition = currentPosition + direction * Time.deltaTime * moveSpeed;

        movePosition.x = Mathf.Clamp(movePosition.x, -xValueClamp, zValueClamp);
        movePosition.z = Mathf.Clamp(movePosition.z, -zValueClamp, zValueClamp);

        playerRb.MovePosition(movePosition);
    }
}
