using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunButton : MonoBehaviour
{
    public Player player;
    public Button runButton;

    void Start()
    {
        runButton.onClick.AddListener(OnRunButtonClicked);
    }

    void OnRunButtonClicked()
    {
        player.StartRunning();
        runButton.interactable = false;
        StartCoroutine(StopRunningAfterDelay(5));
    }

    private IEnumerator StopRunningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.StopRunning();
        runButton.interactable = true;
    }
}
