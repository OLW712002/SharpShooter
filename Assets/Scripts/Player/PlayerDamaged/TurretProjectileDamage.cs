using UnityEngine;

public class TurretProjectileDamage : PlayerDamaged
{
    [SerializeField] int turretDmg = 1;
    [SerializeField] float projectileSpeed = 10f;

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int playerLayerMask = LayerMask.GetMask(playerLayerString);
            Collider[] hitCollider = Physics.OverlapSphere(transform.position, 0.1f, playerLayerMask, QueryTriggerInteraction.Ignore);
            HitPlayer(hitCollider, turretDmg);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
