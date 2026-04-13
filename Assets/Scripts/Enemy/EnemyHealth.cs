using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;
    [SerializeField] ExplosionBehaviorInspector explosionUponDeath;

    Enemy enemyClass;
    ExplosionBehavior enemyExplosionBehavior;
    GameManager gameManager;

    void Start()
    {
        enemyClass = GetComponent<Enemy>();
        enemyExplosionBehavior = explosionUponDeath.GetSelectedBehavior();
        gameManager = FindFirstObjectByType<GameManager>();

        gameManager.AdjustEnemiesLeft(1);
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
            gameManager.AdjustEnemiesLeft(-1);
            if (enemyClass == null)
            {
                Debug.Log("No enemy class");
                Destroy(this.gameObject);
                return;
            }
            enemyClass.StartCoroutine(enemyClass.ExplodeSequence(enemyExplosionBehavior));
        }
    }
}
