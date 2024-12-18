using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int startingHealth = 3;
    [SerializeField] GameObject robotExplosionVFX;
    GameManager gameManager;
    int currentHealth;
    void Awake()
    {
        currentHealth = startingHealth;
    }
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        gameManager.AdjustEnemiesLeft(-1);
    }
}
