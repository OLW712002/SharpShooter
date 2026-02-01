using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

    Robot enemyClass;

    void Start()
    {
        enemyClass = GetComponent<Robot>();
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
            Debug.Log("Die");
            var enemyExplosionParameters = enemyClass.GetParameterForExplosion(0);
            StartCoroutine(SelfDestruct(enemyExplosionParameters));
        }
    }

    public IEnumerator SelfDestruct((GameObject enemyExposion, float enemySelfDestructDelay, Vector3 enemyBaseLocalScale, float enemyBulgeOutScale) data)
    {
        //Bulge out before explosion
        if (data.enemySelfDestructDelay > 0)
        {
            Vector3 startValue = data.enemyBaseLocalScale;
            Vector3 targetValue = data.enemyBaseLocalScale * data.enemyBulgeOutScale;
            float elapsedTime = 0f;
            while (elapsedTime < data.enemySelfDestructDelay)
            {
                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTime / data.enemySelfDestructDelay);
                yield return null;
            }
            transform.localScale = targetValue;
        }

        //Base explosion
        Instantiate(data.enemyExposion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
