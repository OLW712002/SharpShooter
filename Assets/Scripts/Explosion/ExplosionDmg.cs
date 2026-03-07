using UnityEngine;

public class ExplosionDmg : MonoBehaviour
{
    [SerializeField] float radius = 2f;
    [SerializeField] int explodeDmg = 3;

    const string playerLayerString = "Player";

    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        int playerLayerMask = LayerMask.GetMask(playerLayerString);
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius, playerLayerMask, QueryTriggerInteraction.Ignore);
        //Debug.Log(hitCollider);
        foreach(Collider collider in hitCollider)
        {
            PlayerHealth playerHealth = collider.GetComponentInParent<PlayerHealth>();
            if (!playerHealth) continue;
            playerHealth.TakeDamage(explodeDmg);
            break;

            //collider.GetComponentInParent<PlayerHealth>()?.TakeDamage(explodeDmg);
        }
    }
}
