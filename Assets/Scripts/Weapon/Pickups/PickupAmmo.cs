using UnityEngine;

public class PickupAmmo : Pickups
{
    string weaponName;
    string[] wordsInThisObjectName;

    protected override void OnPickup(WeaponSO weaponSO)
    {
        weaponName = GameObject.Find(currentWeaponString).transform.GetChild(0).name;
        wordsInThisObjectName = gameObject.name.Split(' ');
        if (weaponName == wordsInThisObjectName[0])
        {
            Debug.Log("Get Ammo");
        }
    }
}
