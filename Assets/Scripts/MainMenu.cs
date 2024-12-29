using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button quitButton;
    [SerializeField] Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(() => LoadNextLevel());
        quitButton.onClick.AddListener(() => Application.Quit());
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 2)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
