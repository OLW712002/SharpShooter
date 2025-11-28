using UnityEngine;
using System.Collections;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem gunFlash;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform vfxParent;
    [SerializeField] Animator playerAnimator;
    [SerializeField] WeaponSO gunType;
    //[SerializeField] int gunDamage = 1;
    //[SerializeField] float fireCooldown = 1f;

    StarterAssetsInputs starterAssetsInpouts;

    bool isOverHeat = false;
    bool wasShooting = false;

    const string playerShootString = "Shoot";

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
        StartCoroutine(OverHeatCoroutine(gunType.fireCooldown));

        gunFlash.Play();
        playerAnimator.Play(playerShootString, 0, 0);

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<Robot>()?.TakeDamage(gunType.gunDmg);
            Debug.Log(hit.collider.name);
        }
        if (hit.point != null)
        {
            Destroy(Instantiate(hitVFX, hit.point, Quaternion.identity, vfxParent), 5f);
        }
    }

    IEnumerator OverHeatCoroutine(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
        isOverHeat = false;
    }


}
