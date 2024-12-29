using System.Collections;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text youWinText;
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";
    int enemiesLeft = 0;
    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
        if (enemiesLeft <= 0)
        {

            youWinText.gameObject.SetActive(true);
            StartCoroutine(AdvanceNextLevel());
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

    IEnumerator AdvanceNextLevel()
    {
        yield return new WaitForSeconds(5f);
        LoadNextLevel();
    }


    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == 3)
        {
            nextScene = 0;
            starterAssetsInputs.SetCursorState(false);
        }
        SceneManager.LoadScene(nextScene);



    }
}
