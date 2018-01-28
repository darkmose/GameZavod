using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public bool isPause=false, canQuit;
    public GameObject can;
    public GameObject can1;
    public GameObject options;
    GameObject player;
    public Slider music,sound;


    void Start () {
        can.SetActive(false);
        can1.SetActive(false);
        canQuit = false;
        player = GameObject.Find("Player");
    }

  public  void Continue() {
        player.SetActive(true);
        Time.timeScale = 1f;
        isPause = false;
        can.SetActive(false);
    }

    public void Yes() {
        Time.timeScale = 1f;
        can.SetActive(false);
        can1.SetActive(false);
        canQuit = true;
    }
    public void No() {
        can1.SetActive(false);
        can.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        can.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        can.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
    }

    public void Quit() {
        can.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        can.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        can.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
        can1.SetActive(true);

    }

    public void Options() {
        options.SetActive(true);
        music.value = PlayerPrefs.GetFloat("Music") * 100;
        sound.value = PlayerPrefs.GetFloat("SFX") * 100;        
    }
    public void OptionsExit()
    {
        PlayerPrefs.SetFloat("Music", music.value / 100);
        PlayerPrefs.SetFloat("SFX", sound.value / 100);
        PlayerPrefs.Save();
        GameObject.Find("WEATHER").transform.GetChild(0).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
        GameObject.Find("WEATHER").transform.GetChild(1).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
        GameObject.Find("WEATHER").transform.GetChild(2).GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
        options.SetActive(false);
    }



    void Exit()
    {
        
        if (canQuit)
        {   can.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            can.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            can.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }


    void Update () {
        if (!isPause && Input.GetButtonDown("Cancel") && !GameObject.Find("GlobalScripts").GetComponent<Inventory>().isInv)
        {
            player.SetActive(false);
            can.SetActive(true);
            isPause = true;
            Time.timeScale = 0f;
        }
        else if (isPause && Input.GetButtonDown("Cancel"))
        {
            player.SetActive(true);
            can1.SetActive(false);
            can.SetActive(false);
            isPause = false;
            Time.timeScale = 1f;

        }            
   }
    private void FixedUpdate()
    {
        Exit();
    }
}
