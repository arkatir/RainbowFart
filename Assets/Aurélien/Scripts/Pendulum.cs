using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private float length;
    private float theta0;
    private float t;
    private float theta;
    private float speed;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        length = transform.localScale.y;
        theta0 = transform.rotation.eulerAngles.z;
        theta = theta0;
        t = 0;
        speed = 9.8f / length;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.timeMove)
        {
            t += Time.deltaTime * speed;
        }
        theta = theta0 * Mathf.Cos(t);
        transform.rotation = Quaternion.AngleAxis(theta, Vector3.forward);
    }
}
