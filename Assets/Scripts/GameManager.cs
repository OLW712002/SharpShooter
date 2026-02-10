using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] NavMeshSurface mapNavMeshSurface;

    void Awake()
    {
        mapNavMeshSurface.enabled = true;
    }
}
