using UnityEngine;

public class TurretProjectileDamage : PlayerDamaged
{
    [SerializeField] int turretDmg = 1;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject hitVFX;

    Rigidbody rb;

    bool hasHitPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * projectileSpeed;
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        if (hasHitPlayer) return;
        //transform.Translate(Vector3.forward * Time.fixedDeltaTime * projectileSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);

        hasHitPlayer = true;
        Debug.Log(transform.position);
        if (other.CompareTag(playerString))
        {
            //Use ClosestPoint and OverlapSphere for detection
            //transform.position = other.ClosestPoint(transform.position);
            //Debug.Log("Turret projectile hit player: " + other.name);
            //int playerLayerMask = LayerMask.GetMask(playerString);
            //Collider[] hitCollider = Physics.OverlapSphere(transform.position, 0.1f, playerLayerMask, QueryTriggerInteraction.Ignore);
            //HitPlayer(hitCollider, turretDmg);

            //Normal detection
            other.GetComponentInParent<PlayerHealth>()?.TakeDamage(turretDmg);

            ReleaseHitVFXAndSelfDestroy();
        }
        else if (other.CompareTag(pickupString))
        {
            Debug.Log("Turret projectile hit pickup: " + other.name);
        }
        else
        {
            Debug.Log("Turret projectile hit: " + other.name);
            ReleaseHitVFXAndSelfDestroy();
        }
    }

    void ReleaseHitVFXAndSelfDestroy()
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
