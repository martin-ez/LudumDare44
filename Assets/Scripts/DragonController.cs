﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    private Vector2 velocity;
    public float speed;
    public float maxPhysicsSpeed = 10f;
    public float slowdownMultiplier = 5f;
    public float jumpForce = 10f;

    private Vector2 input = Vector2.zero;
    private Rigidbody2D rb;

    public float maxPersonalGravityScale = 1.0f;
    public float minPersonalGravityScale = 0.25f;
    public float currentPersonalGravityScale = 1.0f;
    private float currentPersonalGravityLerp = 0.0f;

    public bool onGround = false;
    public LayerMask groundLayers;

    public float airTimeMax = 5.0f;
    private float airTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayers);
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 0.505f), new Vector2(1, 0.01f));
    }

    private void FixedUpdate()
    {       
        if (onGround)
        {
            airTime = 0f;
            rb.gravityScale = 1f;
            if (input.y > 0f)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

            if (input.x == 0f)
            {
                rb.AddForce(new Vector2(rb.velocity.x * -slowdownMultiplier, 0f), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(input.x * speed, 0f), ForceMode2D.Force);
                Debug.Log(rb.velocity.x * speed);
            }
        }
        else
        {
            airTime += Time.fixedDeltaTime;
            if (airTime > airTimeMax)
            {
                rb.gravityScale = 1f;
                return;
            }
            rb.gravityScale = 0f;
            if (input == Vector2.zero)
            {
                rb.AddForce(rb.velocity * -slowdownMultiplier, ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(input.normalized * speed, ForceMode2D.Force);
            }
        }

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxPhysicsSpeed);
    }
}
