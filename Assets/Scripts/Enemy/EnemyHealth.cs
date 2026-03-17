using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;
    [SerializeField] ExplosionBehaviorInspector explosionUponDeath;

    Enemy enemyClass;
    ExplosionBehavior enemyExplosionBehavior;

    void Start()
    {
        enemyClass = GetComponent<Enemy>();
        enemyExplosionBehavior = explosionUponDeath.GetSelectedBehavior();
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
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
