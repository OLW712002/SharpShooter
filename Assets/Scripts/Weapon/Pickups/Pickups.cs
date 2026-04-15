using UnityEngine;

public abstract class Pickups : Weapon
{
    public enum PickupType { Ammo, Weapon }

    [SerializeField] protected float rotateSpeed = 2f;
    [SerializeField] protected PickupType pickupType;

    protected const string currentWeaponString = "Current Weapon";

    public PickupType GetPickupType() { return pickupType; }

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTagString))
        {
            OnPickup(weaponSO);
        }
    }

    protected abstract void OnPickup(WeaponSO weaponSO);
}
