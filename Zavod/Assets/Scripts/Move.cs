﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10f, jforce = 5f, runspeed = 15f;
    GameObject gmo;
    Animator animar;
    Rigidbody2D rig;
    public bool flag = true;
    [HideInInspector]
    public bool isAttack = false;
    bool inAir = false;
    bool run = true;
    HelpPlayer helpPlayer;
    ParticleSystem particles;
    public bool move = true;


    void Start()
    {
        helpPlayer = GameObject.Find("Player").GetComponent<HelpPlayer>();
        gmo = GameObject.Find("Player");
        particles = gmo.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        animar = gmo.GetComponentInChildren<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }


    void GroundCheck()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        inAir = colls.Length == 1;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && move)
        {
            if (!GameObject.Find("GlobalScripts").GetComponent<Inventory>().isShooting)
            {

                if (!isAttack && helpPlayer.energy > 15)
                {
                    helpPlayer.EnergyAdd(-15);
                    animar.Play("RobotAttack1", 1);
                    StartCoroutine(attack());
                }
            }

        }
    }
    IEnumerator attack()
    {
        isAttack = true;
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
    }
    IEnumerator jumptimer()
    {
        flag = false;
        yield return new WaitForSecondsRealtime(0.7f);
        flag = true;
        yield break;
    }
    IEnumerator canrun()
    {
        while (helpPlayer.energy < 50)
        {
            yield return new WaitForFixedUpdate();
        }
        run = true;
    }

    void Jump()
    {
        if (!inAir)
        {
            if (Input.GetButtonDown("Jump") && Time.timeScale > 0 && flag)
            {
                if (helpPlayer.energy > 10 && move)
                {
                    particles.enableEmission = false;
                    helpPlayer.EnergyAdd(-15);
                    rig.AddForce(Vector2.up * jforce, ForceMode2D.Impulse);
                    animar.SetInteger("move", 3);
                    StartCoroutine("jumptimer");
                }
            }
        }
    }

    void Walk()
    {
        if (Input.GetButton("Horizontal") && move)
        {
            if (Time.timeScale > 0 && (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0))
            {
                if (GameObject.Find("WEATHER").transform.GetChild(0).gameObject.activeSelf)
                {
                    particles.enableEmission = true;
                }

                gmo.transform.position = Vector2.MoveTowards(gmo.transform.position, new Vector2(gmo.transform.position.x + 5 * Input.GetAxisRaw("Horizontal"), gmo.transform.position.y), Time.deltaTime * speed);
                gmo.transform.localScale = new Vector2(Input.GetAxisRaw("Horizontal"), 1);
                animar.SetInteger("move", 1);
            }
        }
        else
        {
            particles.enableEmission = false;
            animar.SetInteger("move", 0);
        }
    }

    void Run()
    {
        if (Input.GetButton("Horizontal") && move)
        {
            if (Input.GetButton("Run") && Time.timeScale > 0f && (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0))
            {
                if (helpPlayer.energy > 5 && run)
                {
                    if (!inAir && GameObject.Find("WEATHER").transform.GetChild(0).gameObject.activeSelf)
                    {
                        particles.enableEmission = true;
                    }

                    helpPlayer.EnergyAdd(-1);
                    gmo.transform.position = Vector2.MoveTowards(gmo.transform.position, new Vector2(gmo.transform.position.x + 5 * Input.GetAxisRaw("Horizontal"), gmo.transform.position.y), Time.deltaTime * runspeed);
                    gmo.transform.localScale = new Vector2(Input.GetAxisRaw("Horizontal"), 1);
                    animar.SetInteger("move", 2);
                }
                else
                {
                    run = false;
                    StartCoroutine(canrun());
                }

            }
        }
    }

    void Update()
    {
        Walk();
        Jump();
        Run();
        Attack();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

}
