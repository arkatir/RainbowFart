using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowMove2D : MonoBehaviour
{
    private Rigidbody2D rib;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float radius = 2.0f;
    private bool moving = false;
    private GameObject sp;
    private bool rigid = false;

    // Start is called before the first frame update
    void Start()
    {
        rib = GetComponent<Rigidbody2D>();
        sp = transform.Find("Speed").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !moving)
        {
            rib.velocity = Vector2.up * speed;
            moving = true;
        }

        if(moving)
        {
            rib.AddForce((new Vector3(radius, 0, 0) - transform.position).normalized * speed * speed / radius);
        }

        sp.transform.localPosition = rib.velocity;

        if(rigid)
        {
            rib.gravityScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moving = false;
        rib.velocity = Vector2.zero;
        rigid = true;
    }
}
