using UnityEngine;

public class PickupAmmo : Pickups
{
    protected override void OnPickup(WeaponSO weaponSO)
    {
        string weaponName = GameObject.Find(currentWeaponString).transform.GetChild(0).name;
        if (weaponName == weaponSO.weaponPrefab.name)
        {
            Debug.Log("Get Ammo");
        }
    }
}
