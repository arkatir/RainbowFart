using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMouse : MonoBehaviour
{
    private float theta = 0;
    [SerializeField] float speed = 0;
    [SerializeField] float radius;
    private bool moving = false;
    private bool loading = false;
    private Vector2 center;
    public GameObject trajectory;
    private Camera c;
    private Vector3 pos;
    private Vector3 direction;
    private float orientation = -1;
    private Rigidbody2D rb;
    private bool rigid = false;
    private float bounce;
    public GameObject power;
    private GameObject powerArrow;


    // Start is called before the first frame update
    void Start()
    {
        c = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        bounce = rb.sharedMaterial.bounciness;
        powerArrow = power.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !moving && !loading)
        {
            radius = 1;
            loading = true;
            pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -c.transform.position.z));
            direction = pos - transform.position;
            /*if (pos.x > transform.position.x)
            {
                orientation = -1;
            }
            else
            {
                orientation = 1;
            }*/
            power.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1, 0), direction), Vector3.forward);
        }

        if (Input.GetMouseButton(0) && loading && !moving)
        {
            radius += Time.deltaTime;
            /*center = (Vector2)transform.position + radius * (Vector2)direction.normalized;*/
            center = Rotate90((Vector2)transform.position + radius * (Vector2)direction.normalized, (Vector2)transform.position, orientation);
            trajectory.transform.position = center;
            trajectory.transform.localScale = 2 * radius * Vector3.one;
            powerArrow.transform.localScale = new Vector3(0.1f, radius, 1);
            powerArrow.transform.localPosition = new Vector2(radius / 2, 0);
        }

        if (Input.GetMouseButtonUp(0) && !moving && loading)
        {
            theta = Vector2.SignedAngle(new Vector2(1, 0), direction) / 180 * Mathf.PI - orientation * Mathf.PI / 2;
            moving = true;
            trajectory.transform.position = center;
            trajectory.transform.localScale = 2 * radius * Vector3.one;
            loading = false;
        }

        if (moving)
        {
            theta += Time.deltaTime * speed / radius * orientation;
            transform.position = center + radius * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        }

        /*if (rigid)
        {
            rb.gravityScale = 1f;
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.F) && !moving)
        {
            orientation = -orientation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moving)
        {
            Vector2 normal = collision.GetContact(0).normal.normalized;
            Vector2 oldVel = speed * orientation * new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta));
            Vector2 newVel = (oldVel - 2 * Vector2.Dot(oldVel, normal) * normal) * collision.collider.bounciness;
            moving = false;
            rigid = true;
            rb.velocity = newVel;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide");
        Vector2 pivot = transform.position;
        center = Rotate90(center, pivot, orientation);
        /*center = pivot + orientation * new Vector2(pivot.y - center.y, center.x - pivot.x);*/
        orientation = -orientation;
        theta -= orientation * Mathf.PI / 2;
    }

    private Vector2 Rotate90(Vector2 v, Vector2 pivot, float orientation)
    {
        return pivot + orientation * new Vector2(pivot.y - v.y, v.x - pivot.x);
    }
}