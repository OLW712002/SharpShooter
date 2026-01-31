using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
            Debug.Log("Die");
            //StartCoroutine(SelfDestruct(0));
        }
    }

    //IEnumerator SelfDestruct(float robotSelfDestructDelay)
    //{
    //    if (robotSelfDestructDelay > 0)
    //    {
    //        float value = transform.localScale.x;
    //        float elapsedTime = 0f;
    //        while (elapsedTime < robotSelfDestructDelay)
    //        {
    //            elapsedTime += Time.deltaTime;
    //            transform.localScale = Vector3.one * Mathf.Lerp(value, robotBulgeOutScale, elapsedTime / robotSelfDestructDelay);
    //            yield return null;
    //        }
    //        transform.localScale = Vector3.one * robotBulgeOutScale;
    //    }
    //    Instantiate(robotExplosion, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}
