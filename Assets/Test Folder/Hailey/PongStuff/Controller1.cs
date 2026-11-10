
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Controller1 : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Player1;

    public float moveSpeed;

    public int paddleYbounds;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
       
        
    }
    


    // Update is called once per frame
    void Update()

    {
        

         // s

            if (Input.GetKey(KeyCode.S))
    
            {    float newY = Player1.transform.position.y - (Time.deltaTime * moveSpeed);
    
                    Player1.transform.position = new Vector2(Player1.transform.position.x, newY);
            
    
                Debug.Log("I am pressing S");
            }
    
            // w

            if (Input.GetKey(KeyCode.W))

            { float newY = Player1.transform.position.y + (Time.deltaTime * moveSpeed);

                    Player1.transform.position = new Vector2(Player1.transform.position.x, newY);


        }



    }
}
