using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FruitManager))]
public class FruitManagerUI : MonoBehaviour
{
    [Header("  Elements  ")]
    //[SerializeField] TextMeshProUGUI nextFruitLabel;
    [SerializeField] Image nextFruitImage;
    FruitManager fruitManager;

    void Awake()
    {
        fruitManager = GetComponent<FruitManager>();
        FruitManager.OnNextFruitIndexSet += UpdateNextFruitImage;
    }

    void UpdateNextFruitImage()
    {
        //nextFruitLabel.text = fruitManager.GetNextFruitName();
        nextFruitImage.sprite = fruitManager.GetNextFruitSprite();
    }
}
