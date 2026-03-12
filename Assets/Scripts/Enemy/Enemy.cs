using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected const string playerString = "Player";

    void ExplodeAndSelfDestroy(GameObject explosionPrefab, Vector3 explosionOffset)
    {
            Instantiate(explosionPrefab, transform.position + explosionOffset, Quaternion.identity);
            Destroy(this.gameObject);
    }

    public IEnumerator ExplodeSequence(ExplosionBehavior explosionBehavior)
    {
        yield return StartCoroutine(explosionBehavior.BehaviorBeforeExploding(transform));
        ExplodeAndSelfDestroy(explosionBehavior.GetEnemyExplosion(), explosionBehavior.GetExplosionOffset());
    }

    public ExplosionBehavior GetExplosionBehavior(DestroyType destroyType, BulgeOutExplosion bulgeOutExplosion, ShakeUnstableExplosion shakeUnstableExplosion, InstantExplosion instantExplosion)
    {
        switch (destroyType)
        {
            case DestroyType.BulgeOut:
                return bulgeOutExplosion;
            case DestroyType.ShakeUnstable:
                return shakeUnstableExplosion;
            case DestroyType.Instant:
                return instantExplosion;
            default:
                return null;
        }
    }
}
