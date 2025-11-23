using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 telePos;

    public void Teleport(Vector3 targetPos)
    {
        Debug.Log(gameObject.transform.position);
        gameObject.transform.position = targetPos;
        Debug.Log("new" + gameObject.transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Teleport(telePos);
        }
    }
}
