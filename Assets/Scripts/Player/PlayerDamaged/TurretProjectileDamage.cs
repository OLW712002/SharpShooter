using UnityEngine;

public class TurretProjectileDamage : PlayerDamaged
{
    int projectileDmg = 1;
    float projectileSpeed = 10f;
    GameObject projectileHitVFX;
    Transform projectileContainer;

    Rigidbody rb;

    public void Init(int dmg, float speed, GameObject hitVFX, Transform container)
    {
        projectileDmg = dmg;
        projectileSpeed = speed;
        projectileHitVFX = hitVFX;
        projectileContainer = container;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * projectileSpeed;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            //Use ClosestPoint and OverlapSphere for detection
            //transform.position = other.ClosestPoint(transform.position);
            //Debug.Log("Turret projectile hit player: " + other.name);
            //int playerLayerMask = LayerMask.GetMask(playerString);
            //Collider[] hitCollider = Physics.OverlapSphere(transform.position, 0.1f, playerLayerMask, QueryTriggerInteraction.Ignore);
            //HitPlayer(hitCollider, turretDmg);

            //Normal detection
            other.GetComponentInParent<PlayerHealth>()?.TakeDamage(projectileDmg);

            ReleaseHitVFXAndSelfDestroy();
        }
        else if (other.CompareTag(pickupString))
        {
            //Do nothing
        }
        else
        {
            ReleaseHitVFXAndSelfDestroy();
        }
    }

    void ReleaseHitVFXAndSelfDestroy()
    {
        GameObject hitVFX = Instantiate(projectileHitVFX, transform.position, Quaternion.identity, projectileContainer);
        Destroy(hitVFX, 5f);
        Destroy(gameObject);
    }
}
