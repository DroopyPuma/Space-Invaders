using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    public float lifeTime;

    private void Update()
    { 
        //puts a timer on the bullet and destroies them once the time is up
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroies game object on collisionn  with asteroid
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(this.gameObject);
        }
    }

}
