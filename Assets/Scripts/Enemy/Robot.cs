using NaughtyAttributes;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Robot : Enemy
{
    [SerializeField] float robotChasingRadius = 10f;

    [Header("Explosion When Approaching Player")]
    [SerializeField] BulgeOutExplosion bulgeOutExplosion;

    FirstPersonController playerController;
    Animator robotAnimator;
    NavMeshAgent agent;

    const string robotChasingString = "isChasing";

    bool isBulgeOut = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        robotAnimator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        playerController = FindFirstObjectByType<FirstPersonController>();
        agent.enabled = true;
    }

    void Update()
    {
        if (!playerController)
        {
            StopChasing();
            return;
        }
        HandleChasing();
    }

    void HandleChasing()
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) < robotChasingRadius && !isBulgeOut)
        {
            //Chase the player
            agent.SetDestination(playerController.transform.position);
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
            StartCoroutine(ExplodeSequence());
        }
    }

    IEnumerator ExplodeSequence()
    {
        yield return StartCoroutine(bulgeOutExplosion.ExplodeBehavior(transform));
        ExplodeAndSelfDestroy(bulgeOutExplosion.GetEnemyExplosion(), bulgeOutExplosion.GetExplosionOffset());
    }
}
