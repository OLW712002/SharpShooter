using UnityEngine;

public class PickupWeapon : Pickups
{
    ActiveWeapon oldActiveWeapon;
    WeaponSO oldWeaponSO;
    Transform weaponPickupParent;

    int oldWeaponAmmoRemain;

    const string weaponPickupParentString = "Weapon Pickup";

    void Awake()
    {
        weaponPickupParent = GameObject.Find(weaponPickupParentString).transform;
    }

    void Start()
    {
        if (needAmmoParameter) storedAmmo = ammoParameter;
    }

    protected override void OnPickup(WeaponSO weaponSO)
    {
        //Destroy current weapon
        oldActiveWeapon = FindFirstObjectByType<ActiveWeapon>();
        oldWeaponSO = oldActiveWeapon.GetWeaponSO();
        oldWeaponAmmoRemain = oldActiveWeapon.GetCurrentAmmoStored();
        Destroy(oldActiveWeapon.gameObject);

        //Create new weapon
        CreateNewWeapon(weaponSO, storedAmmo, true);

        //Create new pickup weapon
        CreateNewPickup(oldWeaponSO, oldWeaponAmmoRemain, true);
    }

    void CreateNewWeapon(WeaponSO weaponSO, int ammoInNewWeapon, bool createWithParameter)
    {
        Transform weaponParent = GameObject.Find(currentWeaponString).transform;
        GameObject newWeapon = Instantiate(weaponSO.weaponPrefab, weaponParent);
        if (createWithParameter) newWeapon.GetComponent<ActiveWeapon>().SetAmmoParameter(ammoInNewWeapon);
    }

    void CreateNewPickup(WeaponSO weaponSO, int ammoInNewPickup, bool createWithParameter)
    {
        GameObject newPickup = Instantiate(weaponSO.pickupPrefab, weaponPickupParent);
        Vector3 forwardCamera = Camera.main.transform.forward;
        forwardCamera.y = 0;
        newPickup.transform.position = gameObject.transform.position + forwardCamera.normalized * 5;
        if (createWithParameter) newPickup.GetComponent<PickupWeapon>().SetAmmoParameter(ammoInNewPickup);
    }
}
