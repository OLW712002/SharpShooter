using UnityEngine;
using System.Collections;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem gunFlash;
    [SerializeField] int gunDamage = 1;
    [SerializeField] float fireCooldown = 1f;

    StarterAssetsInputs starterAssetsInpouts;

    bool isOverHeat = false;
    bool wasShooting = false;

    void Awake()
    {
        starterAssetsInpouts = GetComponentInParent<StarterAssetsInputs>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Debug.Log(starterAssetsInpouts.shoot);

        if (starterAssetsInpouts.shoot && !isOverHeat && !wasShooting)
        {
            ShootProcess();
        }
        wasShooting = starterAssetsInpouts.shoot;
        starterAssetsInpouts.ShootInput(false);
    }

    void ShootProcess()
    {
        isOverHeat = true;
        StartCoroutine(OverHeatCoroutine(fireCooldown));

        gunFlash.Play();

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Robot>()?.TakeDamage(gunDamage);
            Debug.Log(hit.collider.name);
        }
        
    }

    IEnumerator OverHeatCoroutine(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
        isOverHeat = false;
    }
}
