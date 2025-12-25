using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponSO weaponSO;
    protected const string playerTagString = "Player";
    protected const string playerShootString = "Shoot";
    protected const string zoomVigenetteString = "Zoom Vignette";
}
