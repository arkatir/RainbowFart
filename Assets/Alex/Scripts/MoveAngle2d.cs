using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveAngle2d: MonoBehaviour
{   
    private Rigidbody2D rib;
    private float theta2D;
    [SerializeField] float speed2D = 0;
    [SerializeField] float radius2D;
    private bool moving2D = false;
    private bool loading2D = false;
    private Vector2 center2D;
    public GameObject trajectory2D;
    private bool rigid = false;

    // Start is called before the first frame update
    void Start()
    {
        rib = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !moving2D && !loading2D)
        {
            radius2D = 1;
            loading2D = true;
        }

        if (Input.GetKey(KeyCode.Space) && loading2D && !moving2D)
        {
            radius2D += Time.deltaTime;
            center2D = (Vector2)transform.position + new Vector2(radius2D, 0);
            trajectory2D.transform.position = center2D;
            trajectory2D.transform.localScale = 2 * radius2D * Vector3.one;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !moving2D && loading2D)
        {
            theta2D = 0;
            moving2D = true;
            center2D = (Vector2)transform.position + new Vector2(radius2D, 0);
            trajectory2D.transform.position = center2D;
            trajectory2D.transform.localScale = 2 * radius2D * Vector3.one;
            loading2D = false;
        }

        if (moving2D)
        {
            theta2D += Time.deltaTime * speed2D / radius2D;
            transform.position = center2D + radius2D * new Vector2(-Mathf.Cos(theta2D), Mathf.Sin(theta2D));
        }

        if (rigid)
        {
            rib.gravityScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moving2D = false;
        rigid = true;
    }
}
