using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float velocity_x = rb.velocity.x;
        if (velocity_x < 0) {
            velocity_x = -velocity_x;
        }
        anim.SetFloat("speed", velocity_x);
    }
}
