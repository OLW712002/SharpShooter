using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    [SerializeField] int robotHealth = 3;

    FirstPersonController player;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    public void TakeDamage(int dmg)
    {
        robotHealth -= dmg;
        if (robotHealth <= 0) Destroy(gameObject);
    }
}
