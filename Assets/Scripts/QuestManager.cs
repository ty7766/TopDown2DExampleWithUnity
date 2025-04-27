using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public int questId;     //지금 실행중인 퀘스트 아이디
    public int questActionIndex;    //퀘스트 대화 순서 설정(퀘스트 절차를 건너뛰지 않도록 설정)
    public GameObject[] questObj;

    Dictionary<int, QuestData> questList;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("Chapter1. 마을 사람들과 대화하기", new int[]
            {1000, 2000}));

        questList.Add(20, new QuestData("Chapter2. 루도의 동전 찾아주기", new int[]
            {5000, 2000}));

        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[]
            {0}));
    }
    
    //NPC에 맞는 퀘스트 번호를 반환
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //퀘스트 절차 건너뜀 방지용
    public string CheckQuest(int id)
    {
        //현재 맞는 대화 순서를 실행하면 다음 대화로 전환
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;
        //퀘스트 오브젝트 관리
        ControlObj();

        //한 퀘스트 대화 분량을 모두 끝내면 다음 챕터 퀘스트로 전환
        if (questActionIndex == questList[questId].npcId.Length)
            NextQeust();

        //퀘스트 이름 반환
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //퀘스트 이름 반환
        return questList[questId].questName;
    }

    //다음 퀘스트로 넘어가기
    void NextQeust()
    {
        questId += 10;
        questActionIndex = 0;
    }

    //퀘스트 오브젝트 관리
    public void ControlObj()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObj[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObj[0].SetActive(true);
                if (questActionIndex == 1)
                    questObj[0].SetActive(false);
                break;
        }
    }
}
