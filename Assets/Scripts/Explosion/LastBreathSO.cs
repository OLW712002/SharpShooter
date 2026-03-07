using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "LastBreathSO", menuName = "Scriptable Objects/LastBreathSO")]
public class LastBreathSO : ScriptableObject
{
    public IEnumerator Exploding(ExplosionParameters explosionParameters, Transform owner, Vector3 explosionOffset)
    {
        //Pre action before explosion based on destroy type
        switch (explosionParameters.DestroyType)
        {
            case DestroyType.BulgeOut:
                Vector3 startValue = explosionParameters.BaseLocalScale;
                Vector3 targetValue = explosionParameters.BaseLocalScale * explosionParameters.BulgeOutScale;
                float elapsedTimeBulge = 0f;
                while (elapsedTimeBulge < explosionParameters.SelfDestructDelay)
                {
                    elapsedTimeBulge += Time.deltaTime;
                    owner.localScale = Vector3.Lerp(startValue, targetValue, elapsedTimeBulge / explosionParameters.SelfDestructDelay);
                    yield return null;
                }
                owner.localScale = targetValue;
                break;
            case DestroyType.ShakeUnstable:
                Vector3 originalLocalRotate = owner.localEulerAngles;
                float elapsedTimeShake = 0f;
                float internalFreq = 20f;
                while (elapsedTimeShake < explosionParameters.ShakeDuration)
                {
                    elapsedTimeShake += Time.deltaTime;
                    float normalizedTime = elapsedTimeShake / explosionParameters.ShakeDuration;
                    float currentMagnitude = explosionParameters.MaxShakeMagnitude * explosionParameters.MagnitudeOverTime.Evaluate(normalizedTime);
                    float offsetZ = Mathf.Sin(elapsedTimeShake * internalFreq * Mathf.PI * 2f) * currentMagnitude;
                    owner.localEulerAngles = originalLocalRotate + new Vector3(0f, 0f, offsetZ);
                    yield return null;
                }
                owner.localPosition = originalLocalRotate;
                break;
            case DestroyType.Instant:
                //Do nothing, just explode immediately
                break;
        }

        //Explosion
        Instantiate(explosionParameters.EnemyExplosion, owner.position + explosionOffset, Quaternion.identity);
        Destroy(owner.gameObject);
    }
}
