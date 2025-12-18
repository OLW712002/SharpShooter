using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : Weapon
{
    [SerializeField] Transform gunFlashParent;
    [SerializeField] Animator playerAnimator;

    const string vfxParentString = "VFX Parent";
    float elapsedTime = 0f;

    StarterAssetsInputs starterAssetsInpouts;
    Transform vfxParent;
    PlayerInput playerInput;

    void Awake()
    {
        starterAssetsInpouts = GetComponentInParent<StarterAssetsInputs>();
        vfxParent = GameObject.Find(vfxParentString).transform;
        playerInput = FindFirstObjectByType<PlayerInput>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.P)) Debug.Log(starterAssetsInpouts.shoot);

        if (starterAssetsInpouts.shoot && elapsedTime > weaponSO.fireCooldown)
        {
            ShootProcess(weaponSO);
            elapsedTime = 0f;
        }

        if (!weaponSO.isAutomatic || playerInput.actions[playerShootString].WasReleasedThisFrame())
        {
            starterAssetsInpouts.ShootInput(false);
        }
    }

    void ShootProcess(WeaponSO weapon)
    {

        ParticleSystem gunFlashParticle = Instantiate(weapon.gunFlash, gunFlashParent.position, gunFlashParent.rotation, gunFlashParent);
        Destroy(gunFlashParticle.gameObject, 2f);

        playerAnimator.Play(playerShootString, 0, 0);

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<Robot>()?.TakeDamage(weapon.gunDmg);
            //Debug.Log(hit.collider.name);
        }
        if (hit.point != null)
        {
            Destroy(Instantiate(weapon.hitVFX, hit.point, Quaternion.identity, vfxParent), 5f);
        }
    }

    //public void SwitchWeapon(WeaponSO weaponSO)
    //{
        
    //    GameObject newWeapon = Instantiate(weaponSO.weaponPrefab);
    //    Destroy(this);
    //}
}
