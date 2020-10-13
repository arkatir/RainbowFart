using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlat : MonoBehaviour
{
    private Vector2 startPos;
    [SerializeField] Vector2 endPos = new Vector2(0,0);
    private float t;
    [SerializeField] float speed = 0.5f;
    private float dist;
    private float moveDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        dist = (endPos - startPos).magnitude;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (t > 1)
        {
            moveDir = -1;
        }

        if (t < 0)
        {
            moveDir = 1;
        }

        t += moveDir * speed * Time.deltaTime / dist;
        transform.position = endPos * t + startPos * (1 - t);
    }
}
