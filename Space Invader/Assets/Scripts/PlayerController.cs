using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    //[SerializeField] private float FireRate = 5f;
    //private float nextFireTime = 0;

    private int numberOfLives;
    [SerializeField] private Slider livesSlider; // UI slider for lives

    [Header("Gun and Bullet References")]
    [SerializeField] private GameObject bullet; //reference to the bullet prefab 
    [SerializeField] private GameObject bulletSpawnPos1; //bullet spawn location on one gun
    [SerializeField] private GameObject bulletSpawnPos2; //bullet spawn location on other gun

    private Vector2 moveInput; 
    private InputActionMap _inputs;
    private Rigidbody rb;
    private PlayerData _playerData;

    [Header("Sound References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip moveSound;

    //AJ NEEDED FIXES
    public float bulletSpeed;
    public float fireRate, bulletDamage;
    public bool isAuto;

    private float timer;

    private bool attackInput = false;

    InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void Start()
    {
        _playerData = new PlayerData();
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor in the middle of the screen when game starts
        Cursor.visible = false; //makes cursor invisible so the crosshair is used, use esc to see cursor again while in game testing 
        rb = GetComponent<Rigidbody>();
        numberOfLives = _playerData.playerLives;

        livesSlider.maxValue = numberOfLives;
        livesSlider.value = numberOfLives;


        Debug.Log($"Life Count: {numberOfLives}");
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Attack.performed += ctx => attackInput = true;
        inputActions.Player.Attack.canceled += ctx => attackInput = false;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= ctx => attackInput = true;
        inputActions.Player.Attack.canceled -= ctx => attackInput = false;

        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (isAuto)
        {
            if (attackInput && timer <= 0)
            {
                OnAttack();
            }
        }
        else
        {
            if (attackInput && timer <= 0)
            {
                OnAttack();
                attackInput = false;
            }
        }

        Death();
    }

 
    private void FixedUpdate()
    {
        // takes the rigidbdy componenet and uses the move vector 2 from the OnMove times the move speed value and applies it to the rigidbody in the form of vector 3 coordinates
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        
    }


    private void OnTriggerEnter(Collider collision)
    {
        //deactivates player on collision with asteroid and tells the gameManger that it died
        if (collision.gameObject.tag == "Enemy")
        {
            numberOfLives -= 1;

            Debug.Log($"Hit by enemy! Lives remaining: {numberOfLives}");

            // update slider
            livesSlider.value = numberOfLives;
        }
    }

    
    //References the move in the input action map and gets the vector 2 value from the button pressed
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        

    }

    //When the attack button from input action is pressed spawn the bullet game object at predetermined locations 
    public void OnAttack()
    {
        timer = 1f;

        Debug.Log($"bullet: {bullet}, pos1: {bulletSpawnPos1}, pos2: {bulletSpawnPos2}");

            if (bullet == null) { Debug.LogError("bullet is null!"); return; }
            if (bulletSpawnPos1 == null) { Debug.LogError("bulletSpawnPos1 is null!"); return; }
            if (bulletSpawnPos2 == null) { Debug.LogError("bulletSpawnPos2 is null!"); return; }

            GameObject b1 = Instantiate(bullet, bulletSpawnPos1.transform.position, Camera.main.transform.rotation);
            GameObject b2 = Instantiate(bullet, bulletSpawnPos2.transform.position, Camera.main.transform.rotation);

            b1.GetComponent<BulletController>().SetDirection(Camera.main.transform.forward);
            b2.GetComponent<BulletController>().SetDirection(Camera.main.transform.forward);

            // play shooting sound here 
            audioSource.PlayOneShot(shootSound); 

        
    }

    private void Death()
    {
        if (numberOfLives == 0)
        {
            // play death sound here 
            Destroy(this.gameObject);
            Debug.Log("Player has died");
        }
    }

    
}
