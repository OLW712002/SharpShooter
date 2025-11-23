using UnityEngine;
using StarterAssets;

public class TeleportBox : MonoBehaviour
{
    [SerializeField] Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonController playerController = other.GetComponent<FirstPersonController>();
            BasicRigidBodyPush basicRigidBodyPush = other.GetComponent<BasicRigidBodyPush>();
            StarterAssetsInputs starterAssetsInputs = other.GetComponent<StarterAssetsInputs>();

            playerController.enabled = false;
            basicRigidBodyPush.enabled = false;
            starterAssetsInputs.enabled = false;

            Debug.Log("box " + other.gameObject.name);
            //other.transform.position = target.position;
            other.GetComponent<Player>().Teleport(target.position);

            playerController.enabled = true;
            basicRigidBodyPush.enabled = true;
            basicRigidBodyPush.enabled = true;
        }
    }
}
