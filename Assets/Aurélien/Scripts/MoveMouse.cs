﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
//using System.Media;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveMouse : MonoBehaviour
{
    //Movement parameters
    [SerializeField] float speed = 0;
    [SerializeField] float minY = -10;
    private float radius;
    private float theta = 0;
    private float orientation = -1;
    [SerializeField] float rollSpeed = 1;
    [SerializeField] float speedEps = 0.1f;
    
    public Quaternion originalRotationValue; //Reset original rotation
    float rotationResetSpeed = 1.0f;

    //Loading parameters
    [SerializeField] float radiusMin = 1.0f;
    [SerializeField] float radiusMax = 5.0f;
    [SerializeField] float loadSpeed = 1.0f;
    private float loadDir = 1;

    //Audio parameters
    [SerializeField] private AudioSource star_s = null;
    [SerializeField] private AudioSource jumping_s = null;
    [SerializeField] private AudioSource jumpgrunt_s = null;
    [SerializeField] private AudioSource landgrunt_s = null;
    [SerializeField] private AudioSource rainbow_s = null;
    [SerializeField] private AudioSource landing_s = null;
    [SerializeField] private AudioSource warp_s = null;
    [SerializeField] private AudioSource flip_s = null;
    [SerializeField] private AudioSource death_s = null;

    //Mouse position variables
    private Camera c;
    private Vector3 pos;

    //Trajectory helpers variables
    private Vector2 center;
    private Vector3 direction;

    //Status flags
    private bool moving = false;
    private bool loading = false;
    private bool freeFall = false;
    private bool rolling = false;

    //Components
    private Rigidbody2D rb;
    private GameManager gameManager;

    //Trajectory helpers
    public GameObject trajectory;
    public GameObject power;
    private GameObject powerArrow;

    //Animations
    public GameObject Idle;
    public GameObject Charging;
    public GameObject Boule;

    //RainbowTrails
    //public GameObject[] trails;

    void Start()
    {
        //Initialize private parameters
        c = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        powerArrow = power.transform.GetChild(0).gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Initialize animation
        Idle.SetActive(true);
        Charging.SetActive(false);
        Boule.SetActive(false);

        originalRotationValue = transform.rotation; // save the initial rotation

        //Initialize Trails
        /*trails = new GameObject[7];
        trails[0] = GameObject.Find("RedTrail");
        trails[1] = GameObject.Find("OrangeTrail");
        trails[2] = GameObject.Find("YellowTrail");
        trails[3] = GameObject.Find("GreenTrail");
        trails[4] = GameObject.Find("CyanTrail");
        trails[5] = GameObject.Find("BlueTrail");
        trails[6] = GameObject.Find("PurpleTrail");*/
    }

    void Update()
    {
        if (!gameManager.gameOver)
        {
            //Idle.SetActive(true);
            //Charging.SetActive(false);
            //Boule.SetActive(false);

            //Initialization of the loading phase
            if (Input.GetMouseButtonDown(0) && !moving && !loading && !freeFall && EventSystem.current.currentSelectedGameObject == null)
            {
                Idle.SetActive(false);
                Charging.SetActive(true);

                trajectory.SetActive(true);

                radius = radiusMin;
                loading = true;
                pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -c.transform.position.z));
                direction = pos - transform.position;
                power.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1, 0), direction), Vector3.forward);

                //Activate Rainbow
                /*for (int i = 0; i < trails.Length; i++)
                {
                    trails[i].GetComponent<Renderer>().enabled = true;
                }*/
            }

            //Loading phase
            if (Input.GetMouseButton(0) && loading && !moving)
            {
                Idle.SetActive(false);
                Charging.SetActive(true);

                //Update radius
                if (radius > radiusMax)
                {
                    loadDir = -1;
                }

                if (radius < radiusMin)
                {
                    loadDir = 1;
                }
                radius += Time.deltaTime * loadSpeed * loadDir;

                //Update trajectory
                pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -c.transform.position.z));
                direction = pos - transform.position;
                power.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1, 0), direction), Vector3.forward);
                center = Rotate90((Vector2)transform.position + radius * (Vector2)direction.normalized, (Vector2)transform.position, orientation);

                //Update trajectory indicator
                UpdateTrajectory();

                //Update power indicator
                powerArrow.transform.localScale = new Vector3(0.1f, radius, 1);
                powerArrow.transform.localPosition = new Vector2(radius / 2, 0);
            }

            //Initialize circle movement
            if (Input.GetMouseButtonUp(0) && !moving && loading)
            {
                rainbow_s.Play(); 
                jumping_s.Play();
                jumpgrunt_s.Play();


                Idle.SetActive(false);
                Charging.SetActive(false);
                Boule.SetActive(true);

                trajectory.SetActive(false);

                theta = Vector2.SignedAngle(new Vector2(1, 0), direction) / 180 * Mathf.PI - orientation * Mathf.PI / 2;
                moving = true;
                loading = false;
                gameManager.timeMove = true;
                gameManager.jumpCounter += 1;
            }

            //Circle movement
            if (moving)
            {
                Idle.SetActive(false);
                Charging.SetActive(false);
                Boule.SetActive(true);

                
                theta += Time.deltaTime * speed / radius * orientation;
                transform.position = center + radius * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
                UpdateTrajectory();
            }

            //Character Rotation when in Freefall
            if (freeFall || rolling)
            {
                Idle.SetActive(false);
                Charging.SetActive(false);
                Boule.SetActive(true);
                transform.Rotate(new Vector3(0, 0, orientation * 20) * Time.deltaTime * 10);
            }


            //Reset character initial Rotation position on immobile
            if (!moving && !freeFall && !rolling)
            {
                Idle.SetActive(true);
                Charging.SetActive(false);
                Boule.SetActive(false);
                transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed); //Reset character to original rotation
            }
                


            //Flip character orientation
            if ((Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonUp(2)) && !moving)
            {
                flip_s.Play();

                orientation = -orientation;
                Idle.transform.localScale = new Vector3(-Idle.transform.localScale.x, Idle.transform.localScale.y, Idle.transform.localScale.z);
                Boule.transform.localScale = new Vector3(-Boule.transform.localScale.x, Boule.transform.localScale.y, Boule.transform.localScale.z);
                Charging.transform.localScale = new Vector3(-Charging.transform.localScale.x, Charging.transform.localScale.y, Charging.transform.localScale.z);
                //trajectory.transform.localScale = new Vector3(-trajectory.transform.localScale.x, trajectory.transform.localScale.y, trajectory.transform.localScale.z);
            }

            //Character Ball Movement
            if (Input.GetMouseButtonDown(1))
            {
                moving = false;
                rolling = true;
                rainbow_s.Play();

                Idle.SetActive(false);
                Charging.SetActive(false);
                Boule.SetActive(true);
            }

            if (Input.GetMouseButton(1))
            {
                float x = 2;
                float moveBy = x * rollSpeed;
                rb.velocity = new Vector2(- orientation * moveBy, rb.velocity.y);
                transform.Rotate(new Vector3(0, 0, orientation * 20) * Time.deltaTime * 10);
            }

            if (Input.GetMouseButtonUp(1) && rolling)
            {
                rolling = false;
                freeFall = true;
                rainbow_s.Stop();

                Idle.SetActive(true);
                Boule.SetActive(false);
            }

            //Death by falling
            if (transform.position.y < minY)
            {
                rainbow_s.Stop(); 
                death_s.Play();

                gameManager.GameOver();
            }
        }
    }

    //Free fall after collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (moving)
        {
            rainbow_s.Stop();
            landing_s.Play();
            
            Idle.SetActive(false);
            Boule.SetActive(true);

            //Recreate first bounce
            Vector2 normal = collision.GetContact(0).normal.normalized;
            Vector2 oldVel = speed * orientation * new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta));
            Vector2 newVel = (oldVel - 2 * Vector2.Dot(oldVel, normal) * normal) * collision.collider.bounciness;
            rb.velocity = newVel;
            
            moving = false;
            freeFall = true;
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (moving)
        {
            rainbow_s.Stop();
            landing_s.Play();
            landgrunt_s.Play();

            Idle.SetActive(false);
            Boule.SetActive(true);
            //Deactivate Rainbow
            /*for (int i = 0; i < trails.Length; i++)
            {
                trails[i].GetComponent<Renderer>().enabled = false;
            }*/

            //Recreate first bounce
            Vector2 normal = collision.GetContact(0).normal.normalized;
            Vector2 oldVel = speed * orientation * new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta));
            Vector2 newVel = (oldVel - 2 * Vector2.Dot(oldVel, normal) * normal) * collision.collider.bounciness;
            rb.velocity = newVel;

            moving = false;
            freeFall = true;
        }

        if (freeFall && (rb.velocity.magnitude < speedEps))
        {
            Idle.SetActive(false);
            Boule.SetActive(true);

            
            rb.velocity = Vector2.zero;
            freeFall = false;
            gameManager.timeMove = false;

        }
    }


    //Interact with other objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Warp"))
        {
            if (moving)
            {
                warp_s.Play();

                Idle.SetActive(false);
                Boule.SetActive(true);

                Idle.transform.localScale = new Vector3(-Idle.transform.localScale.x, Idle.transform.localScale.y, Idle.transform.localScale.z);
                Boule.transform.localScale = new Vector3(-Boule.transform.localScale.x, Boule.transform.localScale.y, Boule.transform.localScale.z);
                Charging.transform.localScale = new Vector3(-Charging.transform.localScale.x, Charging.transform.localScale.y, Charging.transform.localScale.z);

                Vector2 pivot = transform.position;
                center = Rotate90(center, pivot, orientation);
                orientation = -orientation;
                theta -= orientation * Mathf.PI / 2;
                UpdateTrajectory();
            }
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            star_s.Play();

            string starName = collision.gameObject.name;
            PlayerPrefs.SetInt(starName, 1);

            gameManager.DisplayStar(2 * (int.Parse(starName.Substring(starName.Length - 1)) - 1));

            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("End"))
        {
            rainbow_s.Stop(); 
            //victory_s.Play();

            gameManager.Victory();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            rainbow_s.Stop(); 
            death_s.Play();

            gameManager.GameOver();
        }
    }

    //Rotates v 90 degrees around pivot, counter-clockwise if orientation = 1.
    private Vector2 Rotate90(Vector2 v, Vector2 pivot, float orientation)
    {
        return pivot + orientation * new Vector2(pivot.y - v.y, v.x - pivot.x);
    }

    //Displays the circle that shows the way
    private void UpdateTrajectory()
    {
        trajectory.transform.position = center;
        trajectory.transform.localScale = radius * new Vector3(-orientation, 1, 1);
        trajectory.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(0, 1), direction), Vector3.forward);
    }
}