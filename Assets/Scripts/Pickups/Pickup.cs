using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    ActiveWeapon activeWeapon;
    [SerializeField] float rotationSpeed = 100f;
    const string PLAYER_STRING = "Player";
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
            activeWeapon = other.gameObject.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(this.gameObject);
        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
