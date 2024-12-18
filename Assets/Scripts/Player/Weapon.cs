using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;
    CinemachineImpulseSource cinemachineImpulseSource;
    public void Start()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        cinemachineImpulseSource.GenerateImpulse();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
        }
    }
}
