using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    [Header("  Elements  ")]
    [SerializeField] GameObject deadLine;
    [SerializeField] Transform fruitsParents;
    void Start()
    {

    }

    void Update()
    {
        CheckForGameover();
    }

    void CheckForGameover()
    {
        for (int i = 0; i < fruitsParents.childCount; i++)
        {
            Fruit fruit = fruitsParents.GetChild(i).GetComponent<Fruit>();
            if (fruit.HasCollided())
            {
                CheckIfFruitAboveLine(fruitsParents.GetChild(i));
            }
        }
    }

    void CheckIfFruitAboveLine(Transform fruit)
    {
        if (fruit.position.y > deadLine.transform.position.y)
        {
            Debug.Log("Gameover");
        }
    }
}
