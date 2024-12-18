using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    const string PLAYER_STRING = "Player";

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
            Explose();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Explose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (!playerHealth) continue;
            playerHealth.TakeDamage(3);
            break;
        }
    }
}
