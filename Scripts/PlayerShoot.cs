using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //bullet variables
    public float bulletSpeed;
    public float fireRate, bulletDamage;
    public bool isAuto;

    //Initial Setup
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    private float timer;

    //audio vars
    [SerializeField]
    private AudioSource audioSourceToPlay;
    [SerializeField]
    private AudioClip[] audioClipsToPlay;
        
    // Start is called before the first frame update
    void Start()
    {
        audioSourceToPlay = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool for if its auto and allows you to set the firerate
        if(timer > 0)
        { 
            timer -= Time.deltaTime / fireRate; 
        }
        if (isAuto)
        {
            if (Input.GetKey(KeyCode.Space) && timer <= 0)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) &&  timer <= 0)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        //spawns the bullet and give it speed
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("BulletHolder").transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.up * bulletSpeed, ForceMode.Impulse);
        bullet.transform.rotation = transform.rotation;
        timer = 1;
        // setting up the audio 
        AudioClip audioClip = audioClipsToPlay[Random.Range(0, audioClipsToPlay.Length)];
        audioSourceToPlay.clip = audioClip;
        audioSourceToPlay.Play();
    }

}
