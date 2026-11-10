using UnityEngine;

public class PongBallController : MonoBehaviour
{ 
    public Vector2 startPosition;

    public Rigidbody2D myBody;

    public PongGameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.transform.CompareTag("Brick"))
        {
             manager.UpdateScore();
        } else if (collision.transform.CompareTag("Wall"))
        {
            myBody.linearVelocity = Vector2.zero;
            gameObject.transform.position = startPosition;
            manager.ballLaunched = false;
        }
    }
    }
