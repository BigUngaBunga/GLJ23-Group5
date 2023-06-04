using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelDuration = 60;
    private float startTime;
    private float removedTime = 0;
    private GameTimer timer;
    private bool loadingLevel = false;
    public void RemoveTime(int time) => removedTime -= time;
    
    private void Start()
    {
        timer = FindObjectOfType<GameTimer>();
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        float timeLeft = TimeLeft();
        timer.SetTime(timeLeft);
        if (timeLeft <= 0 && !loadingLevel)
            EndLevel();
        
    }

    private void EndLevel()
    {
        //TODO show score for the level
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
