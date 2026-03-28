using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1,10)] [SerializeField] int playerHealth = 5;
    [SerializeField] CinemachineVirtualCamera playerDeathCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;

    int gameoverVirtualCameraPriority = 20;

    void Start()
    {
        UpdateShieldBars();
    }

    public void TakeDamage(int dmg)
    {
        //playerHealth -= dmg;
        playerHealth = Mathf.Clamp(playerHealth -= dmg, 0, int.MaxValue);
        UpdateShieldBars();

        if (playerHealth <= 0)
        {
            weaponCamera.parent = null;
            playerDeathCamera.Priority = gameoverVirtualCameraPriority;
            Debug.Log("PLayer Die");
            Destroy(this.gameObject);
        }
    }

    void UpdateShieldBars()
    {
        for (int i = 0; i < playerHealth; i++)
        {
            shieldBars[i].color = Color.white;
        }
        for (int i = playerHealth; i < shieldBars.Length; i++)
        {
            shieldBars[i].color = Color.black;
        }
    }
}
