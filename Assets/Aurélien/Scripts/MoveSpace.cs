using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpace : MonoBehaviour
{
    private float theta;
    [SerializeField] float speed;
    [SerializeField] float radius;
    private bool moving = false;
    private bool loading = false;
    private Vector2 center;
    public GameObject trajectory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !moving && !loading)
        {
            radius = 1;
            loading = true;
        }

        if (Input.GetKey(KeyCode.Space) && loading && !moving)
        {
            radius += Time.deltaTime;
            center = (Vector2)transform.position + new Vector2(radius, 0);
            trajectory.transform.position = center;
            trajectory.transform.localScale = 2 * radius * Vector3.one;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !moving && loading)
        {
            theta = 0;
            moving = true;
            center = (Vector2)transform.position + new Vector2(radius, 0);
            trajectory.transform.position = center;
            trajectory.transform.localScale = 2 * radius * Vector3.one;
            loading = false;
        }

        if (moving)
        {
            theta += Time.deltaTime * speed / radius;
            transform.position = center + radius * new Vector2(-Mathf.Cos(theta), Mathf.Sin(theta));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moving = false;
    }
}
