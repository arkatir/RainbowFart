using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MoveMouseRebound : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        c = Camera.main;
        rb = GetComponent<Rigidbody2D>();
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
            /*Debug.Log("pos = " + transform.position);
            Debug.Log("direction = " + direction);
            Debug.Log("directionN = " + direction.normalized);
            Debug.Log("center = " + center);*/
            trajectory.transform.position = center;
            trajectory.transform.localScale = 2 * radius * Vector3.one;
        }

        if (Input.GetMouseButtonUp(0) && !moving && loading)
        {
            /*if (orientation == 1)
            {
                theta = 0;
            }
            else
            {
                theta = Mathf.PI;
            }*/

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moving = false;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}