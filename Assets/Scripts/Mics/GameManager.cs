using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text youWinText;
    [SerializeField] TMP_Text enemiesLeftText;
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";
    int enemiesLeft = 0;
    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
        if (enemiesLeft <= 0)
        {

            youWinText.gameObject.SetActive(true);
        }
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
