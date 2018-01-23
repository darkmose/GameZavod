using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{ 
    bool attck = true;
    IEnumerator timer()
    {
        attck = false;
        yield return new WaitForSeconds(0.55f);
        attck = true;
    }

    IEnumerator Attack(Collider2D collision) {
        yield return new WaitForSeconds(0.2f);
        transform.parent.GetComponent<HelpPlayer>().AttackDamage(collision.gameObject);
        collision.gameObject.GetComponent<Animator>().Play("EnemyDamage");
        float scale = (float)collision.GetComponent<Enemy>().hp / (float)collision.GetComponent<Enemy>().maxhp * 0.6f;
        int color = 200 / collision.gameObject.GetComponent<Enemy>().maxhp * collision.gameObject.GetComponent<Enemy>().hp;
        byte colorp = (byte)color;
        collision.transform.GetChild(1).GetChild(0).transform.localScale = new Vector2(scale, 0.3f);
        collision.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color32(colorp, 0, 0, 194);
        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.transform.position.x - transform.parent.position.x, 3) * 2, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Input.GetMouseButtonDown(0) && attck)
            {
                StartCoroutine(timer());
                StartCoroutine(Attack(collision));                
            }
        }
    }
    




}
