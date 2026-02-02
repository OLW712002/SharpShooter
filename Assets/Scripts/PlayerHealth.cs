using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 5;

    void Start()
    {

    }

    public void TakeDamage(int dmg)
    {
        playerHealth -= dmg;
        if (playerHealth <= 0)
        {
            Debug.Log("PLayer Die");
        }
    }
}
