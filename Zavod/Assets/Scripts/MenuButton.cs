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

        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            PlayerPrefs.Save();
        }
        GameObject.Find("Main Camera").GetComponentInChildren<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetFloat("SFX", 0.5f);
            PlayerPrefs.Save();
        }
      

    }

    public void Exit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void StartGame() {
          SceneManager.LoadScene("scene0", LoadSceneMode.Single);        
    }

    public void Optionss()
    {
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        music.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music")*100;
        sfx.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX") * 100;

    }
    public void OptionExit()
    {        
        PlayerPrefs.SetFloat("Music", music.GetComponent<Slider>().value / 100);
        PlayerPrefs.SetFloat("SFX", sfx.GetComponent<Slider>().value / 100);
        PlayerPrefs.Save();
        GameObject.Find("Options").SetActive(false);
        GameObject.Find("Main Camera").GetComponentInChildren<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
    }


}
