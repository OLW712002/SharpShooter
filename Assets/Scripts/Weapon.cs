using UnityEngine;
using System.Collections;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    [SerializeField] int gunDamage = 1;
    [SerializeField] float fireCooldown = 1f;

    StarterAssetsInputs starterAssetsInpouts;

    bool isOverHeat = false;

    void Awake()
    {
        starterAssetsInpouts = GetComponentInParent<StarterAssetsInputs>();
    }
    
    void Update()
    {
        if (starterAssetsInpouts.shoot && !isOverHeat)
        {
            ShootProcess();
        }
    }

    void ShootProcess()
    {
        isOverHeat = true;
        StartCoroutine(OverHeatCoroutine(fireCooldown));

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Robot>()?.TakeDamage(gunDamage);
            Debug.Log(hit.collider.name);
        }
        starterAssetsInpouts.ShootInput(false);
    }

    IEnumerator OverHeatCoroutine(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
        isOverHeat = false;
    }
}
