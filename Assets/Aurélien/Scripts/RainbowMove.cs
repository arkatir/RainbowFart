using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float radius = 2.0f;
    private bool moving = false;
    private GameObject sp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = transform.Find("Speed").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && !moving)
        {
            rb.velocity = Vector2.up * speed;
            moving = true;
        }

        if(moving)
        {
            rb.AddForce((new Vector3(radius, 0, 0) - transform.position).normalized * speed * speed / radius);
        }

        sp.transform.localPosition = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moving = false;
        rb.velocity = Vector2.zero;
    */}
}
