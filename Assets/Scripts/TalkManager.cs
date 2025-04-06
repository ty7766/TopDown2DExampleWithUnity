using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //Dictionary�� ID, Text ����
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    //Sprite ��������
    public Sprite[] PortraitArr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //2���ʿ� ���� ��ȭ ����
        talkData.Add(1000, new string[] {"�ȳ�?:0", "�� ���� ó�� �Ա���?:1"});
        talkData.Add(100, new string[] { "����� �������ڴ�." });
        talkData.Add(200, new string[] { "������ ����ߴ� ������ �ִ� å���̴�." });

        //�̹��� �ҷ�����
        portraitData.Add(1000 + 0, PortraitArr[0]);
        portraitData.Add(1000 + 1, PortraitArr[1]);
        portraitData.Add(1000 + 2, PortraitArr[2]);
        portraitData.Add(1000 + 3, PortraitArr[3]);
    }

    //��ȭ ��������
    public string GetTalk(int id, int talkIndex)
    {
        //������Ʈ ��ȣ�ۿ뿡 ����ִ� ��� �ؽ�Ʈ ��ȭ�� ������
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
