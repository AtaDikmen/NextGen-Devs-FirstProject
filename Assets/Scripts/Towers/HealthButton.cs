using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthButton : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public bool isDone = false;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (isDone)
                return;
            StartCoroutine(DelayCanvas());           
        }
    }

    private IEnumerator DelayCanvas()
    {
        yield return new WaitForSeconds(2);
        canvas.gameObject.SetActive(true);
    }
}
