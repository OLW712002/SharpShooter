using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class ActiveWeapon : Weapon
{
    [SerializeField] Transform gunFlashParent;
    [SerializeField] Animator playerAnimator;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] MeshRenderer gunMeshRenderer;

    StarterAssetsInputs starterAssetsInputs;
    Transform vfxParent;
    PlayerInput playerInput;
    CinemachineVirtualCamera playerFollowCamera;
    Image zoomVignette;
    FirstPersonController playerController;
    TextMeshProUGUI ammoText;
    CinemachineImpulseSource cinemachineImpulseSource;

    const string vfxParentString = "VFX Parent";
    const string ammoTextString = "Ammo Text";

    float elapsedTime = 0f;
    float defaultVerticalFOV;
    float defaultRotationSpeed;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        vfxParent = GameObject.Find(vfxParentString).transform;
        playerInput = FindFirstObjectByType<PlayerInput>();
        playerFollowCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        zoomVignette = GameObject.Find(zoomVigenetteString).GetComponent<Image>();
        playerController = GetComponentInParent<FirstPersonController>();
        ammoText = GameObject.Find(ammoTextString).GetComponent<TextMeshProUGUI>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        if (needAmmoParameter) storedAmmo = ammoParameter;

        defaultVerticalFOV = playerFollowCamera.m_Lens.FieldOfView;
        zoomVignette.enabled = false;
        defaultRotationSpeed = playerController.RotationSpeed;
        ammoText.text = storedAmmo.ToString("D2");
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
        if (!starterAssetsInputs.shoot || elapsedTime < weaponSO.fireCooldown || storedAmmo <= 0) return;

        ReduceAmmo(1);

        cinemachineImpulseSource.GenerateImpulse();

        ParticleSystem gunFlashParticle = Instantiate(weapon.gunFlash, gunFlashParent.position, gunFlashParent.rotation, gunFlashParent);
        Destroy(gunFlashParticle.gameObject, 2f);

        playerAnimator.Play(playerShootString, 0, 0);

        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore);
        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<EnemyHealth>()?.TakeDamage(weapon.gunDmg);
        }
        if (hit.point != null && hit.point != Vector3.zero)
        {
            Destroy(Instantiate(weapon.hitVFX, hit.point, Quaternion.identity, vfxParent), 5f);
        }

        ZoomOut();
        starterAssetsInputs.ZoomInput(false);

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

        if (starterAssetsInputs.needChangeZoomState)
        {
            if (starterAssetsInputs.zoom) ZoomIn();
            else ZoomOut();
            starterAssetsInputs.needChangeZoomState = false;
        }
    }

    void ZoomIn()
    {
        playerFollowCamera.m_Lens.FieldOfView = weaponSO.zoomFOV;
        zoomVignette.enabled = true;
        playerController.RotationSpeed = weaponSO.zoomRotationSpeed;
        gunMeshRenderer.enabled = false;
    }

    void ZoomOut()
    {
        playerFollowCamera.m_Lens.FieldOfView = defaultVerticalFOV;
        zoomVignette.enabled = false;
        playerController.RotationSpeed = defaultRotationSpeed;
        gunMeshRenderer.enabled = true;
    }

    void ReduceAmmo(int i)
    {
        storedAmmo -= i;
        storedAmmo = Mathf.Clamp(storedAmmo, 0, weaponSO.maxAmmo);
        ammoText.text = storedAmmo.ToString("D2");
    }

    public void HandlePickup(Pickups pickups, int amount)
    {
        if (pickups.GetPickupType() == Pickups.PickupType.Ammo) ReduceAmmo(-amount);
    }

    //public void SwitchWeapon(WeaponSO weaponSO)
    //{        
    //    GameObject newWeapon = Instantiate(weaponSO.weaponPrefab);
    //    Destroy(this);
    //}
}
