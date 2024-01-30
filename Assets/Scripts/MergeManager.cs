using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    Fruit lastSender;

    [Header("  Actions  ")]
    public static Action<FruitType, Vector2> OnMergeProcessed;

    void Start()
    {
        Fruit.OnCollisionWithFruit += CollisionFruitCb;
    }

    void Update()
    {

    }

    void CollisionFruitCb(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
        {
            return;
        }

        lastSender = sender;

        // merge
        ProcessMerge(sender, otherFruit);
        Debug.Log("Collision with: " + sender.name);
    }

    void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeFruitType = sender.GetFruitType();
        mergeFruitType += 1;

        Vector2 fruitSpawnPos = (sender.transform.position + otherFruit.transform.position) / 2;

        Destroy(sender.gameObject);
        Destroy(otherFruit.gameObject);

        StartCoroutine(ResetLastSenderCo());

        OnMergeProcessed?.Invoke(mergeFruitType, fruitSpawnPos);
    }

    IEnumerator ResetLastSenderCo()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
}
