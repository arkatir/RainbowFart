using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 pos;
    private Camera c;

    // Start is called before the first frame update
    void Start()
    {
        c = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -c.transform.position.z));
        transform.position = pos;
    }
}
