using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject pickupPrefab;
    public int gunDmg = 1;
    public float fireCooldown = 1f;
    public bool isAutomatic = false;
    public bool canZoom = false;
    public float zoomFOV = 10f;
    public float zoomRotationSpeed = 0.3f;
    public int maxAmmo = 10;
    public ParticleSystem gunFlash;
    public GameObject hitVFX;
    
}
