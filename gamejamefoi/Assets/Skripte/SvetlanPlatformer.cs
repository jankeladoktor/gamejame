using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SvetlanPlatformer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    float horizontalMovement;

    public float jumpPower = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed) {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}
