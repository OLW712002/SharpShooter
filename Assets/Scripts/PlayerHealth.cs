using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 5;
    [SerializeField] CinemachineVirtualCamera playerDeathCamera;
    [SerializeField] Transform weaponCamera;

    int gameoverVirtualCameraPriority = 20;

    void Start()
    {

    }

    public void TakeDamage(int dmg)
    {
        playerHealth -= dmg;
        if (playerHealth <= 0)
        {
            weaponCamera.parent = null;
            playerDeathCamera.Priority = gameoverVirtualCameraPriority;
            Debug.Log("PLayer Die");
            Destroy(this.gameObject);
        }
    }
}
