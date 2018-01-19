using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public float birdSpeed = 10f;
    public int coins = 0;
    public Vector2 startpos;
    Vector2 flu;
    //public GameObject groundPanel;
    //public Transform ground;
    //public int count;
    //float size;
    public Transform spawn1, spawn2;
    public GameObject solduer;
    public float speedSoldier = 7f;
    
    public int firetime = 4; 


    void Start () {
    StartCoroutine("BirdInst");
        Physics2D.IgnoreLayerCollision(10, 10, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
        Physics2D.IgnoreLayerCollision(10, 8, true);
        StartCoroutine(InvasionEvent());
    }

    IEnumerator InvasionEvent() {
        while (true)
        {
            GameObject.Find("Vertolety").GetComponent<Animator>().SetBool("invasion", true);
            //yield return new WaitForSeconds(3);
            Debug.Log("INVASION!!!!!!!!");
            int k = Random.Range(1, 5);
            Debug.Log(k);
            for (int i = 0; i < k; i++)
            {
                SoldierInst();
                yield return new WaitForSeconds(2);
            }
            GameObject.Find("Vertolety").GetComponent<Animator>().SetBool("invasion", false);
            yield return new WaitForSeconds(70);
        }
    }
    

 /*   void StartGround() {
        float x = 0;
        Vector2 pos = new Vector2(x,0);
        GameObject panel = Instantiate<GameObject>(groundPanel,pos,Quaternion.identity,ground);
        for (int i = 0; i < count;i++)
        {
            x += size;
            pos = new Vector2(x, 0);
            panel = Instantiate<GameObject>(groundPanel, pos, Quaternion.identity, ground);
            pos = new Vector2(-x, 0);
            panel = Instantiate<GameObject>(groundPanel, pos, Quaternion.identity, ground);
        }        
    }*/

    IEnumerator BirdInst()
    {
        while (true)
        {
            GameObject bird = Instantiate(Resources.Load<GameObject>("prefabs/Bird"),startpos, Quaternion.identity, GameObject.Find("Fly").transform);
            for ( ;bird.transform.position.x < 119f; )
            {
                flu = bird.transform.position;
                flu.x += 20;
                bird.transform.position = Vector2.MoveTowards(bird.transform.position, flu, Time.deltaTime*birdSpeed);
                yield return null;
            }
            Destroy(bird);
            yield return null;
        }
    }

    void SoldierInst()
    {
        Vector2 pos = spawn1.position;
        GameObject sold = Instantiate(solduer,pos,Quaternion.identity);
        bool j = false;
        StartCoroutine(timer(2, j));
    kek:
        if (true)
        {
            StartCoroutine(soldier(sold));//1 
        }
        else goto kek;

        pos = spawn2.position;
        sold = Instantiate(solduer, pos, Quaternion.identity);
        j = false;
        StartCoroutine(timer(2,j));
    lol:
        if (true)
        {
            StartCoroutine(soldier(sold));//2
        }
        else goto lol;     
        
    }

    IEnumerator timer(int col,bool j) {
        for (int i = 0; i < col; i++)
        {
          yield return new WaitForSeconds(1);
        }
        j = true;
    }

    public IEnumerator soldier(GameObject sold)
    {
        Vector2 target = sold.transform.position ;
        for (; ; )
        {
            if (sold.GetComponent<Enemy>().hp==0)
            {
                Destroy(sold);
                yield break;
            }
            if (sold.transform.position.x < (GameObject.Find("Player").transform.position.x + (Screen.width / 2) - 4) * -sold.transform.localScale.x)
            {
                sold.GetComponent<Animator>().SetInteger("Do", 2);
                target.x = GameObject.Find("Player").transform.position.x;
                sold.transform.position = Vector2.MoveTowards(sold.transform.position, target, Time.deltaTime * speedSoldier);
                yield return null;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    sold.GetComponent<Animator>().SetInteger("Do", 1);
                }
                yield return new WaitForSeconds(firetime);                
            }            
        }          
    }



}
