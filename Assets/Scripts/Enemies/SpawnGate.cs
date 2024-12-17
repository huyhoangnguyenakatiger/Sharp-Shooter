using System;
using System.Collections;
using StarterAssets;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robot;
    [SerializeField] float timeToSpawnEnemy = 5f;
    [SerializeField] FirstPersonController player;
    void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        while (player)
        {
            Instantiate(robot, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeToSpawnEnemy);
        }
    }
}
