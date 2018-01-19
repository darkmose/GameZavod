using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class MenuButton : MonoBehaviour {
    public Transform music;
    public Transform sfx;
    private void Start()
    {
        PlayerPrefs.SetInt("Music", 50);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame() {
          SceneManager.LoadScene("scene0", LoadSceneMode.Single);        
    }

    public void Optionss()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        music.GetComponent<Slider>().value = PlayerPrefs.GetInt("Music");

    }
    public void OptionExit() {
        int mus = (int)music.GetComponent<Slider>().value;
        PlayerPrefs.SetInt("Music", mus);
        GameObject.Find("Main Camera").GetComponentInChildren<AudioSource>().volume = PlayerPrefs.GetInt("Music");
        GameObject.Find("Options").SetActive(false);
    }

}
