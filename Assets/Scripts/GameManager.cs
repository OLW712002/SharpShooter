using StarterAssets;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] NavMeshSurface mapNavMeshSurface;
    [SerializeField] GameObject youWinText;
    [SerializeField] TextMeshProUGUI enemiesLeftText;

    const string ENEMIES_LEFT_TEXT = "Enemies Left: ";

    int enemiesLeft = 0;

    void Awake()
    {
        StarterAssetsInputs playerInput = FindFirstObjectByType<StarterAssetsInputs>();
        playerInput.SetCursorState(true);
        mapNavMeshSurface.enabled = true;
    }

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_TEXT + enemiesLeft.ToString("D2");

        if (enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
        }
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
