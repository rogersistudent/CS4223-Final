using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class GameHandler : MonoBehaviour
{
    public Text timer;
    public Text points;
    public Text lives;
    public Text user;
    public int tempLives;
    public int tempPoints;
    public float tempTimer;
    public AudioSource music;


    // Start is called before the first frame update
    void Start()
    {
        music.Play(0);
        if(PlayerPrefs.GetInt("points") > 0)
        {
            tempPoints = PlayerPrefs.GetInt("points");
        } else
        {
            tempPoints = 0;
        }
        tempTimer = PlayerPrefs.GetFloat("startingTime");
        tempLives = PlayerPrefs.GetInt("lives");
        user.text = "Currently Playing: " + PlayerPrefs.GetString("username");
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = tempLives.ToString();
        points.text = tempPoints.ToString();
        if (tempTimer > 0)
        {
            tempTimer -= Time.deltaTime;
            timer.text = Mathf.Round(tempTimer) + "(seconds) left.";
        }
    }
    public void addPoint()
    {
        tempPoints++;
    }
    public void removePoint()
    {
        tempPoints--;
    }
    public void addLive()
    {
        tempLives++;
    }
    public void removeLive()
    {
        tempLives--;
    }
    public void nextScene()
    {
        PlayerPrefs.SetInt("score", tempPoints);
        AddNewScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int num_scores = 10;
    public void AddNewScore()
    {
        string path = "Assets/scores.txt";
        string line;
        string[] fields;
        int scores_written = 0;
        string newName = "don't forget to input";
        string newScore = "999";
        bool newScoreWritten = false;
        string[] writeNames = new string[10];
        string[] writeScores = new string[10];

        newName = PlayerPrefs.GetString("username");
        newScore = PlayerPrefs.GetInt("score").ToString();

        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            fields = line.Split(',');
            if (!newScoreWritten && scores_written < num_scores) // if new score has not been written yet
            {
                //check if we need to write new higher score first
                if (Convert.ToInt32(newScore) > Convert.ToInt32(fields[1]))
                {
                    writeNames[scores_written] = newName;
                    writeScores[scores_written] = newScore;
                    newScoreWritten = true;
                    scores_written += 1;
                }

            }
            if (scores_written < num_scores) // we have not written enough lines yet
            {
                writeNames[scores_written] = fields[0];
                writeScores[scores_written] = fields[1];
                scores_written += 1;
            }
        }
        reader.Close();

        // now we have parallel arrays with names and scores to write
        StreamWriter writer = new StreamWriter(path);

        for (int x = 0; x < scores_written; x++)
        {
            writer.WriteLine(writeNames[x] + ',' + writeScores[x]);
        }
        writer.Close();

        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("scores");

    }
}
