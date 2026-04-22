using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    public float damage;
    public float lifeTime;

    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}