using UnityEngine;

public class TurretProjectileDamage : PlayerDamaged
{
    [SerializeField] int turretDmg = 1;
    [SerializeField] float projectileSpeed = 10f;

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
        hasHitPlayer = true;
        Debug.Log(transform.position);
        if (other.CompareTag("Player"))
        {
            //Use ClosestPoint and OverlapSphere for detection
            transform.position = other.ClosestPoint(transform.position);
            Debug.Log("Turret projectile hit player: " + other.name);
            int playerLayerMask = LayerMask.GetMask(playerLayerString);
            Collider[] hitCollider = Physics.OverlapSphere(transform.position, 0.1f, playerLayerMask, QueryTriggerInteraction.Ignore);
            HitPlayer(hitCollider, turretDmg);

            //Normal detection
            //other.GetComponentInParent<PlayerHealth>()?.TakeDamage(turretDmg);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Pickup"))
        {
            Debug.Log("Turret projectile hit pickup: " + other.name);
        }
        else
        {
            Debug.Log("Turret projectile hit: " + other.name);
            Destroy(gameObject);
        }
    }
}
