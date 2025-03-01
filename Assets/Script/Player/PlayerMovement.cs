using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float xValueClamp;
    [SerializeField] float zValueClamp;
    Vector2 movement;
    Rigidbody playerRb;

    void   Awake() 
   
    {
        playerRb = GetComponent<Rigidbody>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement);
    }

    void FixedUpdate()
    {
        ProccessMove();
    }

    void ProccessMove(){
        Vector3 currentPosition = playerRb.position;
        Vector3 direction = new Vector3(movement.x, 0f, movement.y);
        Vector3 movePosition = currentPosition + direction * Time.deltaTime * moveSpeed;


        movePosition.x = Mathf.Clamp(movePosition.x , -xValueClamp, zValueClamp);
        movePosition.z = Mathf.Clamp(movePosition.z , -zValueClamp, zValueClamp);
        
        playerRb.MovePosition(movePosition);
    }
}
