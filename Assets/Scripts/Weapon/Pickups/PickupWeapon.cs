using UnityEngine;

public class PickupWeapon : Pickups
{
    ActiveWeapon oldActiveWeapon;
    WeaponSO oldWeaponSO;

    int oldWeaponAmmoRemain;
    bool needAmmoParameter = false;
    int ammoParameter; 

    const string currentWeaponString = "Current Weapon";

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
        Instantiate(weaponSO.weaponPrefab, GameObject.Find(currentWeaponString).transform);

        //Create new pickup weapon
        CreateNewPickup(oldWeaponSO, oldWeaponAmmoRemain, true);
    }

    void CreateNewPickup(WeaponSO weaponSO, int ammoInNewPickup, bool createWithParameter)
    {
        GameObject newPickup = Instantiate(weaponSO.pickupPrefab);
        Vector3 forwardCamera = Camera.main.transform.forward;
        forwardCamera.y = 0;
        newPickup.transform.position = gameObject.transform.position + forwardCamera.normalized * 5;
        if (createWithParameter) newPickup.GetComponent<PickupWeapon>().SetAmmoParameter(ammoInNewPickup);
    }

    //public void InitStoredAmmo(int i)
    //{
    //    storedAmmo = i;
    //}

    public void SetAmmoParameter(int i)
    {
        needAmmoParameter = true;
        ammoParameter = i;
    }
}
