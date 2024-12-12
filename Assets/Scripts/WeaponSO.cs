using UnityEngine;
[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptstable Object/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject Prefab;
    public int Damage = 1;
    public float FireRate = 0.5f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
    public float ZoomRotationSpeed = .2f;
}
