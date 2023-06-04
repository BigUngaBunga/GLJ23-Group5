using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelDuration = 60;
    [SerializeField] int scoreFactor = 5;
    private float startTime;
    private float removedTime = 0;
    private GameTimer timer;
    private bool loadingLevel = false;
    private float ElapsedTime => Time.time - startTime + removedTime;
    public static int levelScore;
    public void RemoveTime(int time) => removedTime -= time;
    
    private void Start()
    {
        SoundManager.instance.StartLevel(SceneManager.GetActiveScene().buildIndex);
        levelScore = 0;
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
        SoundManager.instance.PlayJingle();
        levelScore += (int)(levelDuration - ElapsedTime) * scoreFactor;
        HighScoreList.score += levelScore;
        loadingLevel = true;
        LoadNextLevel();
    }

    private void LoadNextLevel() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    private float TimeLeft()
    {
        return levelDuration - ElapsedTime;
    }

}
