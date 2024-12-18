using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float speed = 30f;
    [SerializeField] GameObject projectileHitVFX;

    Rigidbody rb;

    int damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    public void Init(int damage)
    {
        this.damage = damage;
        Debug.Log("Projectile initialized with damage: " + damage);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        playerHealth?.TakeDamage(damage);
        // Debug.Log(playerHealth?.currentHealth);
        // Debug.Log(other.gameObject.name);
        Destroy(this.gameObject);
    }
}
