using NaughtyAttributes;
using UnityEngine;

public enum DestroyType
{
    BulgeOut,
    ShakeUnstable,
    Instant
}


[System.Serializable]
public class ExplosionBehaviorInspector
{
    [SerializeField] DestroyType destroyType;
    [ShowIf("IsBulgeOut")][SerializeField] BulgeOutExplosion bulgeOutExplosion;
    [ShowIf("IsShakeUnstable")][SerializeField] ShakeUnstableExplosion shakeUnstableExplosion;
    [ShowIf("IsInstant")][SerializeField] InstantExplosion instantExplosion;
    bool IsBulgeOut => destroyType == DestroyType.BulgeOut;
    bool IsShakeUnstable => destroyType == DestroyType.ShakeUnstable;
    bool IsInstant => destroyType == DestroyType.Instant;
}

//public class ExplosionParameters
//{
//    public DestroyType DestroyType { get; }
//    public GameObject EnemyExplosion { get; }
//    public float SelfDestructDelay { get; }
//    public Vector3 BaseLocalScale { get; }
//    public float BulgeOutScale { get; }
//    public float ShakeDuration { get; }
//    public float MaxShakeMagnitude { get; }
//    public AnimationCurve MagnitudeOverTime { get; }

//    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion, float selfDestructDelay, Vector3 baseLocalScale, float bulgeOutScale)
//    {
//        DestroyType = destroyType;
//        EnemyExplosion = enemyExplosion;
//        //Bulge out parameters
//        SelfDestructDelay = selfDestructDelay;
//        BaseLocalScale = baseLocalScale;
//        BulgeOutScale = bulgeOutScale;
//        //Default values for shake unstable parameters
//        ShakeDuration = 1f;
//        MaxShakeMagnitude = 0f;
//        MagnitudeOverTime = AnimationCurve.Constant(0f, 1f, 0f);
//    }

//    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion, float shakeDuration, float maxShakeMagnitude, AnimationCurve magnitudeOverTime)
//    {
//        DestroyType = destroyType;
//        EnemyExplosion = enemyExplosion;
//        //Default values for bulge out parameters
//        SelfDestructDelay = 0f;
//        BaseLocalScale = Vector3.one;
//        BulgeOutScale = 1f;
//        //Shake unstable parameters
//        ShakeDuration = shakeDuration;
//        MaxShakeMagnitude = maxShakeMagnitude;
//        MagnitudeOverTime = magnitudeOverTime;
//    }

//    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion)
//    {
//        DestroyType = destroyType;
//        EnemyExplosion = enemyExplosion;
//        //Default values for bulge out parameters
//        SelfDestructDelay = 0f;
//        BaseLocalScale = Vector3.one;
//        BulgeOutScale = 1f;
//        //Default values for shake unstable parameters
//        ShakeDuration = 1f;
//        MaxShakeMagnitude = 0f;
//        MagnitudeOverTime = AnimationCurve.Constant(0f, 1f, 0f);
//    }

//    public ExplosionParameters GetParameterForExplosion()
//    {
//        switch (DestroyType)
//        {
//            case DestroyType.BulgeOut:
//                return new ExplosionParameters(DestroyType, EnemyExplosion, SelfDestructDelay, BaseLocalScale, BulgeOutScale);
//            case DestroyType.ShakeUnstable:
//                return new ExplosionParameters(DestroyType, EnemyExplosion, ShakeDuration, MaxShakeMagnitude, MagnitudeOverTime);
//            case DestroyType.Instant:
//                return new ExplosionParameters(DestroyType, EnemyExplosion);
//            default:
//                return new ExplosionParameters(DestroyType.Instant, EnemyExplosion);
//        }
//    }
//}
