using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouse : MonoBehaviour
{
    //Movement parameters
    [SerializeField] float speed = 0;
    [SerializeField] float minY = -10;
    private float radius;
    private float theta = 0;
    private float orientation = -1;
    [SerializeField] float speedEps = 0.1f;

    //Loading parameters
    [SerializeField] float radiusMin = 1.0f;
    [SerializeField] float radiusMax = 5.0f;
    [SerializeField] float loadSpeed = 1.0f;
    private float loadDir = 1;

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

    //Components
    private Rigidbody2D rb;
    private GameManager gameManager;

    //Trajectory helpers
    public GameObject trajectory;
    public GameObject power;
    private GameObject powerArrow;


    void Start()
    {
        //Initialize private parameters
        c = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        powerArrow = power.transform.GetChild(0).gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.gameOver)
        {
            //Initialization of the loading phase
            if (Input.GetMouseButtonDown(0) && !moving && !loading && !freeFall)
            {
                radius = radiusMin;
                loading = true;
                pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -c.transform.position.z));
                direction = pos - transform.position;
                power.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1, 0), direction), Vector3.forward);
            }

            //Loading phase
            if (Input.GetMouseButton(0) && loading && !moving)
            {
                //Update radius
                if(radius > radiusMax)
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
                theta = Vector2.SignedAngle(new Vector2(1, 0), direction) / 180 * Mathf.PI - orientation * Mathf.PI / 2;
                moving = true;
                loading = false;
            }

            //Circle movement
            if (moving)
            {
                theta += Time.deltaTime * speed / radius * orientation;
                transform.position = center + radius * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
            }

            //Flip character orientation
            if (Input.GetKeyDown(KeyCode.F) && !moving)
            {
                orientation = -orientation;
            }

            //Death by falling
            if(transform.position.y < minY)
            {
                gameManager.GameOver();
            }
        }
    }

    //Free fall after collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moving)
        {
            //Recreate first bounce
            Vector2 normal = collision.GetContact(0).normal.normalized;
            Vector2 oldVel = speed * orientation * new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta));
            Vector2 newVel = (oldVel - 2 * Vector2.Dot(oldVel, normal) * normal) * collision.collider.bounciness;
            rb.velocity = newVel;

            moving = false;
            freeFall = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (freeFall && (rb.velocity.magnitude < speedEps))
        {
            rb.velocity = Vector2.zero;
            freeFall = false;
        }
    }


    //Warp object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Warp"))
        {
            if (moving)
            {
                Vector2 pivot = transform.position;
                center = Rotate90(center, pivot, orientation);
                orientation = -orientation;
                theta -= orientation * Mathf.PI / 2;
                UpdateTrajectory();
            }
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            gameManager.Victory();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
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
        trajectory.transform.localScale = 2 * radius * Vector3.one;
    }
}