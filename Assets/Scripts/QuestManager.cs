using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public int questId;     //���� �������� ����Ʈ ���̵�
    public int questActionIndex;    //����Ʈ ��ȭ ���� ����(����Ʈ ������ �ǳʶ��� �ʵ��� ����)
    public GameObject[] questObj;

    Dictionary<int, QuestData> questList;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("Chapter1. ���� ������ ��ȭ�ϱ�", new int[]
            {1000, 2000}));

        questList.Add(20, new QuestData("Chapter2. �絵�� ���� ã���ֱ�", new int[]
            {5000, 2000}));

        questList.Add(30, new QuestData("����Ʈ �� Ŭ����!", new int[]
            {0}));
    }
    
    //NPC�� �´� ����Ʈ ��ȣ�� ��ȯ
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //����Ʈ ���� �ǳʶ� ������
    public string CheckQuest(int id)
    {
        //���� �´� ��ȭ ������ �����ϸ� ���� ��ȭ�� ��ȯ
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;
        //����Ʈ ������Ʈ ����
        ControlObj();

        //�� ����Ʈ ��ȭ �з��� ��� ������ ���� é�� ����Ʈ�� ��ȯ
        if (questActionIndex == questList[questId].npcId.Length)
            NextQeust();

        //����Ʈ �̸� ��ȯ
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //����Ʈ �̸� ��ȯ
        return questList[questId].questName;
    }

    //���� ����Ʈ�� �Ѿ��
    void NextQeust()
    {
        questId += 10;
        questActionIndex = 0;
    }

    //����Ʈ ������Ʈ ����
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
