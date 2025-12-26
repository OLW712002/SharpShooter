using UnityEngine;

public class PickupWeapon : Pickups
{
    const string currentWeaponString = "Current Weapon";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Transform currentWeapon = GameObject.Find(currentWeaponString).transform;
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTagString))
        {
            OnPickup();
        }
    }

    protected override void OnPickup()
    {
        //other.gameObject.GetComponentInChildren<ActiveWeapon>().SwitchWeapon(weaponSO);
        Destroy(FindFirstObjectByType<ActiveWeapon>().gameObject);
        Instantiate(weaponSO.weaponPrefab, GameObject.Find(currentWeaponString).transform);
        //this.enabled = false;
        Destroy(gameObject);
    }
}
