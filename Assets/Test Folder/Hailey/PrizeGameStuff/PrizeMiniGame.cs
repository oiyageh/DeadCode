using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Yarn.Unity;
[System.Serializable]
public class PrizeSender : UnityEvent<Prize> { }
public class PrizeMiniGame : MonoBehaviour
{
    
    [SerializeField] private List<Prize> PrizeList;

    [System.Serializable]
    public struct minigameDelays
    {
        public float timeBeforeCharacterAsk;
        public float timeBeforePlayerTurn;
    }
    [Header("Delays")]
    [SerializeField] private minigameDelays delays;

    [System.Serializable]
    public struct minigameEvents
    {
        public UnityEvent OnMinigameStart;
        public UnityEvent OnPlayerWin;
        public UnityEvent OnPlayerFail;
        public PrizeSender OnPrizeRequested;
    }
    [Header("Events")]
    [SerializeField] private minigameEvents events;

    [System.Serializable]
    public struct cameraVariables
    {
        public CamController mainCameraController;
        public Transform aimTransform;
        public bool GizmosOn;
        public float gizmoRadius;
    }

    [Header("Camera Variables")]
    public cameraVariables camVariables;

    private Prize requestedPrize;

    private bool miniGameStarted = false;
    private bool playerEnded = false;
    void Awake()
    {
        miniGameStarted = false;
        playerEnded = false;

        if(camVariables.mainCameraController == null)
        {
            camVariables.mainCameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>();
            if(camVariables.mainCameraController == null)
            {
                Debug.Log("ERROR: No Main Camera found.");
            }
        }

        if(camVariables.aimTransform == null)
        {
            camVariables.aimTransform = GameObject.FindGameObjectWithTag("AimTransform").transform;
            if (camVariables.aimTransform == null)
            {
                Debug.Log("ERROR: No aim Transform found.");
            }
        }
    }

    [ContextMenu("Test Start Minigame")]
    [YarnCommand("StartMinigame")]
    public void StartGame()
    {
        if (miniGameStarted)
        {
            Debug.Log("Minigame already started");
            return;
        }
        miniGameStarted = true;
        StartCoroutine(PrizeMiniGameSequence());
    }


    public IEnumerator PrizeMiniGameSequence()
    {
        //Prize minigame steps:
        //Ask for a specific prize
        //Player must turn around and click on one prize
        //If player clicks wrong prize, return angry
        //If player clicks right prize, Do Event
        playerEnded = false;
        events.OnMinigameStart?.Invoke();
        Debug.Log("Minigame Started");
        yield return new WaitForSeconds(delays.timeBeforeCharacterAsk);

        requestedPrize = GetRandomPrize();
        Debug.Log(requestedPrize);
        events.OnPrizeRequested?.Invoke(requestedPrize);
        Debug.Log("Prize Requested");
        yield return new WaitForSeconds(delays.timeBeforePlayerTurn);
        camVariables.mainCameraController.TurnToFace(camVariables.aimTransform);
        Debug.Log("Player Turned");

        //Waiting for player to select an object
        
        while (true)
        {
            if (playerEnded) break;
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Minigame Ended");
        miniGameStarted = false;
    }
    public LayerMask prizeLayer;
    void Update()
    {
        if (miniGameStarted && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Ray mouseDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(mouseDirection,out hitInfo, 20f, prizeLayer))
            {
                if(hitInfo.transform.GetComponent<PrizeContainer>().thisPrize.name == requestedPrize.name)
                {

                    Debug.Log("Won!");
                    playerEnded = true;
                    events.OnPlayerWin?.Invoke();
                }
                else
                {
                    Debug.Log("Lost");
                    playerEnded = true;
                    events.OnPlayerFail?.Invoke();
                }
            }

            
        }
        
    }
    public Prize GetRandomPrize()
    {
        return PrizeList[Random.Range(0, PrizeList.Count)];
    }

    public void OnDrawGizmos()
    {
        if (camVariables.GizmosOn == false) return;
        if (camVariables.gizmoRadius <= 0f) return;

        Gizmos.color = new Color(0, 255, 0, 1);
        Gizmos.DrawWireSphere(camVariables.mainCameraController.GetCam().position, camVariables.gizmoRadius);

        if (camVariables.aimTransform == null) return;
        Gizmos.color = new Color(0, 0, 255, 1);
        Gizmos.DrawLine(camVariables.mainCameraController.GetCam().position, camVariables.aimTransform.position);
        Gizmos.DrawWireSphere(camVariables.aimTransform.position, camVariables.gizmoRadius);
    }
}

