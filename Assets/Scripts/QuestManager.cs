using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public int questId;     //���� �������� ����Ʈ ���̵�
    public int questActionIndex;    //����Ʈ ��ȭ ���� ����(����Ʈ ������ �ǳʶ��� �ʵ��� ����)
    Dictionary<int, QuestData> questList;


    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("ù ���� �湮", new int[]
            {1000, 2000}));
    }
    
    //NPC�� �´� ����Ʈ ��ȣ�� ��ȯ
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //�� ����Ʈ ��ȭ�� ������ ���� ����Ʈ ��ȭ�� ����
    //����Ʈ ���� �ǳʶ� ������
    public void CheckQuest()
    {
        questActionIndex++;
    }
}
