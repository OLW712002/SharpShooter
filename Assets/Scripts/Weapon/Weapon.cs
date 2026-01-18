using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponSO weaponSO;

    protected const string playerTagString = "Player";
    protected const string playerShootString = "Shoot";
    protected const string zoomVigenetteString = "Zoom Vignette";

    public int storedAmmo = 10;

    protected bool needAmmoParameter = false;
    protected int ammoParameter;

    public WeaponSO GetWeaponSO()
    {
        return weaponSO;
    }

    public int GetCurrentAmmoStored()
    {
        return storedAmmo;
    }

    public void SetAmmoParameter(int i)
    {
        needAmmoParameter = true;
        ammoParameter = i;
    }
}
