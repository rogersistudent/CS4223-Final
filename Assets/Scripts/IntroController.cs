using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{

    public InputField username;
    public Slider startingTime;
    public Dropdown lives;
    public Text startingT;


    private void Update()
    {
        startingT.text = startingTime.value.ToString() + " seconds";
    }


    public void StartGame()
    {
        if(lives.value == 0)
        {
            Debug.Log("Please select a number of lives.");
        } else
        {
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.SetFloat("startingTime", startingTime.value);
            PlayerPrefs.SetInt("lives", lives.value);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
