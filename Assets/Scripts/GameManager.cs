using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform mainPlayer;
    public bool isEnemyWavePhase;
    private float countdownTime = 10f; // 1.5 minutes in seconds
    private bool isPlayerInsideBase = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdown(countdownTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyWavePhase && enemyParent.childCount == 0)
        {
            isEnemyWavePhase = false;
            StartCoroutine(StartCountdown(countdownTime));
            timer.SetActive(true);
        }
        if(!mainPlayer)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    private IEnumerator StartCountdown(float duration)
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText(remainingTime);
            yield return null;
        }
        UpdateTimerText(0);
        isEnemyWavePhase = true;
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

    public bool IsAnyPlayerAlive()
    {
        foreach (Transform child in playerParent)
        {
            if (child.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void SetPlayerInsideBase(bool value)
    {
        isPlayerInsideBase = value;
    }

}
