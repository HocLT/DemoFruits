using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [Header("  Elements  ")]
    [SerializeField] Fruit fruitPrefab;

    [Header("  Settings  ")]
    [SerializeField] float fruitYSpawnPos = 3.5f;

    [Header("  Debugs  ")]
    [SerializeField] bool enableGizmos;

    [SerializeField] LineRenderer fruitSpawnLine;

    Fruit currentFruit;

    bool canControl;
    bool isControlling;

    void Start()
    {
        canControl = true;
        HideLine();
    }

    void Update()
    {
        if (canControl)
        {
            ManagePlayerInput();
        }
    }

    void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCb();
        }
        else if (Input.GetMouseButtonUp(0) && isControlling)
        {
            MouseUpCb();
        }
        else if (Input.GetMouseButton(0) && isControlling)
        {
            MouseDragCb();
        }

        // Vector2 spawnPos = GetClickedWorldPosition();
        // spawnPos.y = fruitYSpawnPos;
        //Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
    }

    Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void MouseDownCb()
    {
        DisplayLine();  // hiện line

        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);

        SpawnFruit();
        isControlling = true;
    }

    void MouseUpCb()
    {
        HideLine();

        // chuyển fruit bodyType => dynamic
        currentFruit.EnablePhysics();

        canControl = false;
        isControlling = false;
        StartControlTimer();
    }

    void MouseDragCb()
    {
        PlaceLineAtClickedPosition();
        // currentFruit.transform.position = new Vector2(GetSpawnPosition().x, fruitYSpawnPos);
        currentFruit.MoveTo(new Vector2(GetSpawnPosition().x, fruitYSpawnPos));
    }

    void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 clickedPosition = GetClickedWorldPosition();
        clickedPosition.y = fruitYSpawnPos;
        return clickedPosition;
    }

    void SpawnFruit()
    {
        Vector2 spawnPos = GetSpawnPosition();
        currentFruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
    }

    void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    void StartControlTimer()
    {
        Invoke("StopControlTimer", .5f);
    }

    void StopControlTimer()
    {
        canControl = true;
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!enableGizmos)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-50, fruitYSpawnPos, 0), new Vector3(50, fruitYSpawnPos, 0));
    }
#endif
}
