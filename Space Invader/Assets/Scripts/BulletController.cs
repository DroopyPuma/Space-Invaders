using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    public float damage;
    public float lifeTime = 3;
    private Vector3 moveDirection; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    { 
        //puts a timer on the bullet and destroies them once the time is up
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
       moveDirection = Camera.main.transform.forward;
        rb.linearVelocity =  moveDirection * bulletSpeed; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroies game object on collisionn  with asteroid
        if (collision.gameObject.tag == "Enemy")
        {
            // play enemy death sounds here 
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    
}
