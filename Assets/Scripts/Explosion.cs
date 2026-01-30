using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 2f;

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

    }
}
