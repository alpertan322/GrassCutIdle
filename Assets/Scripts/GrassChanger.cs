using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassChanger : MonoBehaviour
{
    [SerializeField] private List<Texture> masks5;
    [SerializeField] private List<Texture> masks4;
    [SerializeField] private List<Texture> masks3;
    [SerializeField] private List<Texture> masks2;
    [SerializeField] private List<Texture> masks1;
    private List<Texture> masks;
    private double period;
    [SerializeField] private List<Material> grasses;
    private int activeGrassCount;


    private const float MAX_ROTATION_SPEED_LEVEL = 10;
    private float rotationSpeed;
    [SerializeField] private float rotationSpeedLevel;
    [SerializeField] private float rotationSpeedIncrement;
    [SerializeField] private float baseRotationSpeed;

    private bool change;

    private float maxRotationSpeed;






    private double timeAfterLastReload;
    private int curIndex;
    // Start is called before the first frame update
    void Awake()
    {
        change = true;
        activeGrassCount = 0;
        timeAfterLastReload = 0;
        period = 0.001f;     
        curIndex = 180;
        rotationSpeed = baseRotationSpeed + rotationSpeedLevel * rotationSpeedIncrement;
        maxRotationSpeed = baseRotationSpeed + MAX_ROTATION_SPEED_LEVEL * rotationSpeedIncrement;
    }

    void Start()
    {
        
        SetBladeLengthLevel(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        curIndex -= (int) rotationSpeedLevel;
        curIndex = ((curIndex % masks.Count) + masks.Count) % masks.Count;
        Debug.Log("1 " + curIndex);
        for (int i = 0; i < activeGrassCount; i++)
        {
            grasses[i].SetTexture("_GrassMap", masks[curIndex]);
        }
        if (change)
        {
            for (int i = activeGrassCount; i < grasses.Count; i++)
            {
                Debug.Log(activeGrassCount);
                grasses[i].SetTexture("_GrassMap", null);
                change = false;
            }
        }
    }



    public void SetRotationSpeed(int level)
    {
        rotationSpeed = baseRotationSpeed + level * rotationSpeedIncrement;
        rotationSpeedLevel = level;
    }

    public void SetBladeLengthLevel(int level)
    {
        switch (level)
        {
            case 1:
                masks = masks1;
                break;
            case 2:
                masks = masks2;
                break;
            case 3:
                masks = masks3;
                break;
            case 4:
                masks = masks4;
                break;
            case 5:
                masks = masks5;
                break;
            default:
                masks = masks1;
                break;
        }
    }

    public void SetActiveGrassCount(int count)
    {
        activeGrassCount = count;
        change = true;
    }
}
