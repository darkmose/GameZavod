using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class Shooting : MonoBehaviour
{
    public IkCCD2D target;
    public Transform RightHand;
    public GameObject bullet;
    public Transform RightArm;
    public LayerMask layer;
    AudioSource playerSource;
    Animator animator;
    Inventory inv;
    Text AmmoText, AmmoText2;
    Image imgGun;
    public AudioClip[] sounds = new AudioClip[3];
    public AudioClip reloadSound;
    float reload = 0.0f, reloadSpeed = 1f, shootTime = 0.0f, shootSpeed;
    int currentAmmo, AmmoInOne, AllAmmo;



    private void Awake()
    {
        AmmoText = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
        AmmoText2 = transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
        imgGun = transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>();
        inv = GameObject.Find("GlobalScripts").GetComponent<Inventory>();
        playerSource = GameObject.Find("Player").GetComponent<AudioSource>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }


    private void OnEnable()
    {
        shootSpeed = 7 / inv.armor[4].speed;
        AllAmmo = inv.armor[4].AllAmmo;
        currentAmmo = inv.armor[4].AmmoCurOne;
        AmmoInOne = inv.armor[4].AmmoInOne;
        imgGun.sprite = Resources.Load<Sprite>(inv.armor[4].sprite);
        AmmoText.text = currentAmmo.ToString();
        AmmoText2.text = AllAmmo.ToString();
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.visible = true;
        imgGun.sprite = null;
        inv.armor[4].AllAmmo = AllAmmo;
        inv.armor[4].AmmoCurOne = currentAmmo;
    }

    void ToCursor()
    {
        target.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnDrawGizmosSelected()
    {
        
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0 && reload <= 0 && shootTime <= 0)
            {
                animator.Play("RobotShooting", 1);

                try
                {
                    RaycastHit2D hit = Physics2D.Raycast(GameObject.Find("Player").transform.position, (target.transform.position + transform.up * 0.35f) - GameObject.Find("Player").transform.position, 100f, layer);
                    {
                        if (hit.collider.tag == "Enemy")
                        {
                            hit.collider.gameObject.GetComponent<Enemy>().TakeDamageFunc(0.05f);
                        }
                    }
                }
                catch (System.NullReferenceException)
                {
                }
                finally
                {
                    currentAmmo--;
                    AmmoText.text = currentAmmo.ToString();
                    shootTime = shootSpeed;
                }
            }
        }
    }

    void Reload()
    {
        if (Input.GetButtonDown("Reload"))
        {
            //playerSource.PlayOneShot(reloadSound);
            if ((AllAmmo - AmmoInOne) >= 0)
            {
                AllAmmo -= (AmmoInOne - currentAmmo);
                currentAmmo = AmmoInOne;
            }
            else
            {
                currentAmmo = AllAmmo;
                AllAmmo = 0;
            }
            reload = reloadSpeed;
            AmmoText.text = currentAmmo.ToString();
            AmmoText2.text = AllAmmo.ToString();
        }
    }


    void Timer()
    {
        if (reload > 0)
        {
            reload -= Time.deltaTime;
        }
        if (shootTime > 0)
        {
            shootTime -= Time.deltaTime;
        }
    }

    void ToCursorWithGun()
    {

        if (GameObject.Find("GlobalScripts").GetComponent<Inventory>().isShooting && this.gameObject.activeInHierarchy)
        {
            float x = (GameObject.Find("Player").transform.position.x < transform.position.x) ? 1 : -1;
            GameObject.Find("Player").transform.localScale = new Vector2(x, 1);
        }
    }

    void Update()
    {
        ToCursor();
        Shoot();
        ToCursorWithGun();
        Reload();
        Timer();
    }
}
