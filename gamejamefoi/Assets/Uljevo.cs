using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uljevo : MonoBehaviour
{
    public float brzina;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * brzina * Time.deltaTime;
        if (transform.position.x < -15f)
            Destroy(gameObject);
    }

}
