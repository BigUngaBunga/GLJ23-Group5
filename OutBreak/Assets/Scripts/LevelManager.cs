using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelDuration = 60;
    private float startTime;
    private float removedTime = 0;
    private GameTimer timer;
    private bool loadingLevel = false;
    public void RemoveTime(int time) => removedTime -= time;

    [SerializeField] GameObject HighScoreBoard;
    
    private void Start()
    {
        timer = FindObjectOfType<GameTimer>();
        startTime = Time.time;
        
        
    }

    private void ResumeTime()
    {
        Debug.Log("Deactivate");
        HighScoreBoard.SetActive(false);
    }

    private void FixedUpdate()
    {
        float timeLeft = TimeLeft();
        timer.SetTime(timeLeft);
        if (timeLeft <= 0 && !loadingLevel)
        {
            Time.timeScale = 0.01f;

            HighScoreBoard.SetActive(true);

            Transform obj = HighScoreBoard.transform.GetChild(0);

            TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();

            text.text = "Current Score: "+2; // insert high score

            Invoke("EndLevel", 0.03f);
        }
        
    }

    private void EndLevel()
    {
        //TODO show score for the level

        Time.timeScale = 1;

        loadingLevel = true;
        LoadNextLevel();
    }

    private void LoadNextLevel() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    private float TimeLeft()
    {
        float elapsedTime = Time.time - startTime + removedTime;
        return levelDuration - elapsedTime;
    }

}
