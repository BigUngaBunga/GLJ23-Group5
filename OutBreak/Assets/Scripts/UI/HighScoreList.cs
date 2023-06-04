using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class HighScoreList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreList;
    [SerializeField] private TextMeshProUGUI nameField;
    public static int score;

    private List<string> namedScores = new List<string>();

    void Start()
    {
        LoadScore();
    }

    private void LoadScore()
    {
        var scores = new string[] { "Baba yaga 0", "Ooga 42"};
        namedScores.AddRange(scores);
        namedScores.AddRange(PlayerPrefs.GetString("Scores").Split(' '));
        foreach (var item in namedScores)
            Debug.Log(item);
        
        SortScores();
        UpdateList();
    }

    private void SortScores()
    {
        List<Highscore> scores = new List<Highscore>();

        foreach (var item in namedScores)
        {
            int lastSpaceIndex = item.LastIndexOf(' ');
            scores.Add(new Highscore(item.Substring(0, lastSpaceIndex), item.Substring(lastSpaceIndex)));
        }

        scores.Sort();
        

        namedScores.Clear();

        foreach (var item in scores)
        {
            namedScores.Add(item.GetString());
            Debug.Log(item.GetString());
        }
            
    }

    private void UpdateList()
    {
        var stringbuilder = new StringBuilder();
        foreach (var item in namedScores) {
            stringbuilder.AppendLine(item);
        }
        highscoreList.text= stringbuilder.ToString();
    }

    public void SaveScore()
    {
        if (nameField.text.Length > 0)
        {
            var newScore = nameField.text + " " + score;
            var scores = PlayerPrefs.GetString("Scores");
            scores += newScore;
            PlayerPrefs.SetString("Scores", scores);

            namedScores.Add(newScore);

            SortScores();
            UpdateList();
        }
    }

}

public struct Highscore: IComparable<Highscore>
{
    public string name;
    public int score;
    public Highscore(string name, string score) { 
        this.name = name;
        this.score = int.Parse(score.Trim());
    }

    public int CompareTo(Highscore other) => other.score.CompareTo(score);

    public string GetString() => name + " " + score.ToString();


}
