using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Levels : MonoBehaviour {

    public GameObject logo;
    public GameObject clickToCont;


    private void Start()
    {        
        clickToCont.SetActive(false);
    }


    IEnumerator loadLevel(int scene) {
        Time.timeScale = 0;
        
       
       
        float x1 = transform.GetChild(3).GetComponent<RectTransform>().rect.width - (transform.GetChild(3).GetComponent<RectTransform>().rect.width*0.2f) ;
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;
        while (true) {

            if (operation.progress == 0.9f)
            {   clickToCont.SetActive(true);            
                if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
                {
                    Time.timeScale = 1;
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }

    public void LoadLevel(int level)
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.SetActive(true);
            StartCoroutine(loadLevel(level));
        }        
        
    }

}
