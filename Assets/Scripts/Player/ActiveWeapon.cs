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
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] WeaponSO startingWeaponSO;
    Weapon currentWeapon;

    WeaponSO currentWeaponSO;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    AudioSource audioSource;


    float defaultFOV;
    float defaultRotationSpeed;
    float defaultWeaponFOV;
    const string SHOOT_STRING = "Shoot";
    float timeSinceLastShot = 0;
    int currentAmmo;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
        defaultWeaponFOV = weaponCamera.fieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
        audioSource = GetComponent<AudioSource>();

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
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
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
        if (weaponSO.ShootingAudio != null)
        {
            audioSource.clip = weaponSO.ShootingAudio;
        }
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
            weaponCamera.fieldOfView = 10f;
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
            zoomVignette.SetActive(true);
        }
        else
        {
            // starterAssetsInputs.ZoomInput(false);
            cinemachineVirtualCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultWeaponFOV;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            zoomVignette.SetActive(false);
        }
    }
}
