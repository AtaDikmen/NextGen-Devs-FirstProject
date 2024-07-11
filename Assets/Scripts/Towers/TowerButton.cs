using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType{
    AttackSpeed, Health, Speed
};

public class TowerButton : MonoBehaviour
{
    [SerializeField] private TowerType towerType;
    [SerializeField] private List<GameObject> canvasList;
    // [SerializeField] private 

    private int canvasNumber;
    private void Awake() {
        switch (towerType)
        {
            case TowerType.AttackSpeed:
                canvasNumber = 0;
            break;
            case TowerType.Health:
                canvasNumber = 1;
            break;
            case TowerType.Speed:
                canvasNumber = 2;
            break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DelayedUI());
        }
    }

    private IEnumerator DelayedUI()
    {
        yield return new WaitForSeconds(3);
        canvasList[canvasNumber].SetActive(true);
    }

}
