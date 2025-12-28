using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class ActiveWeapon : Weapon
{
    [SerializeField] Transform gunFlashParent;
    [SerializeField] Animator playerAnimator;
    [SerializeField] LayerMask interactionLayer;

    const string vfxParentString = "VFX Parent";
    float elapsedTime = 0f;
    float defaultVerticalFOV;
    float defaultRotationSpeed;

    StarterAssetsInputs starterAssetsInputs;
    Transform vfxParent;
    PlayerInput playerInput;
    CinemachineVirtualCamera playerFollowCamera;
    Image zoomVignette;
    FirstPersonController playerController;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        vfxParent = GameObject.Find(vfxParentString).transform;
        playerInput = FindFirstObjectByType<PlayerInput>();
        playerFollowCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        zoomVignette = GameObject.Find(zoomVigenetteString).GetComponent<Image>();
        playerController = GetComponentInParent<FirstPersonController>();

        defaultVerticalFOV = playerFollowCamera.m_Lens.FieldOfView;
        zoomVignette.enabled = false;
        defaultRotationSpeed = playerController.RotationSpeed;
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
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore);
        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<Robot>()?.TakeDamage(weapon.gunDmg);
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
    }

    void ZoomOut()
    {
        playerFollowCamera.m_Lens.FieldOfView = defaultVerticalFOV;
        zoomVignette.enabled = false;
        playerController.RotationSpeed = defaultRotationSpeed;
    }

    //public void SwitchWeapon(WeaponSO weaponSO)
    //{
        
    //    GameObject newWeapon = Instantiate(weaponSO.weaponPrefab);
    //    Destroy(this);
    //}
}
