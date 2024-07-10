using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum ObjectType
{
    Empty, Tree1, Tree2, Tree3, BanditCamp 
};

public class Tile : MonoBehaviour
{
    [SerializeField] private ObjectType objectType;
    [SerializeField] private List<GameObject> prefabList;
    private int objectNumber;
    [SerializeField] private bool isBanditCamp = false;

    private void Awake() {
        objectType = ObjectType.Empty;
    }

    public void CreateEnvironment()
    {  
        switch (objectType)
        {
            case ObjectType.Empty:
                objectNumber = 0;
            break;
            case ObjectType.Tree1:
                objectNumber = 1;
            break;
            case ObjectType.Tree2:
                objectNumber = 2;
            break;
            case ObjectType.Tree3:
                objectNumber = 3;
            break;
        }

        GameObject temp = Instantiate(prefabList[objectNumber]);
        temp.transform.position = new Vector3(0, 0, -0.125f);
        temp.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
        temp.transform.SetParent(transform, false);
    }
};



