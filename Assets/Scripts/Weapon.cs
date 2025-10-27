using UnityEngine;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    [SerializeField] int gunDamage = 1;

    StarterAssetsInputs starterAssetsInpouts;

    void Awake()
    {
        starterAssetsInpouts = GetComponentInParent<StarterAssetsInputs>();
    }
    
    void Update()
    {
        if (starterAssetsInpouts.shoot)
        {
            RaycastHit hit;

            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Robot>()?.TakeDamage(gunDamage);
                Debug.Log(hit.collider.name);
            }
            starterAssetsInpouts.ShootInput(false);
        }

        
    }
}
