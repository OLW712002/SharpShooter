using StarterAssets;
using UnityEngine;

public class ActiveWeapon : Weapon
{
    [SerializeField] WeaponSO gunType;

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
        //if (!starterAssetsInpouts.shoot) starterAssetsInpouts.ShootInput(false);
    }

    
}
