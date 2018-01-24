using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Levels : MonoBehaviour {

    public GameObject logo;
    public GameObject clickToCont;


    private void Start()
    {
        this.gameObject.SetActive(false);
        clickToCont.SetActive(false);
        logo.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
    }


    IEnumerator loadLevel(int scene) {
        Time.timeScale = 0;
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
        float x1 = transform.GetChild(3).GetComponent<RectTransform>().rect.width - (transform.GetChild(3).GetComponent<RectTransform>().rect.width*0.2f) ;
        
        while (true) {
            x1 = x1 / 100f * operation.progress;
            Vector2 pos = new Vector2(x1,x1);
            logo.GetComponent<RectTransform>().offsetMin = Vector2.MoveTowards(logo.GetComponent<RectTransform>().offsetMin, pos, 5);
            logo.GetComponent<RectTransform>().anchorMin = logo.GetComponent<RectTransform>().rect.min;
            logo.GetComponent<RectTransform>().anchorMax = logo.GetComponent<RectTransform>().rect.max;

            if (operation.isDone)
            {
                clickToCont.SetActive(true);
                if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
                {
                    Time.timeScale = 1;
                    operation.allowSceneActivation = true;
                    break;
                }
            }
            yield return null;
        }
    }

    public void LoadLevel(int level)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(loadLevel(level));
    }

}
