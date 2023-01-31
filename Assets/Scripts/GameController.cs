using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    [Header("Blades")]
    [SerializeField] private List<BladeController> blades;
    [SerializeField] private int bladeCount;
    private const int MAX_BLADE_COUNT = 6;
    [SerializeField] private List<int> bladeTiers;
    private const int MAX_TIER_LEVEL = 3;

    private const int MAX_ROTATION_SPEED_LEVEL = 10;
    [Header("Rotation")]
    [SerializeField] private int rotationSpeedLevel;

    private int MAX_BLADE_LENGTH_LEVEL = 5;
    [Header("Blade Level")]
    [SerializeField] private int bladeLengthLevel;

    [Header("Cash")]
    [SerializeField] private float totalCash;
    [SerializeField] private float totalIncome;

    [Header("UI")]
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text incomeText;

    [SerializeField] private ShopController shopController;

    [SerializeField] private GrassChanger grassChanger;

    private float timeAfterIncome;

    private bool mergeable = false;

    private void Awake()
    {
        SetBladeTiers(0);
        SetTotalIncome();
        timeAfterIncome = 0;
    }

    private void FixedUpdate() 
    {
        if (timeAfterIncome > 1)
        {
            totalCash += totalIncome;
            SetCashText();     
            timeAfterIncome = 0;
        }
        timeAfterIncome += Time.deltaTime;
    }

    private void SetCashText()
    {
        cashText.text = "$ " + totalCash.ToString("0.0");
    }

    private void SetIncomeText()
    {
        incomeText.text = "$ " + totalIncome.ToString("0.0") + " / sec";
    }

    private void SetBladeTiers(int start)
    {
        for (int i = start; i < blades.Count; i++)
        {
            blades[i].SetBladeTierLevel(bladeTiers[i]);
        }
    }

    public void SetTotalCash(float cash)
    {
        totalCash = cash; 
        SetCashText();
    }

    private void SetTotalIncome()
    {
        float total = 0;
        for (int i = 0; i < blades.Count; i++)
        {
            total = total + blades[i].GetIncome();
        }
        totalIncome = total;
        SetIncomeText();
    }

    public float GetTotalCash()
    {
        return totalCash;
    }

    public float GetTotalIncome()
    {
        return totalIncome;
    }

    public int GetRotationSpeedLevel()
    {
        return rotationSpeedLevel;
    }

    public int GetBladeLengthLevel()
    {
        return bladeLengthLevel;
    }

    public void BuyRotation()
    {
        if (CheckRotationSpeedLevel() && shopController.BuyRotation())
        {
            RotationSpeedLevelUp();
            grassChanger.SetRotationSpeed(rotationSpeedLevel);
        }
    }

    public void BuyLength()
    {
        if (CheckBladeLengthLevel() && shopController.BuyLength())
        {
            BladeLengthLevelUp();
            grassChanger.SetBladeLengthLevel(bladeLengthLevel);
        }
    }

    public void BuyBlade()
    {
        if (CheckBladeCount() && shopController.BuyBlade())
        {
            bladeCount += 1;
            bladeTiers[bladeCount - 1] = 1;
            SetBladeTiers(0);
            grassChanger.SetActiveGrassCount(bladeCount);
            mergeable = true;
        }
    }

    public void BuyMerge()
    {
        if (CheckMergeable() && shopController.BuyMerge())
        {
            for (int i = 0; i < bladeTiers.Count - 1; i++)
            {
                if (bladeTiers[i] + bladeTiers[i + 1] != 0 && 
                    bladeTiers[i] == bladeTiers[i + 1] &&
                    bladeTiers[i] + bladeTiers[i + 1] != MAX_TIER_LEVEL * 2)
                {
                    bladeTiers[i] += 1;

                    for (int x = i + 1; x < bladeTiers.Count - 1; x++)
                    {
                        bladeTiers[x] = bladeTiers[x + 1];
                    }
                    bladeCount -= 1;
                    bladeTiers[bladeTiers.Count - 1] = 0;
                    SetBladeTiers(i);
                    grassChanger.SetActiveGrassCount(bladeCount);
                    mergeable = true;
                    return;
                }
            }
            mergeable = false;
        }
    }

    private bool CheckMergeable()
    {
        return mergeable && bladeCount > 1;
    }

    private bool CheckBladeCount()
    {
        return bladeCount < MAX_BLADE_COUNT;
    }

    private bool CheckRotationSpeedLevel()
    {
        return rotationSpeedLevel < MAX_ROTATION_SPEED_LEVEL;
    }

    private bool CheckBladeLengthLevel()
    {
        return bladeLengthLevel < MAX_BLADE_LENGTH_LEVEL;
    }

    public void RotationSpeedLevelUp()
    {
        rotationSpeedLevel = rotationSpeedLevel + 1;
        for (int i = 0; i < blades.Count; i++)
        {
            blades[i].RotationSpeedLevelUp();
        }
        SetTotalIncome();
    }

    public void BladeLengthLevelUp()
    {
        bladeLengthLevel = bladeLengthLevel + 1;
        for (int i = 0; i < blades.Count; i++)
        {
            blades[i].BladeLengthLevelUp();
        }
        SetTotalIncome();
    }
}