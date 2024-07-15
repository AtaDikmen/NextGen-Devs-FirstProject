using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public enum GameStates
{
    rescueState,
    surviveState
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStates currentState;

    //Audio
    private AudioManager audioManager;
    private AudioClip lastSeconds;

    public List<GameObject> totalSpawnedEnemies;
    public List<CampSpawner> campSpawners;
    public List<AllySpawner> allySpawners;

    //UI
    [SerializeField] private GameObject timer;
    [SerializeField] private TextMeshProUGUI timerText;


    [SerializeField] private Transform baseSpawnPoint;
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform mainPlayer;
    public bool isEnemyWavePhase;
    private float countdownTime = 20f; // 1.5 minutes in seconds
    private bool isPlayerInsideBase = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
        lastSeconds = Resources.Load<AudioClip>("LastSeconds");

        StartCoroutine(StartCountdown(countdownTime));
    }

    private void KillAllEnemies()
    {
        while (totalSpawnedEnemies.Count > 0)
        {
            GameObject enemy = totalSpawnedEnemies[totalSpawnedEnemies.Count-1];
            totalSpawnedEnemies.RemoveAt(totalSpawnedEnemies.Count - 1);
            Destroy(enemy);
        }
    }
    void Update()
    {
        if(isEnemyWavePhase && totalSpawnedEnemies.Count == 0)
        {
            TeleportToBase();
            isEnemyWavePhase = false;
            StartCoroutine(StartCountdown(countdownTime));
            timer.SetActive(true);

            foreach (CampSpawner campSpawner in campSpawners) 
            {
                campSpawner.isUnitsSpawned = false;
            }

            foreach (AllySpawner allySpawner in allySpawners)
            {
                allySpawner.isSpawned = false;
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator StartCountdown(float duration)
    {
        bool isAudioPlaying = false;
        currentState = GameStates.rescueState;

        float remainingTime = duration;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText(remainingTime);

            if (remainingTime <= 11 && !isAudioPlaying)
            {
                audioManager.PlaySFX(lastSeconds);
                isAudioPlaying = true;
            }
            yield return null;
        }
        UpdateTimerText(0);
        isEnemyWavePhase = true;
        currentState = GameStates.surviveState;
        timer.SetActive(false);

        if(!isPlayerInsideBase)
        {
            GameOver();
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public Transform GetPlayerParent()
    {
        return playerParent;
    }


    public void SetPlayerInsideBase(bool value)
    {
        isPlayerInsideBase = value;
    }

    private void TeleportToBase()
    {
        for (int i = 0; i < playerParent.childCount; i++)
        {
            if (i == 0)
            {
                playerParent.GetChild(i).transform.position = baseSpawnPoint.position;
            }
            else
            {
                playerParent.GetChild(i).GetChild(0).transform.position = baseSpawnPoint.position + new Vector3(i,0,0);
            }
        }
    }
}
