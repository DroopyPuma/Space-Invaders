using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [Header("Gun and Bullet References")]
    [SerializeField] private GameObject bullet; //reference to the bullet prefab 
    [SerializeField] private GameObject bulletSpawnPos1; //bullet spawn location on one gun
    [SerializeField] private GameObject bulletSpawnPos2; //bullet spawn location on other gun

    private Vector2 moveInput; 
    private InputActionMap _inputs;
    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor in the middle of the screen when game starts
        Cursor.visible = false; //makes cursor invisible so the crosshair is used, use esc to see cursor again while in game testing 
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

 
    private void FixedUpdate()
    {
        // takes the rigidbdy componenet and uses the move vector 2 from the OnMove times the move speed value and applies it to the rigidbody in the form of vector 3 coordinates
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

    
    //References the move in the input action map and gets the vector 2 value from the button pressed
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); 
    }

    //When the attack button from input action is pressed spawn the bullet game object at predetermined locations 
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"bullet: {bullet}, pos1: {bulletSpawnPos1}, pos2: {bulletSpawnPos2}");

            if (bullet == null) { Debug.LogError("bullet is null!"); return; }
            if (bulletSpawnPos1 == null) { Debug.LogError("bulletSpawnPos1 is null!"); return; }
            if (bulletSpawnPos2 == null) { Debug.LogError("bulletSpawnPos2 is null!"); return; }

            Instantiate(bullet, bulletSpawnPos1.transform.position, Camera.main.transform.rotation);
            Instantiate(bullet, bulletSpawnPos2.transform.position, Camera.main.transform.rotation);
        }
    }
}
