using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    protected const string playerLayerString = "Player";

    protected void ReducePlayerHealth(Collider[] hitCollider, int dmg)
    {
        foreach (Collider collider in hitCollider)
        {
            PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
            if (!playerHealth) continue;
            playerHealth.TakeDamage(dmg);
            break;
        }
    }
}
