using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{

    [SerializeField] private List<GameObject> tiers;

    private const int MAX_ROTATION_SPEED_LEVEL = 10;
    [Header("Rotation")]
    [SerializeField] private int rotationSpeedLevel;
    [SerializeField] private float rotationSpeedIncrement;
    [SerializeField] private float baseRotationSpeed;
    private float rotationSpeed;

    private int MAX_BLADE_LENGTH_LEVEL = 5;
    [Header("Blade Level")]
    [SerializeField] private int bladeLengthLevel;
    [SerializeField] private float bladeLengthIncrement;
    [SerializeField] private float baseBladeLength;
    private float bladeLength;

    private int MAX_TIER_LEVEL = 5;
    [Header("Blade Tier")]
    [SerializeField] private int bladeTierLevel;

    [Header("Income")]
    [SerializeField] private float income;
    [SerializeField] private float baseIncome;
    [SerializeField] private float incomeIncrementGeneral;
    [SerializeField] private float incomeIncrementRotation;
    [SerializeField] private float incomeIncrementLength;
    [SerializeField] private float incomeIncrementTier;

    private GameObject bladeParent;
    private GameObject baseParent;
    private GameObject activeTier;

    void Awake()
    {
        rotationSpeed = baseRotationSpeed + rotationSpeedLevel * rotationSpeedIncrement;
        bladeLength = baseBladeLength + bladeLengthLevel * rotationSpeedIncrement;
        SetRotationSpeed();
        SetBladeTier();
        SetIncome();
    }

    private void FixedUpdate()
    {
        RotateBlade();
    }

    private void SetRotationSpeed()
    {
        rotationSpeed = baseRotationSpeed + rotationSpeedLevel * rotationSpeedIncrement;
    }

    private void SetBladeLength()
    {
        bladeLength = baseBladeLength + bladeLengthLevel * bladeLengthIncrement;
        bladeParent.transform.GetChild(0).localPosition = new Vector3(bladeLength ,0, 0);
        baseParent.transform.localScale = new Vector3(0, bladeLength / 2, 0.05f);
    }

    public void SetBladeTier()
    {
        for (int i = 0; i < tiers.Count; i++)
        {
            tiers[i].SetActive(false);
        }
        if (bladeTierLevel >= 1)
        {
            tiers[bladeTierLevel - 1].SetActive(true);
            activeTier = tiers[bladeTierLevel - 1];
            bladeParent = activeTier.transform.GetChild(0).gameObject;
            baseParent = activeTier.transform.GetChild(1).gameObject;
            SetBladeLength();
        }
    }

    private void SetIncome()
    {
        income = baseIncome + incomeIncrementGeneral * (bladeTierLevel * incomeIncrementTier + 
                                                 rotationSpeedLevel * incomeIncrementRotation + 
                                                 bladeLengthLevel * incomeIncrementLength);
    }

    public void SetBladeLengthLevel(int level)
    {
        if (level < MAX_BLADE_LENGTH_LEVEL)
        {
            bladeLengthLevel = level;
            SetBladeLength();
            SetIncome();
        }
    }

    public void SetRotationSpeedLevel(int level)
    {
        if (level < MAX_ROTATION_SPEED_LEVEL)
        {
            rotationSpeedLevel = level;
            SetRotationSpeed();
            SetIncome();
        }
    }

    public void SetBladeTierLevel(int level)
    {
        if (level < MAX_TIER_LEVEL)
        {
            bladeTierLevel = level;
            SetBladeTier();
            SetIncome();
        }
    }
    
    private void RotateBlade()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }

    public float GetIncome()
    {
        return income;
    }

    public int GetBladeTierLevel()
    {
        return bladeTierLevel;
    }

    public int GetRotationSpeedLevel()
    {
        return rotationSpeedLevel;
    }

    public int GetBladeLengthLevel()
    {
        return bladeLengthLevel;
    }

    public void RotationSpeedLevelUp()
    {
        if (rotationSpeedLevel < MAX_ROTATION_SPEED_LEVEL)
        {
            rotationSpeedLevel = rotationSpeedLevel + 1;
            SetRotationSpeed();
            SetIncome();
        }
    }

    public void BladeLengthLevelUp()
    {
        if (bladeLengthLevel < MAX_BLADE_LENGTH_LEVEL)
        {
            bladeLengthLevel = bladeLengthLevel + 1;
            SetBladeLength();
            SetIncome();
        }
    }

    public void BladeTierLevelUp()
    {
        if (bladeTierLevel < MAX_TIER_LEVEL)
        {
            bladeTierLevel = bladeTierLevel + 1;
            SetIncome();
        }
    }
}