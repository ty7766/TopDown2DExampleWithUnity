using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isAction;

    //대화
    public TalkManager talkManager;
    public int talkIndex;

    //초상화
    public Image portraitImg;

    //퀘스트
    public QuestManager questManager;

    void Start()
    {
        talkPanel.SetActive(false);
        isAction = false;
        talkIndex = 0;
    }

    //대화 출력
    public void Action(GameObject sObj)
    {
        scanObject = sObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNPC)
    {
        //대화 초기화
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //대화 끝났을 때
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questManager.CheckQuest();  //퀘스트 절차 건너뜀 방지
            return;
        }

        //
        //NPC와 사물 구분
        if (isNPC)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;    //그다음 대화로 이동
    }
}
