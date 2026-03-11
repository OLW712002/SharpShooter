using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected const string playerString = "Player";

    void ExplodeAndSelfDestroy(GameObject explosionPrefab, Vector3 explosionOffset)
    {
            //Explosion
            Instantiate(explosionPrefab, transform.position + explosionOffset, Quaternion.identity);
            Destroy(this.gameObject);
    }

    public IEnumerator ExplodeSequence(ExplosionBehavior explosionBehavior)
    {
        yield return StartCoroutine(explosionBehavior.BehaviorBeforeExploding(transform));
        ExplodeAndSelfDestroy(explosionBehavior.GetEnemyExplosion(), explosionBehavior.GetExplosionOffset());
    }
}
