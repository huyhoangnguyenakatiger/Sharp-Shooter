using Cinemachine;
using UnityEngine.UI;
using UnityEngine;
using StarterAssets;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int startingHealth = 10;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    public int currentHealth;
    void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        AdjustShieldBar();
        if (currentHealth <= 0)
        {
            PlayerGameOver();

        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = 20;
        Destroy(this.gameObject);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
        gameOverContainer.SetActive(true);
    }

    public void AdjustShieldBar()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }


}
