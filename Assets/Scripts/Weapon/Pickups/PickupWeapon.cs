using UnityEngine;

public class PickupWeapon : Pickups
{
    const string currentWeaponString = "Current Weapon";

    protected override void OnPickup(WeaponSO weaponSO)
    {
        Destroy(FindFirstObjectByType<ActiveWeapon>().gameObject);
        Instantiate(weaponSO.weaponPrefab, GameObject.Find(currentWeaponString).transform);
    }
}
