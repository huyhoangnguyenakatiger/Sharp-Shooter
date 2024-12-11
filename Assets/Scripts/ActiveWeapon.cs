using UnityEngine;
using StarterAssets;
using UnityEditor;

public class ActiveWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    [SerializeField] WeaponSO weaponSO;
    Animator animator;
    const string SHOOT_STRING = "Shoot";
    float timeSinceLastShot = 0;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        currentWeapon = GetComponentInChildren<Weapon>();
    }
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;
        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(weaponSO);
            timeSinceLastShot = 0f;

        }

        if (!weaponSO.IsAutomatic) starterAssetsInputs.ShootInput(false);

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
            Weapon newWeapon = Instantiate(weaponSO.Prefab, this.transform).GetComponent<Weapon>();
            currentWeapon = newWeapon;
            this.weaponSO = weaponSO;
        }
    }
}
