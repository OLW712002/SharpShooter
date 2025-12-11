using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : Weapon
{
    [SerializeField] WeaponSO gunType;
    [SerializeField] PlayerInput playerInput;

    float elapsedTime = 0f;

    StarterAssetsInputs starterAssetsInpouts;
    
    void Awake()
    {
        starterAssetsInpouts = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.P)) Debug.Log(starterAssetsInpouts.shoot);
        if (starterAssetsInpouts.shoot && elapsedTime > gunType.fireCooldown)
        {
            ShootProcess(gunType);
            elapsedTime = 0f;
        }
        if (!gunType.isAutomatic || playerInput.actions[playerShootString].WasReleasedThisFrame())
        {
            starterAssetsInpouts.ShootInput(false);
        }
    }

    
}
