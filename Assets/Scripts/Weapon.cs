using UnityEngine;
using StarterAssets;

public class Weapon : MonoBehaviour
{
    StarterAssetsInputs inputs;

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
    }
    
    void Update()
    {
        if (inputs.shoot)
        {
            RaycastHit hit;

            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
            if (hit.collider != null) Debug.Log(hit.collider.name);
            inputs.ShootInput(false);
        }

        
    }
}
