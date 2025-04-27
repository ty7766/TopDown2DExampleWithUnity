using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public int questId;     //지금 실행중인 퀘스트 아이디
    public int questActionIndex;    //퀘스트 대화 순서 설정(퀘스트 절차를 건너뛰지 않도록 설정)
    Dictionary<int, QuestData> questList;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문", new int[]
            {1000, 2000}));
    }
    
    //NPC에 맞는 퀘스트 번호를 반환
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //한 퀘스트 대화가 끝나면 다음 퀘스트 대화로 진행
    //퀘스트 절차 건너뜀 방지용
    public void CheckQuest()
    {
        questActionIndex++;
    }
}
