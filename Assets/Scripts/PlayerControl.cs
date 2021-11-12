using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upBtn = KeyCode.W;
    public KeyCode downBtn = KeyCode.S;
    public float speed = 10f;
    public float yBoundary = 9f;

    private Rigidbody2D rb;
    private int score;
    private ContactPoint2D lastContactPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //movement player up/down
        Vector2 velocity = rb.velocity;
        if (Input.GetKey(upBtn))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downBtn))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0f;
        }
        rb.velocity = velocity;

        //player movement boundary
        Vector3 pos = transform.position;
        if(pos.y > yBoundary)
        {
            pos.y = yBoundary;
        }
        else if(pos.y < -yBoundary)
        {
            pos.y = -yBoundary;
        }
        transform.position = pos;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score()
    {
        return score;
    }

    public ContactPoint2D LastContactPoint()
    {
         return lastContactPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
