using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("  Actions  ")]
    public static Action<Fruit, Fruit> OnCollisionWithFruit;

    [Header("  Data  ")]
    [SerializeField] FruitType fruitType;

    void Start()
    {

    }

    void Update()
    {

    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void MoveTo(Vector2 pos)
    {
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.GetFruitType() != fruitType)
            {
                return;
            }
            //Destroy(fruit.gameObject);
            OnCollisionWithFruit?.Invoke(this, otherFruit); // gọi action
        }
    }

    public FruitType GetFruitType()
    {
        return fruitType;
    }
}
