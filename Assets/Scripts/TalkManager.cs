using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //Dictionary에 ID, Text 저장
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    //Sprite 가져오기
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
        //2차례에 걸쳐 대화 진행
        talkData.Add(1000, new string[] {"안녕?:0", "이 곳에 처음 왔구나?:1"});
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        talkData.Add(2000, new string[] { "누구세요? 처음보는 얼굴인데...:0", "외지에서 오셨나요?:1" });

        //이미지 불러오기
        portraitData.Add(1000 + 0, PortraitArr[0]);
        portraitData.Add(1000 + 1, PortraitArr[1]);
        portraitData.Add(1000 + 2, PortraitArr[2]);
        portraitData.Add(1000 + 3, PortraitArr[3]);

        portraitData.Add(2000 + 0, PortraitArr[4]);
        portraitData.Add(2000 + 1, PortraitArr[5]);
        portraitData.Add(2000 + 2, PortraitArr[6]);
        portraitData.Add(2000 + 3, PortraitArr[7]);
    }

    //대화 가져오기
    public string GetTalk(int id, int talkIndex)
    {
        //오브젝트 상호작용에 들어있는 모든 텍스트 대화를 했으면
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    //초상화가져오기
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
