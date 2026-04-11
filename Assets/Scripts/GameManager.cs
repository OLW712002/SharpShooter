using StarterAssets;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] NavMeshSurface mapNavMeshSurface;

    void Awake()
    {
        StarterAssetsInputs playerInput = FindFirstObjectByType<StarterAssetsInputs>();
        playerInput.SetCursorState(true);
        mapNavMeshSurface.enabled = true;
    }

    public void QuitButton()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

    public void RestartButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
