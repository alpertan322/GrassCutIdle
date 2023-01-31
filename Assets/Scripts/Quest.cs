using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    private Animator _animator;
    private float timeLeftForQuest;
    [SerializeField] private int waitDuration;
    [SerializeField] private List<int> questItems;
    [SerializeField] private List<int> questCounts;
    [SerializeField] private List<int> questDurations;
    private int curQuestIndex;
    private bool change;
    private int totalCollected;
    private int amountToCollect;
    private bool questIsActive;
    [SerializeField] private GameObject animator;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text collectedText;
    private float timeWaited; 
    
    private void Awake()
    {
        timeWaited = 0;
        curQuestIndex = 0;
        change = false;
        timeLeftForQuest = 0;
        questIsActive = false;
        _animator = animator.GetComponent<Animator>();
    }

    private void Update()
    {
        if (questIsActive)
        {
            if (totalCollected >= questCounts[curQuestIndex])
            {
                QuestSuccessful();
            }
            if (timeLeftForQuest <= 0)
            {
                QuestFailed();
            }
            if (change)
            {
                ContinueQuest();
            }
            timeText.text = (timeLeftForQuest / 60).ToString("00") + ":" + (timeLeftForQuest % 60).ToString("00");
            timeLeftForQuest -= Time.deltaTime;
        }
        else
        {
            if (timeWaited > waitDuration)
            {
                OpenQuest(questItems[curQuestIndex], questCounts[curQuestIndex]);
                timeLeftForQuest = questDurations[curQuestIndex];
                totalCollected = -1;
                questIsActive = true;
                change = true;
                timeWaited = 0;
            }
            timeWaited += Time.deltaTime;
        }
    }

    private void OpenQuest(int itemType, int count)
    {
        // TODO Alp itemType = carrot  count= kaç tane lazm
        amountToCollect = count;
        collectedText.text = "        0 / " + amountToCollect;
        _animator.SetTrigger("isQuest");
    }

    private void QuestSuccessful()
    {
        questIsActive = false;
        curQuestIndex += 1;
        curQuestIndex = curQuestIndex % questCounts.Count;
    }

    private void QuestFailed()
    {
        questIsActive = false;
        curQuestIndex += 1;
        curQuestIndex = curQuestIndex % questCounts.Count;
    }

    private void ContinueQuest()
    {
        float randomX = UnityEngine.Random.Range(2, 7);
        float randomZ = UnityEngine.Random.Range(2, 7);
        transform.position = new Vector3(randomX, transform.position.y, randomZ);
        totalCollected += 1;
        collectedText.text = "        " + totalCollected + " / " + amountToCollect;
        change = false;
    }

    public void Change()
    {
        change = true;
    }
}
