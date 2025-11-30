using StarterAssets;
using UnityEngine;
using System.Collections;

public class ActiveWeapon : Weapon
{
    [SerializeField] ParticleSystem gunFlash;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform vfxParent;
    [SerializeField] Transform gunFlashParent;
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
            ShootProcess(gunType);
        }
        wasShooting = starterAssetsInpouts.shoot;
        starterAssetsInpouts.ShootInput(false);
    }

    void ShootProcess(WeaponSO weapon)
    {
        isOverHeat = true;
        StartCoroutine(OverHeatCoroutine(weapon.fireCooldown));

        //gunFlash.Play();
        ParticleSystem gunFlasht = Instantiate(gunType.gunFlash, gunFlashParent.position, gunFlashParent.rotation, gunFlashParent);
        Destroy(gunFlasht.gameObject, 2f);

        playerAnimator.Play(playerShootString, 0, 0);

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<Robot>()?.TakeDamage(weapon.gunDmg);
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
