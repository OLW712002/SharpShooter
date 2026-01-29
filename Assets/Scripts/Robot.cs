using UnityEngine;
using UnityEngine.AI;
using StarterAssets;
using System.Collections;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject robotExplosion;
    [SerializeField] int robotHealth = 3;
    [SerializeField] float robotChasingRadius = 10f;
    [SerializeField] float robotSelfDestructDelay = 2f;
    [SerializeField] float robotBulgeOutScale = 2f;

    FirstPersonController player;
    Animator robotAnimator;
    NavMeshAgent agent;

    const string playerString = "Player";
    const string robotChasingString = "isChasing";

    bool isBulgeOut = false;

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

    public void TakeDamage(int dmg)
    {
        robotHealth -= dmg;
        if (robotHealth <= 0)
        {
            Debug.Log("Die");
            StartCoroutine(SelfDestruct(0));
        }
    }

    IEnumerator SelfDestruct(float robotSelfDestructDelay)
    {
        if (robotSelfDestructDelay > 0)
        {
            float value = transform.localScale.x;
            float elapsedTime = 0f;
            while (elapsedTime < robotSelfDestructDelay)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.one * Mathf.Lerp(value, robotBulgeOutScale, elapsedTime / robotSelfDestructDelay);
                yield return null;
            }
            transform.localScale = Vector3.one * robotBulgeOutScale;
        }
        Instantiate(robotExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            Debug.Log("Kaboom");
            isBulgeOut = true;
            StopChasing();
            StartCoroutine(SelfDestruct(robotSelfDestructDelay));
        }
    }
}
