using UnityEngine;

public class PickupWeapon : Pickups
{
    WeaponSO oldWeaponSO;
    GameObject oldWeapon;

    const string currentWeaponString = "Current Weapon";

    void Awake()
    {

    }

    protected override void OnPickup(WeaponSO weaponSO)
    {
        oldWeapon = FindFirstObjectByType<ActiveWeapon>().gameObject;
        oldWeaponSO = oldWeapon.GetComponent<ActiveWeapon>().GetCurrentWeaponSO();
        Destroy(oldWeapon);
        Instantiate(weaponSO.weaponPrefab, GameObject.Find(currentWeaponString).transform);
        CreateNewPickup(oldWeaponSO, true);
    }

    void CreateNewPickup(WeaponSO weaponSO, bool createWithParameter)
    {
        GameObject newPickup = Instantiate(weaponSO.pickupPrefab);
        if (createWithParameter) newPickup.GetComponent<PickupWeapon>().weaponSO = oldWeaponSO;
    }
}
