using UnityEngine;

public abstract class Pickups : Weapon
{
    [SerializeField] protected float rotateSpeed = 2f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTagString))
        {
            OnPickup(weaponSO);
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup(WeaponSO weaponSO);
}
