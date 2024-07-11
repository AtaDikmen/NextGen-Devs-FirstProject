using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunButton : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Button runButton;

    void Start()
    {
        runButton.onClick.AddListener(OnRunButtonClicked);
    }

    void OnRunButtonClicked()
    {
        playerMovement.StartRunning();
        runButton.interactable = false;
        StartCoroutine(StopRunningAfterDelay(5));
    }

    private IEnumerator StopRunningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerMovement.StopRunning();
        runButton.interactable = true;
    }
}
