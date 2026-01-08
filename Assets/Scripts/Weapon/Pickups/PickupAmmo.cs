using UnityEngine;

public class PickupAmmo : Pickups
{
    protected override void OnPickup(WeaponSO weaponSO)
    {
        Transform currentWeapon = GameObject.Find(currentWeaponString).transform.GetChild(0);
        if (currentWeapon.name == weaponSO.weaponPrefab.name)
        {
            currentWeapon.GetComponent<ActiveWeapon>().ReduceAmmo(-storedAmmo);
        }
    }
}
