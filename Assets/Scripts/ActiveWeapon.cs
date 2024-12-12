using UnityEngine;
using StarterAssets;
using UnityEditor;
using Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] GameObject zoomVignette;
    Animator animator;
    float defaultFOV;
    float defaultRotationSpeed;
    const string SHOOT_STRING = "Shoot";
    float timeSinceLastShot = 0;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        defaultFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultRotationSpeed = firstPersonController.RotationSpeed;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        HandleShoot();
        HandleZoom();
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

    public void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;
        if (starterAssetsInputs.zoom)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
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
