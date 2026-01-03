using UnityEngine;

public class PickupWeapon : Pickups
{
    GameObject oldWeapon;

    int oldWeaponAmmoRemain;


    const string currentWeaponString = "Current Weapon";

    void Awake()
    {
        
    }

    protected override void OnPickup(WeaponSO weaponSO)
    {
        oldWeapon = FindFirstObjectByType<ActiveWeapon>().gameObject;
        oldWeaponAmmoRemain = oldWeapon.GetComponent<ActiveWeapon>().GetCurrentAmmoStored();
        Destroy(oldWeapon);
        Instantiate(weaponSO.weaponPrefab, GameObject.Find(currentWeaponString).transform);
        CreateNewPickup(oldWeaponAmmoRemain, true);


    }

    void CreateNewPickup(int ammoInNewPickup, bool createWithParameter)
    {
        GameObject newPickup = Instantiate(weaponSO.pickupPrefab);
        Vector3 forwardCamera = Camera.main.transform.forward;
        forwardCamera.y = 0;
        newPickup.transform.position = gameObject.transform.position + forwardCamera.normalized * 5;
        if (createWithParameter) newPickup.GetComponent<ActiveWeapon>().ReduceAmmo(weaponSO.maxAmmo - ammoInNewPickup);
    }
}
