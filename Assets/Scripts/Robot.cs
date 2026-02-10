using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject robotExplosion;
    [SerializeField] float robotChasingRadius = 10f;
    [SerializeField] float robotSelfDestructDelay = 2f;
    [SerializeField] float robotBulgeOutScale = 2f;

    FirstPersonController player;
    Animator robotAnimator;
    NavMeshAgent agent;
    EnemyHealth robotHealth;

    const string playerString = "Player";
    const string robotChasingString = "isChasing";

    bool isBulgeOut = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        robotAnimator = GetComponentInChildren<Animator>();
        robotHealth = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
        agent.enabled = true;
    }

    void Update()
    {
        if (!player) return;
        HandleChasing();
    }

    void HandleChasing()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < robotChasingRadius && !isBulgeOut)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            Debug.Log("Kaboom");
            isBulgeOut = true;
            StopChasing();
            StartCoroutine(robotHealth.SelfDestruct(GetParameterForExplosion(1)));
        }
    }

    public (GameObject enemyExplosion, float enemySelfDestructDelay, Vector3 enemyLocalScale, float enemyBulgeOutScale) GetParameterForExplosion(int i)
    {
        if (i == 0) return (robotExplosion, 0, transform.localScale, robotBulgeOutScale);
        return (robotExplosion, robotSelfDestructDelay, transform.localScale, robotBulgeOutScale);
    }
}
