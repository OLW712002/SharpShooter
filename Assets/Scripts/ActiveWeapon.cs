using StarterAssets;
using UnityEngine;

public class ActiveWeapon : Weapon
{
    [SerializeField] WeaponSO gunType;

    StarterAssetsInputs starterAssetsInpouts;
    
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

    
}
