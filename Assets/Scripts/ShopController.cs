using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    private const int MAX_ROTATION_SPEED_LEVEL = 10;
    [Header("Rotation")]

    [SerializeField] private float baseRotationPrice;
    [SerializeField] private float rotationPriceIncrement;
    private float totalRotationBought;
    private float rotationPrice;
    [SerializeField] private TMP_Text rotationPriceText;
    [SerializeField] private TMP_Text roationPriceLevel;

    private const int MAX_BLADE_LENGTH_LEVEL = 5;
    [Header("Blade Length")]

    [SerializeField] private float baseLengthPrice;
    [SerializeField] private float lengthPriceIncrement;
    private float totalLengthBought;
    private float lengthPrice;
    [SerializeField] private TMP_Text lengthPriceText;
    [SerializeField] private TMP_Text lengthPriceLevel;

    private const int MAX_BLADE_COUNT = 4;
    [Header("Blade Count")]
    [SerializeField] private float baseBladePrice;
    [SerializeField] private float bladePriceIncrement;
    private float bladePrice;
    private float totalBladesBought;
    [SerializeField] private TMP_Text bladePriceText;
    [SerializeField] private TMP_Text bladePriceLevel;

    [Header("Merge")]
    [SerializeField] private float baseMergePrice;
    [SerializeField] private float mergePriceIncrement;
    private int totalMergeBought;
    private float mergePrice;
    [SerializeField] private TMP_Text mergePriceText;

    [SerializeField] private GameController gameController;

    private void Start() {
        UpdateBladePrice();
        UpdateLengthPrice();
        UpdateRotationPrice();
        UpdateMergePrice();
    }

    public void UpdateRotationPrice()
    {
        rotationPrice = baseRotationPrice + totalRotationBought * rotationPriceIncrement;
        rotationPriceText.text = "$ " + rotationPrice.ToString("0.0");
    }

    public void UpdateLengthPrice()
    {
        lengthPrice = baseLengthPrice + totalLengthBought * lengthPriceIncrement;
        lengthPriceText.text = "$ " + lengthPrice.ToString("0.0");
    }

    public void UpdateBladePrice()
    {
        bladePrice = baseBladePrice + totalBladesBought * bladePriceIncrement;
        bladePriceText.text = "$ " + bladePrice.ToString("0.0");
    }

    public void UpdateMergePrice()
    {
        mergePrice = baseMergePrice + totalMergeBought * mergePriceIncrement;
        mergePriceText.text = "$ " + mergePrice.ToString("0.0");
    }

    public bool BuyRotation()
    {
        if (CheckBalance(rotationPrice))
        {
            gameController.SetTotalCash(gameController.GetTotalCash() - rotationPrice);
            totalRotationBought += 1;
            UpdateRotationPrice();
            return true;
        }
        return false;
    }

    public bool BuyLength()
    {
        if (CheckBalance(lengthPrice))
        {
            gameController.SetTotalCash(gameController.GetTotalCash() - lengthPrice);
            totalLengthBought += 1;
            UpdateLengthPrice();
            return true;
        }
        return false;
    }

    public bool BuyBlade()
    {
        if (CheckBalance(bladePrice))
        {
            gameController.SetTotalCash(gameController.GetTotalCash() - bladePrice);
            totalBladesBought += 1;
            UpdateBladePrice();
            return true;
        }
        return false;
    }

    public bool BuyMerge()
    {
        if (CheckBalance(mergePrice))
        {
            gameController.SetTotalCash(gameController.GetTotalCash() - mergePrice);
            totalMergeBought += 1;
            UpdateMergePrice();
            return true;
        }
        return false;
    }

    private bool CheckBalance(float price)
    {
        return gameController.GetTotalCash() >= price;
    }
}