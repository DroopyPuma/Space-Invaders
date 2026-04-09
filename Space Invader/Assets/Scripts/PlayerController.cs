using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float thrustPower = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //allows the player to move in all directions
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrustPower);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(transform.forward * rotationSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(transform.forward * -rotationSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //deactivates player on collision with asteroid and tells the gameManger that it died
        if (collision.gameObject.tag == "Asteroid")
        { 
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            this.gameObject.SetActive(false);

            //the bad way 
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

    public void WrapTopBottom()
    { 
    
    }
    public void WrapLeftRight()
    { 
    
    }
}
