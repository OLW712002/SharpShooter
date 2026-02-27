using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject enemyExplosion;

    protected const string playerString = "Player";

    //public virtual (GameObject enemyExplosion, float enemySelfDestructDelay, Vector3 enemyLocalScale, float enemyBulgeOutScale) GetParameterForExplosion(int i)
    //{
    //    Debug.Log("Only explsion parameter");
    //    return (enemyExplosion, 0f, Vector3.one, 1f);
    //}
}
