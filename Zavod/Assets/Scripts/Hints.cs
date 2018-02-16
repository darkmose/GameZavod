using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hints : MonoBehaviour
{

    Text txtField;
    RectTransform rect;
    string _string;
    float rectx;
    float rectHeight;
    float pos;

    void Start()
    {
        txtField = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        rect = gameObject.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
        rectx = rect.localScale.x;
        rectHeight = rect.sizeDelta.y;
        pos = rect.localPosition.y;
    }

    public void SetText(string sstring)
    {
        StopCoroutine("charSet");
        _string = sstring;
        float fieldSize;
        if (_string.Length > 30)
        {
            fieldSize = _string.Length * 3f;
            rect.sizeDelta = new Vector2(fieldSize, rectHeight);
            rect.localPosition = new Vector2(rect.localPosition.x, pos);
        }
        else
        {
            fieldSize = _string.Length * 10f;
            rect.sizeDelta = new Vector2(fieldSize, rectHeight / 3);
            rect.localPosition = new Vector2(rect.localPosition.x, 1.92f);
        }


        txtField.text = "";
        StartCoroutine("charSet");
    }

    IEnumerator charSet()
    {
        for (int i = 0; i < _string.Length; i++)
        {
            txtField.text += _string[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.localScale.x < 0)
        {
            rect.localScale = new Vector2(rectx * transform.localScale.x, rect.localScale.y);
        }
        else
        {
            rect.localScale = new Vector2(rectx * transform.localScale.x, rect.localScale.y);
        }

    }

}
