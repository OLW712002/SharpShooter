using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected const string playerString = "Player";

    protected void ExplodeAndSelfDestroy(GameObject explosionPrefab, Vector3 explosionOffset)
    {
        Instantiate(explosionPrefab, transform.position + explosionOffset, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public virtual IEnumerator ExplodeSequence(ExplosionBehavior explosionBehavior)
    {
        yield return StartCoroutine(explosionBehavior.BehaviorBeforeExploding(transform));
        ExplodeAndSelfDestroy(explosionBehavior.GetEnemyExplosion(), explosionBehavior.GetExplosionOffset());
    }
}
