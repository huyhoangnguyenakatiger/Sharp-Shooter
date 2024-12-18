using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform headTurret;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject projectileTurretPrefab;
    [SerializeField] float fireRate = 3f;
    [SerializeField] int damage = 2;
    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireCoroutine());
    }
    void Update()
    {
        if (!player) return;
        headTurret.LookAt(playerTargetPoint.position);
    }

    IEnumerator FireCoroutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);
            Projectile projectileTurret = Instantiate(projectileTurretPrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            projectileTurret.transform.LookAt(playerTargetPoint);
            projectileTurret.Init(damage);
        }

    }
}
