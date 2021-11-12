using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float xInitForce;
    public float yInitForce;

    private Rigidbody2D rb;
    private Vector2 trajectoryOrigin;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    void ResetBall()
    {
        //Reset ball position
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //Push ball to random direction
        float yRandInitForce = Random.Range(0, 1);
        float randDirection = Random.Range(0, 2) % 2;
        yRandInitForce = (yRandInitForce == 0f) ? -yInitForce : yInitForce;
        if(randDirection < 1f)
        {
            rb.AddForce(new Vector2(-xInitForce, yRandInitForce));
        }
        else
        {
            rb.AddForce(new Vector2(xInitForce, yRandInitForce));
        }
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin()
    {
        return trajectoryOrigin;
    }
}
