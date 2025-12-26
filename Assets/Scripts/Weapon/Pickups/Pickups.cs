using UnityEngine;

public abstract class Pickups : Weapon
{
    [SerializeField] protected float rotateSpeed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    protected abstract void OnPickup();
}
