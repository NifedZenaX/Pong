using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl p1;
    private Rigidbody2D p1rb;

    public PlayerControl p2;
    private Rigidbody2D p2rb;

    public BallControl ball;
    private Rigidbody2D ballrb;
    private CircleCollider2D ballcoll;
    private bool isDebugWindowShown = false;

    public int maxScore;
    public Trajectory trajectory;
    void Start()
    {
        p1rb = p1.GetComponent<Rigidbody2D>();
        p2rb = p2.GetComponent<Rigidbody2D>();
        ballrb = ball.GetComponent<Rigidbody2D>();
        ballcoll = ball.GetComponent<CircleCollider2D>();
    }

    private void OnGUI()
    {
        //GUI 
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + p1.Score());
        GUI.Label(new Rect(Screen.width / 2 + 150 - 12, 20, 100, 100), "" + p2.Score());
        
        if(GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 30), "Restart"))
        {
            p1.ResetScore();
            p2.ResetScore();

            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if(p1.Score() == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "Player 1 Wins");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if(p2.Score() == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "Player 2 Wins");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //GUI for debug window
        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            float ballMass = ballrb.mass;
            Vector2 ballVelocity = ballrb.velocity;
            float ballSpeed = ballrb.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballcoll.friction;

            float impulseP1X = p1.LastContactPoint().normalImpulse;
            float impulseP1Y = p1.LastContactPoint().tangentImpulse;
            float impulseP2X = p2.LastContactPoint().normalImpulse;
            float impulseP2Y = p2.LastContactPoint().tangentImpulse;

            string debugText =
                   "Ball mass = " + ballMass + "\n" +
                   "Ball velocity = " + ballVelocity + "\n" +
                   "Ball speed = " + ballSpeed + "\n" +
                   "Ball momentum = " + ballMomentum + "\n" +
                   "Ball friction = " + ballFriction + "\n" +
                   "Last impulse from player 1 = (" + impulseP1X + ", " + impulseP1Y + ")\n" +
                   "Last impulse from player 2 = (" + impulseP2X + ", " + impulseP2Y + ")\n";
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
            GUI.backgroundColor = oldColor;
        }

        if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "Toggle\nDebug Info"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}
