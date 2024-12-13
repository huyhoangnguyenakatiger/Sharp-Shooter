using UnityEngine;
using StarterAssets;
using UnityEditor;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] TMP_Text ammoText;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] WeaponSO startingWeaponSO;
    Weapon currentWeapon;

    WeaponSO currentWeaponSO;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;


    float defaultFOV;
    float defaultRotationSpeed;
    const string SHOOT_STRING = "Shoot";
    float timeSinceLastShot = 0;
    int currentAmmo;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
        // currentWeapon = GetComponentInChildren<Weapon>();

    }
    void Start()
    {
        SwitchWeapon(startingWeaponSO);
        AdjustAmmo(startingWeaponSO.MagazineSize);
    }
    void Update()
    {

        HandleShoot();
        HandleZoom();
    }

    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;
        if (!starterAssetsInputs.shoot) return;
        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(currentWeaponSO);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic) starterAssetsInputs.ShootInput(false);

    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.Prefab, this.transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(weaponSO.MagazineSize);
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString("D2");
    }

    public void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;
        if (starterAssetsInputs.zoom)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
            zoomVignette.SetActive(true);
        }
        else
        {
            // starterAssetsInputs.ZoomInput(false);
            cinemachineVirtualCamera.m_Lens.FieldOfView = defaultFOV;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            zoomVignette.SetActive(false);
        }
    }
}
