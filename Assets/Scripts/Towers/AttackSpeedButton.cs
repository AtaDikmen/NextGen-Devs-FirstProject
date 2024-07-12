using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedButton : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public bool isDone = false;


    [SerializeField] private GameObject brokenTower;
    [SerializeField] private GameObject fixedTower;


    public void CheckIsDone()
    {
        if (isDone)
        {
            brokenTower.SetActive(false);
            fixedTower.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (isDone)
                return;
            StartCoroutine(DelayCanvas());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isDone)
                return;
            StopAllCoroutines();
        }
    }

    private IEnumerator DelayCanvas()
    {
        yield return new WaitForSeconds(2);
        canvas.gameObject.SetActive(true);
    }
}
