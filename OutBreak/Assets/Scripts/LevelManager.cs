using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelDuration = 60;
    private float startTime;
    private float removedTime = 0;
    private Timer timer;
    public void RemoveTime(float time) => removedTime -= time;
    
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        float timeLeft = TimeLeft();
        timer.SetTime(timeLeft);
        if (timeLeft <= 0)
            EndLevel();
        
    }

    private void EndLevel()
    {

    }

    private float TimeLeft()
    {
        float elapsedTime = Time.time - startTime + removedTime;
        return levelDuration - elapsedTime;
    }
 
}
