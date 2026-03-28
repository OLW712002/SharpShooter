using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    protected const string playerLayerString = "Player";

    protected void HitPlayer(Collider[] hitCollider, int dmg)
    {
        foreach (Collider collider in hitCollider)
        {
            PlayerHealth playerHealth = collider.GetComponentInParent<PlayerHealth>();
            if (!playerHealth) continue;
            playerHealth.TakeDamage(dmg);
            break;
        }
    }

    protected void HitOther(Collider hitCollider)
    {

    }
}
