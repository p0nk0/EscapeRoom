using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    wait,
    startGame,
    playing,
    itemScanned,
    winGame,
    loseGame,
    gameOver,
};

public class GameStateManager : MonoBehaviour
{
    private readonly HashSet<GameState> validStates = new HashSet<GameState>
    {
        GameState.wait,
        GameState.startGame,
        GameState.playing,
        GameState.itemScanned,
        GameState.winGame,
        GameState.loseGame,
        GameState.gameOver
    };
    private GameState state = GameState.wait;

    [Header("Game Settings")]
    [SerializeField] float timeLimit = 1800f; // 30 minutes in seconds
    [SerializeField] private int maxScans = 5;
    private HashSet<string> itemsScanned = new HashSet<string>();
    private string scannedItem;
    private float startTime = 0f;
   
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    [Header("Misc")]
    //TODO: audio

    private HashSet<string> validProps = new HashSet<string>
    {
        "f3 dc 28 36", // Moon Tarot Card
        "f1 a6 f6 7b", // Amber
        "c3 4f 4a dd", // Thymos 1
        // "ed 6d ac b9", // Thymos 2
        "83 a8 53 dd", // Hairbrush
        "9d 0d 72 b9", // Honey

        // "13 af 7a 36", // Random card
        // "33 f0 0a 36", // Another random card
    };

      private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
      {
        {"f3 dc 28 36", "Moon Tarot Card"},
        {"f1 a6 f6 7b", "Amber"},
        {"c3 4f 4a dd", "Thymos 1"},
        // {" ed 6d ac b9", "Thymos 2"},
        {"83 a8 53 dd", "Hairbrush"},
        {"9d 0d 72 b9", "Honey"},
        {"13 af 7a 36", "Random card"},
        {"33 f0 0a 36", "Another random card"},
    };


    void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    void Update()
    {
        if (startTime > 0f)
        {
            float elapsedTime = Time.time - startTime;
            float timeleft = Mathf.Max(0f, timeLimit - elapsedTime);

            if (elapsedTime >= timeLimit && state != GameState.gameOver)
            {
                startTime = 0f; // Stop the timer
                state = GameState.loseGame;
                Debug.Log("User ran out of time");
            }

            timer.text = $"Time Left: {Mathf.FloorToInt(timeleft / 60):00}:{Mathf.FloorToInt(timeleft % 60):00}";

        }

        switch (state)
        {
            case GameState.wait:
                losePanel.SetActive(false);
                winPanel.SetActive(false);
                if (Input.anyKeyDown)
                {
                    state = GameState.startGame;
                }
                break;
            case GameState.startGame:
                startTime = Time.time;
                itemsScanned = new HashSet<string>();
                state = GameState.playing;
                AudioManager.Instance.Play("Intro");
                Debug.Log("Starting game");
                break;
            case GameState.playing:
                if (itemsScanned.Count >= maxScans)
                {
                    state = GameState.winGame;
                }
                break;
            case GameState.itemScanned:
                if (scannedItem != null)
                {
                    if (itemsScanned.Add(scannedItem)) // new item
                    {
                        AudioManager.Instance.Play("Potion Success");
                        if (itemsScanned.Count == 1)
                        {
                            AudioManager.Instance.Play("Potion Start");
                            break;
                        }
                        Debug.Log("New item scanned: " + itemDescriptions[scannedItem]);
                        state = GameState.playing;
                    }
                    else
                    {
                        Debug.Log("Item already scanned: " + itemDescriptions[scannedItem]);
                        state = GameState.playing;
                    }
                }
                break;
            case GameState.winGame:
                StartCoroutine(WinGameSequence());
                winPanel.SetActive(true);
                state = GameState.gameOver;
                break;
            case GameState.loseGame:
                StartCoroutine(LoseGameSequence());
                losePanel.SetActive(true);
                state = GameState.gameOver;
                break;
            case GameState.gameOver:
                // waits while other coroutines run
                break;
        }
    }

    private IEnumerator WinGameSequence()
    {
        Debug.Log("starting win game sequence");
        AudioManager.Instance.Play("Good Ending");
        yield return new WaitForSeconds(5f);
        startTime = 0;
        state = GameState.wait;
        Debug.Log("transitioning to wait state");
    }
    
    private IEnumerator LoseGameSequence()
    {
        Debug.Log("starting lose game sequence");
        AudioManager.Instance.Play("Bad Ending");
        yield return new WaitForSeconds(5f);
        startTime = 0;
        state = GameState.wait;
        Debug.Log("transitioning to wait state");
    }

    public void HandleScannedItem(string item) {
        item = item.Trim();
        if (state == GameState.playing)
        {
            if (!validProps.Contains(item))
            {
                Debug.Log("Invalid item scanned: " + item);
                return;
            }
            Debug.Log("Scanned item: " + itemDescriptions[item]);
            scannedItem = item;
            state = GameState.itemScanned;
        } else if (state == GameState.wait)
        {
            Debug.Log("Item scanned, starting game");
            state = GameState.startGame;
            return;
        }
        else
        {
            Debug.Log($"Item scanned but game state is {state.ToString()}");
            return;

        }
    }
}
