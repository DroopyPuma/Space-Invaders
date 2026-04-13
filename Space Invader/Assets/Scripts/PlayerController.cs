using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput; 
    private InputActionMap _inputs;
    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

 
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //deactivates player on collision with asteroid and tells the gameManger that it died
        if (collision.gameObject.tag == "Asteroid")
        { 
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            this.gameObject.SetActive(false);

     
        }
    }

    

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); 
    }
}
