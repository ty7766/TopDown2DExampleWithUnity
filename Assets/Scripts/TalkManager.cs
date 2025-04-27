using System.Collections.Generic;
using UnityEngine;

//04.24
//퀘스트 진행 중 기본 대사를 출력할 때도 반복해서 Ch1, Ch2의 문구가 반복해서 출력됨

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
        //NPCA
        talkData.Add(1000, new string[] {"안녕?:0",
                                         "이 곳에 처음 왔구나?:1",
                                         "한 번 둘러보도록 해.:0"});

        //Obj
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        //NPCB
        talkData.Add(2000, new string[] { "누구세요? 처음보는 얼굴인데...:0", "외지에서 왔어?:1" });

        //10 Quest
        //10 = 퀘스트 ID
        talkData.Add(10 + 1000, new string[] { "어서 와.:0", 
                                               "이 마을에 놀라운 전설이 있다는데:1",
                                               "오른쪽 호수 쪽에 루도가 알려줄꺼야.:0"});

        talkData.Add(11 + 2000, new string[] { "이 호수의 전설을 들으러 온거야?:0",
                                               "그럼 일 좀 하나 도와줘야 겠는걸?:1",
                                               "내 집 근처에 떨어진 동전 좀 주워줬으면 해.:1"});

        talkData.Add(20 + 1000, new string[] { "루도의 동전?:0",
                                               "돈을 흘리고 다니면 못쓰지!:3",
                                               "나중에 루도에게 한마디 해야겠어.:3"});

        talkData.Add(20 + 2000, new string[] { "찾으면 꼭 좀 가져다 줘.:1" });

        talkData.Add(20 + 5000, new string[] { "동전을 찾았다." });

        talkData.Add(21 + 2000, new string[] { "동전을 찾았어?:1",
                                               "찾아줘서 고마워.:2" });



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
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //퀘스트 이외 대사 유지(퀘스트 대상 제외한 나머지 Obj는 기본 대사 출력
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                //해당 퀘스트 진행 중, 남은 대사가 없을 때
                // -> 퀘스트 처음 대사 가져오기
                return GetTalk(id - id % 10, talkIndex);
            }

        }
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
