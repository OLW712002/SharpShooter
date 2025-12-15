using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponSO gunType;
    protected const string playerShootString = "Shoot";
}
