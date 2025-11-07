using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    [SerializeField] int robotHealth = 3;
    [SerializeField] float robotChasingRadius = 10f;

    FirstPersonController player;
    Animator robotAnimator;
    NavMeshAgent agent;

    const string robotChasingString = "isChasing";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        robotAnimator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < robotChasingRadius)
        {
            agent.SetDestination(player.transform.position);
            robotAnimator.SetBool(robotChasingString, true);
        }
        else
        {
            agent.SetDestination(transform.position);
            robotAnimator.SetBool(robotChasingString, false);
        }
            
    }

    public void TakeDamage(int dmg)
    {
        robotHealth -= dmg;
        if (robotHealth <= 0) Destroy(gameObject);
    }
}
