using UnityEngine;
using System.Collections;

public abstract class ExplosionBehaviorSO : ScriptableObject
{
    public abstract IEnumerator Explode(ExplosionParameters explosionParameters, Transform transform);
}
