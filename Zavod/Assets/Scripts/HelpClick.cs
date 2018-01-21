using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpClick : MonoBehaviour {
    public void Enter()
    {
        if (GetComponent<Button>().interactable==true)
        {
            GetComponents<AudioSource>()[1].Play();
        }
        
    }
    public void Click()
    {
        GetComponents<AudioSource>()[0].Play();
    }
}
