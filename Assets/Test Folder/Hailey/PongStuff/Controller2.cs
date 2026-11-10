using UnityEngine;

public class Controller2 : MonoBehaviour


{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Player2;
    
    public float moveSpeed;

    public int paddleYbounds;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float newY = Player2.transform.position.y - (Time.deltaTime * moveSpeed);
             Player2.transform.position = new Vector2(Player2.transform.position.x, newY);
            
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            float newY = Player2.transform.position.y + (Time.deltaTime * moveSpeed);
             Player2.transform.position = new Vector2(Player2.transform.position.x, newY);
            
        }
    }
}
