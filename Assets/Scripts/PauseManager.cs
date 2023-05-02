using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject pause;
    [System.Serializable]
    public class player
    {
        public string username;
        public int lives;
        public float seconds;
        public int points;
    }
    player newPlayer = new player();
    public Text points;
    public Text lives;
    public AudioSource music;
    public Toggle isMusic;

    // Update is called once per frame
    void Update()
    {
        if (isMusic.isOn)
        {
            music.Pause();
        } else
        {
            music.Play(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    public void continueButton()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    public void loadButton()
    {
        PlayerPrefs.SetString("username", newPlayer.username);
        PlayerPrefs.SetInt("lives", newPlayer.lives);
        PlayerPrefs.SetFloat("startingTime", newPlayer.seconds);
        PlayerPrefs.SetInt("points", newPlayer.points);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void saveButton()
    {
        newPlayer.points = int.Parse(points.text);
        newPlayer.lives = int.Parse(lives.text);
        newPlayer.seconds = PlayerPrefs.GetFloat("startingTime");
        newPlayer.username = PlayerPrefs.GetString("username");
        Debug.Log(newPlayer.points + " points, " +
            newPlayer.lives + " lives, " +
            newPlayer.seconds + " seconds, " +
            newPlayer.username);
    }
    public void newButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
