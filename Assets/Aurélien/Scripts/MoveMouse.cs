using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveMouse : MonoBehaviour
{
    private float theta;
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

    // Start is called before the first frame update
    void Start()
    {
        c = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        bounce = rb.sharedMaterial.bounciness;
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
            if (pos.x > transform.position.x)
            {
                orientation = -1;
            }
            else
            {
                orientation = 1;
            }
        }

        if (Input.GetMouseButton(0) && loading && !moving)
        {
            radius += Time.deltaTime;
            center = (Vector2)transform.position + radius * (Vector2)direction.normalized;
            trajectory.transform.position = center;
            trajectory.transform.localScale = 2 * radius * Vector3.one;
        }

        if (Input.GetMouseButtonUp(0) && !moving && loading)
        {
            theta = Vector2.SignedAngle(new Vector2(1, 0), -direction) / 180 * Mathf.PI;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moving)
        {
            Vector2 normal = collision.GetContact(0).normal.normalized;
            Vector2 oldVel = speed * radius * orientation * new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta));
            Vector2 newVel = (oldVel - 2 * Vector2.Dot(oldVel, normal) * normal) * collision.collider.bounciness;
            moving = false;
            rigid = true;
            rb.velocity = newVel;
        }
        
    }
}