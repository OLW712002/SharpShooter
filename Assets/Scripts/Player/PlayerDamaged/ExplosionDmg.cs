using UnityEngine;

public class ExplosionDmg : PlayerDamaged
{
    [SerializeField] float radius = 2f;
    [SerializeField] int explodeDmg = 3;

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
        int playerLayerMask = LayerMask.GetMask(playerString);
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius, playerLayerMask, QueryTriggerInteraction.Ignore);
        HitPlayer(hitCollider, explodeDmg);
    }
}
