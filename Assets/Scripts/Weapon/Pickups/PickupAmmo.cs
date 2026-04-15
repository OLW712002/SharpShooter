using UnityEngine;

public class PickupAmmo : Pickups
{
    protected override void OnPickup(WeaponSO weaponSO)
    {
        Transform currentWeapon = GameObject.Find(currentWeaponString).transform.GetChild(0);
        string currentWeaponName = currentWeapon.GetComponent<ActiveWeapon>().GetWeaponSO().weaponPrefab.name;
        if (currentWeaponName == weaponSO.weaponPrefab.name)
        {
            currentWeapon.GetComponent<ActiveWeapon>().HandlePickup(this, storedAmmo);
            Destroy(gameObject);
        }
    }
}
