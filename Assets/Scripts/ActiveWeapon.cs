using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : Weapon
{
    [SerializeField] Transform gunFlashParent;
    [SerializeField] Animator playerAnimator;

    const string vfxParentString = "VFX Parent";
    float elapsedTime = 0f;

    StarterAssetsInputs starterAssetsInputs;
    Transform vfxParent;
    PlayerInput playerInput;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        vfxParent = GameObject.Find(vfxParentString).transform;
        playerInput = FindFirstObjectByType<PlayerInput>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Debug.Log(starterAssetsInputs.shoot);

        ShootProcess(weaponSO);
        StopShootingProcess();
        ZoomProcess(weaponSO);
    }


    void ShootProcess(WeaponSO weapon)
    {
        elapsedTime += Time.deltaTime;
        if (!starterAssetsInputs.shoot || elapsedTime < weaponSO.fireCooldown) return;

        ParticleSystem gunFlashParticle = Instantiate(weapon.gunFlash, gunFlashParent.position, gunFlashParent.rotation, gunFlashParent);
        Destroy(gunFlashParticle.gameObject, 2f);

        playerAnimator.Play(playerShootString, 0, 0);

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<Robot>()?.TakeDamage(weapon.gunDmg);
        }
        if (hit.point != null)
        {
            Destroy(Instantiate(weapon.hitVFX, hit.point, Quaternion.identity, vfxParent), 5f);
        }

        elapsedTime = 0f;
    }

    void StopShootingProcess()
    {
        if (!weaponSO.isAutomatic || playerInput.actions[playerShootString].WasReleasedThisFrame())
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void ZoomProcess(WeaponSO weaponSO)
    {
        if (!weaponSO.canZoom) return;

        if (starterAssetsInputs.zoom)
        {
            Debug.Log("Zooming");
            
        }
    }

    //public void SwitchWeapon(WeaponSO weaponSO)
    //{
        
    //    GameObject newWeapon = Instantiate(weaponSO.weaponPrefab);
    //    Destroy(this);
    //}
}
