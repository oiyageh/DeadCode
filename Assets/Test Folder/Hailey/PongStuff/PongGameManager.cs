using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PongGameManager : MonoBehaviour

{
   public Rigidbody2D ballBody;
public InputActionAsset inputActions;

   public float forceAmount = 10f;


   public int xLaunchVelocity = 20;
   public int yLaunchVelocity = 15;

   public bool ballLaunched;


   public int score;

      public TextMeshProUGUI scoreTextBox;

   private InputAction startAction;


   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
startAction = inputActions.FindAction("Start", true);
        startAction.Enable();   }


   // Update is called once per frame
   void Update()
   {
       if (startAction.WasPressedThisFrame())
       { if(ballLaunched == false)
           {
                 StartBall();
                   ballLaunched = true;
           }


           Debug.Log("I pressed space");
       }
   }






   private void StartBall()
   {
       int rand = Random.Range(0, 2);


       Debug.Log("Rand returned " + rand);


       if (rand <1)
       {
          ballBody.AddForce(new Vector2(-xLaunchVelocity, yLaunchVelocity) * forceAmount);
       }
       else
       {
           ballBody.AddForce(new Vector2(xLaunchVelocity, yLaunchVelocity) * forceAmount);
       }


   }

    public void UpdateScore()
    {
        score+=100;
        scoreTextBox.text = "Score: " + score;
    }


}


