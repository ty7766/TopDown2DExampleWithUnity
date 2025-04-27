using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;
    public GameObject talkPanel;
    public bool isAction;

    //��ȭ
    public TalkManager talkManager;
    public int talkIndex;

    //�ʻ�ȭ
    public Image portraitImg;

    //����Ʈ
    public QuestManager questManager;

    void Start()
    {
        talkPanel.SetActive(false);
        isAction = false;
        talkIndex = 0;
    }

    //��ȭ ���
    public void Action(GameObject sObj)
    {
        scanObject = sObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNPC)
    {
        //��ȭ �ʱ�ȭ
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //��ȭ ������ ��
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questManager.CheckQuest();  //����Ʈ ���� �ǳʶ� ����
            return;
        }

        //
        //NPC�� �繰 ����
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
        talkIndex++;    //�״��� ��ȭ�� �̵�
    }
}
