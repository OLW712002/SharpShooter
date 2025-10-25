using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    void Update()
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null) Debug.Log(hit.collider.name);
    }
}
