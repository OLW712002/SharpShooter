using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected const string playerString = "Player";

    public void ExplodeAndSelfDestroy(GameObject explosionPrefab, Vector3 explosionOffset)
    {
            //Explosion
            Instantiate(explosionPrefab, transform.position + explosionOffset, Quaternion.identity);
            Destroy(this.gameObject);
    }

    //IEnumerator ExplodeSequence()
    //{
    //    yield return StartCoroutine(bulgeOutExplosion.ExplodeBehavior(transform));
    //    ExplodeAndSelfDestroy(bulgeOutExplosion.GetEnemyExplosion(), bulgeOutExplosion.GetExplosionOffset());
    //}
}
