using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    protected const string playerLayerString = "Player";

    protected void HitPlayer(Collider[] hitCollider, int dmg)
    {
        Debug.Log("Hit");
        foreach (Collider collider in hitCollider)
        {
            PlayerHealth playerHealth = collider.GetComponentInParent<PlayerHealth>();
            if (!playerHealth) continue;
            playerHealth.TakeDamage(dmg);
            Debug.Log("Player hit for " + dmg + " damage.");
            break;
        }
    }

    protected void HitOther(Collider hitCollider)
    {

    }
}
