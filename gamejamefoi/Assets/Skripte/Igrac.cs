using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igrac : MonoBehaviour
{
    public float brzina;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 micanjeBrzina;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        Vector2 micanjeinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        micanjeBrzina = micanjeinput.normalized * brzina;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + micanjeBrzina * Time.deltaTime);
    }
}
