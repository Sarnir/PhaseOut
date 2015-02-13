using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    enum EGameState
    {
        Start,
        Gameplay,
        GameOver
    }

    [SerializeField()]
    PlayerController player;
    [SerializeField()]
    GameObject obstaclePrefab;
    [SerializeField()]
    ObstacleManager obstacleManager;
    [SerializeField()]
    Text UIText;
    [SerializeField()]
    Score score;

    EGameState currentState;
    
    bool touchEnabled;

    // Use this for initialization
    void Start()
    {
        touchEnabled = true;
        EnterState(EGameState.Start);
    }

    void EnterState(EGameState gameState)
    {
        currentState = gameState;
        switch (gameState)
        {
            case EGameState.Start:
                UIText.text = "Tap to start!";
                obstacleManager.enabled = false;
                break;
            case EGameState.Gameplay:
                player.Reset();
                DestroyObstacles();
                UIText.text = "";
                score.ResetScoreAndStart(true);
                obstacleManager.enabled = true;
                break;
            case EGameState.GameOver:
                //Destroy(currentSequence);
                touchEnabled = false;
                score.SetUpdateScore(false);
                Invoke("SetGameOver", 1.0f);
                obstacleManager.enabled = false;
                break;
            default:
                break;
        }
    }

    void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    void SetGameOver()
    {
        touchEnabled = true;
        UIText.text = "Game over.\nTap to start again!";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTouches();

        if (currentState == EGameState.Gameplay)
        {
            //if (!currentSequence)
            //    currentSequence = gameObject.AddComponent<Sequence>();

            if (!player.isAlive)
            {
                EnterState(EGameState.GameOver);
            }

        }
    }

    private void CheckForTouches()
    {
        if (!touchEnabled)
            return;

        if (Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        TouchStarted();
                        break;
                    case TouchPhase.Canceled:
                        break;
                    case TouchPhase.Ended:
                        TouchEnded();
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Stationary:
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) == true)
                TouchStarted();

            if (Input.GetMouseButtonUp(0) == true)
                TouchEnded();
        }
    }

    private void TouchStarted()
    {
        switch (currentState)
        {
            case EGameState.Gameplay:
                player.PhaseOut(true);
                break;
        }
    }

    private void TouchEnded()
    {
        switch (currentState)
        {
            case EGameState.Start:
                EnterState(EGameState.Gameplay);
                break;
            case EGameState.Gameplay:
                player.PhaseOut(false);
                break;
            case EGameState.GameOver:
                EnterState(EGameState.Gameplay);
                break;
        }
    }
}
