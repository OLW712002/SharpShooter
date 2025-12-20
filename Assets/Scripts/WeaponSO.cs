using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public int gunDmg = 1;
    public float fireCooldown = 1f;
    public bool isAutomatic = false;
    public bool canZoom = false;
    public ParticleSystem gunFlash;
    public GameObject hitVFX;
    
}
