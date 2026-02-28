using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public enum DestroyType
{
    BulgeOut,
    ShakeUnstable,
    Instant
}

public class ExplosionParameters
{
    public DestroyType DestroyType { get; }
    public GameObject EnemyExplosion { get; }
    public float SelfDestructDelay { get; }
    public Vector3 BaseLocalScale { get; }
    public float BulgeOutScale { get; }

    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion, float selfDestructDelay, Vector3 baseLocalScale, float bulgeOutScale)
    {
        DestroyType = destroyType;
        EnemyExplosion = enemyExplosion;
        //Bulge out parameters
        SelfDestructDelay = selfDestructDelay;
        BaseLocalScale = baseLocalScale;
        BulgeOutScale = bulgeOutScale;
        //Shake unstable parameters can be added here in the future
    }

    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion)
    {
        DestroyType = destroyType;
        EnemyExplosion = enemyExplosion;
        //Default values for bulge out parameters
        SelfDestructDelay = 0f;
        BaseLocalScale = Vector3.one;
        BulgeOutScale = 1f;
    }
}

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

    [Header("Explosion Upon Death")]
    [SerializeField] DestroyType destroyType;
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] Vector3 enemyExplosionOffset = Vector3.zero;

    [Header("BulgeOut")]
    [ShowIf("IsBulgeOut")][SerializeField] float enemySelfDestructDelay = 0f;
    [ShowIf("IsBulgeOut")][SerializeField] float enemyBulgeOutScale = 0f;
    bool IsBulgeOut => destroyType == DestroyType.BulgeOut;

    Enemy enemyClass;

    void Start()
    {
        enemyClass = GetComponent<Enemy>();
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
            if (enemyClass == null)
            {
                Destroy(this.gameObject);
                return;
            }

            //Use enemy.cs parameters
            //var enemyExplosionParameters = enemyClass.GetParameterForExplosion(0);
            //StartCoroutine(SelfDestruct(enemyExplosionParameters));

            //Use this class's parameters
            ExplosionParameters instantExplosion = new ExplosionParameters(DestroyType.Instant, enemyExplosion);
            StartCoroutine(SelfDestruct(instantExplosion));
        }
    }

    public IEnumerator SelfDestruct(ExplosionParameters explosionParameters)
    {
        switch(explosionParameters.DestroyType)
        {
            case DestroyType.BulgeOut:
                Vector3 startValue = explosionParameters.BaseLocalScale;
                Vector3 targetValue = explosionParameters.BaseLocalScale * explosionParameters.BulgeOutScale;
                float elapsedTime = 0f;
                while (elapsedTime < explosionParameters.SelfDestructDelay)
                {
                    elapsedTime += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTime / explosionParameters.SelfDestructDelay);
                    yield return null;
                }
                transform.localScale = targetValue;
                break;
            case DestroyType.ShakeUnstable:
                //Implement shake unstable behavior here in the future
                break;
            case DestroyType.Instant:
                //Do nothing, just explode immediately
                break;
        }

        //Bulge out before explosion
        //if (data.enemySelfDestructDelay > 0)
        //{
        //    Vector3 startValue = data.enemyBaseLocalScale;
        //    Vector3 targetValue = data.enemyBaseLocalScale * data.enemyBulgeOutScale;
        //    float elapsedTime = 0f;
        //    while (elapsedTime < data.enemySelfDestructDelay)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        transform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTime / data.enemySelfDestructDelay);
        //        yield return null;
        //    }
        //    transform.localScale = targetValue;
        //}

        //Base explosion
        Instantiate(explosionParameters.EnemyExplosion, transform.position + enemyExplosionOffset, Quaternion.identity);
        Destroy(gameObject);
    }

    public ExplosionParameters GetParameterForExplosion()
    {
        return new ExplosionParameters(destroyType, enemyExplosion, enemySelfDestructDelay, transform.localScale, enemyBulgeOutScale); ;
    }
}
