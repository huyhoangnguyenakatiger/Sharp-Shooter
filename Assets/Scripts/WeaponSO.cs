using UnityEngine;
[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptstable Object/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public int Damage = 1;
    public float FireRate = 0.5f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;
    public GameObject Prefab;
}
