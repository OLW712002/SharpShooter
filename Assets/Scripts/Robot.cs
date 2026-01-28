using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject robotExplosion;
    [SerializeField] int robotHealth = 3;
    [SerializeField] float robotChasingRadius = 10f;

    FirstPersonController player;
    Animator robotAnimator;
    NavMeshAgent agent;

    const string playerString = "Player";
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
        HandleChasing();

    }

    void HandleChasing()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < robotChasingRadius)
        {
            //Chase the player
            agent.SetDestination(player.transform.position);
            robotAnimator.SetBool(robotChasingString, true);
        }
        else
        {
            StopChasing();
        }
    }

    void StopChasing()
    {
        agent.SetDestination(transform.position);
        robotAnimator.SetBool(robotChasingString, false);
    }

    public void TakeDamage(int dmg)
    {
        robotHealth -= dmg;
        if (robotHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(robotExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            Debug.Log("Kaboom");
        }
    }
}
