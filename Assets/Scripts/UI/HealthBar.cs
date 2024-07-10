using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the Slider component
    private Camera mainCamera;
    private Canvas healthBarCanvas;
    private Coroutine hideHealthBarCoroutine;
    void Start()
    {
        mainCamera = Camera.main;
        healthBarCanvas = GetComponentInParent<Canvas>();
        HideHealthBar();
    }

    void Update()
    {
        // Make the health bar look at the camera
        if (mainCamera != null)
        {
            Vector3 lookAtPosition = transform.position + mainCamera.transform.rotation * Vector3.forward;
            Vector3 upDirection = mainCamera.transform.rotation * Vector3.up;
            transform.LookAt(lookAtPosition, upDirection);
        }
    }

    public void SetHealth(float healthNormalized)
    {
        healthSlider.value = healthNormalized;
    }

    public void ShowHealthBar()
    {
        healthBarCanvas.enabled = true;
    }

    public void HideHealthBar()
    {
        healthBarCanvas.enabled = false;
    }

    public void ShowHealthBarTemporarily()
    {
        ShowHealthBar();
        if (hideHealthBarCoroutine != null)
        {
            StopCoroutine(hideHealthBarCoroutine);
        }
        hideHealthBarCoroutine = StartCoroutine(HideHealthBarAfterDelay());
    }

    private IEnumerator HideHealthBarAfterDelay()
    {
        yield return new WaitForSeconds(3);
        HideHealthBar();
    }
}
