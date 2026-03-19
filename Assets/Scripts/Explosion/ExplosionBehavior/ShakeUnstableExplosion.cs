using System.Collections;
using UnityEngine;

[System.Serializable]
public class ShakeUnstableExplosion : ExplosionBehavior
{
    //DestroyType destroyType = DestroyType.ShakeUnstable;

    [SerializeField] float enemyShakeDuration = 1f;
    [SerializeField] float enemyMaxShakeMagnitude = 10f;
    [SerializeField] AnimationCurve enemyMagnitudeOverTime = AnimationCurve.Constant(0f, 1f, 0f);

    public override IEnumerator BehaviorBeforeExploding(Transform ownerTransform)
    {
        //ExplosionParameters explosionParameters = new ExplosionParameters(destroyType, enemyExplosion, enemyShakeDuration, enemyMaxShakeMagnitude, enemyMagnitudeOverTime);

        //Vector3 originalLocalRotate = ownerTransform.localEulerAngles;
        //float elapsedTimeShake = 0f;
        //float internalFreq = 20f;
        //while (elapsedTimeShake < explosionParameters.ShakeDuration)
        //{
        //    elapsedTimeShake += Time.deltaTime;
        //    float normalizedTime = elapsedTimeShake / explosionParameters.ShakeDuration;
        //    float currentMagnitude = explosionParameters.MaxShakeMagnitude * explosionParameters.MagnitudeOverTime.Evaluate(normalizedTime);
        //    float offsetZ = Mathf.Sin(elapsedTimeShake * internalFreq * Mathf.PI * 2f) * currentMagnitude;
        //    ownerTransform.localEulerAngles = originalLocalRotate + new Vector3(0f, 0f, offsetZ);
        //    yield return null;
        //}
        //ownerTransform.localPosition = originalLocalRotate;

        Vector3 originalLocalRotate = ownerTransform.localEulerAngles;
        float elapsedTimeShake = 0f;
        float internalFreq = 20f;
        while (elapsedTimeShake < enemyShakeDuration)
        {
            elapsedTimeShake += Time.deltaTime;
            float normalizedTime = elapsedTimeShake / enemyShakeDuration;
            float currentMagnitude = enemyMaxShakeMagnitude * enemyMagnitudeOverTime.Evaluate(normalizedTime);
            float offsetZ = Mathf.Sin(elapsedTimeShake * internalFreq * Mathf.PI * 2f) * currentMagnitude;
            ownerTransform.localEulerAngles = originalLocalRotate + new Vector3(0f, 0f, offsetZ);
            yield return null;
        }
        ownerTransform.localEulerAngles = originalLocalRotate;
    }

    public override float GetSelfDestructDelay()
    {
        return enemyShakeDuration;
    }
}
