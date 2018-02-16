using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    int sec;
    int min;
    Text text;
    Hints hints;
    GameObject hintMenu;
    private void Awake()
    {
        hintMenu = GameObject.Find("Player").transform.GetChild(2).gameObject;
        hints = GameObject.Find("Player").GetComponent<Hints>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    public void SetTime(int _min)
    {
        text.text = "";
        StartCoroutine(settime(_min));
    }

    IEnumerator settime(int _min)
    {


        sec = 60;
        min = _min - 1;
        Show(sec, min);
        yield return new WaitForSeconds(1);

        do
        {
            while (sec > 0)
            {
                sec--;
                Show(sec, min);
                yield return new WaitForSeconds(1);
            }

            min--;
            sec = 60;

        } while (min > 0);
        hintMenu.SetActive(true);
        hints.SetText("Приближаются враги");
        GameObject.Find("GlobalScripts").GetComponent<World>().Invasion();
        gameObject.SetActive(false);
    }

    void Show(int sec, int min)
    {
        text.text = ((min < 9) ? ("0" + min.ToString()) : min.ToString()) + " : " + ((sec < 9) ? ("0" + sec.ToString()) : sec.ToString());
    }

}
