using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeather : MonoBehaviour {
    public GameObject snow, rain, normal;
    public void Snow()
    {
        Time.timeScale = 0;
        snow.SetActive(true);
        rain.SetActive(false);
        normal.SetActive(false);
        Time.timeScale = 1;
    }
    public void Normal()
    {
        Time.timeScale = 0;
        snow.SetActive(false);
        rain.SetActive(false);
        normal.SetActive(true);
        Time.timeScale = 1;
    }
    public void Rain()
    {
        Debug.Log("Rain");
        Time.timeScale = 0;
        snow.SetActive(false);
        rain.SetActive(true);
        normal.SetActive(false);
        Time.timeScale = 1;
    }
}
