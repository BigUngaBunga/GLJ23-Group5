using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
   public void SetPlayerNumber(int playerNumber)
    {
        PlayerPrefs.SetInt("Players", playerNumber);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
