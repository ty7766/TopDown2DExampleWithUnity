using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject scanObject;
    public Animator talkPanel;
    public GameObject player;
    public bool isAction;

    //��ȭ
    public TalkManager talkManager;
    public TypeEffect talkString;
    public int talkIndex;

    //�ʻ�ȭ
    public Image portraitImg;
    public Animator talkPortrait;
    public Sprite prevPortrait; //���� �ʻ�ȭ

    //����Ʈ
    public QuestManager questManager;
    public Text questTalk;

    //����޴�
    public GameObject MenuSet;

    //�̱���
    public static GameManager instance = null;

    void Awake()
    {
        //�ν��Ͻ��� ������ ����
        if (instance == null)
        {
            instance = this;
            //�ٸ� ������ �Ѿ�� ���� ���ӿ�����Ʈ�� �������� �ʰ� ����
            DontDestroyOnLoad(this.gameObject);
        }

        //�ν��Ͻ��� ������ �����Ǿ������� ���� ������ �ν��Ͻ� ����
        else if(instance != null)
            Destroy(this.gameObject);
    }

    void Start()
    {
        GameLoad();
        questTalk.text = questManager.CheckQuest();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
        }
    }

    public void SubMenuActive()
    {
        if (MenuSet.activeSelf)
            MenuSet.SetActive(false);
        else
            MenuSet.SetActive(true);
    }
    //��ȭ ���
    public void Action(GameObject sObj)
    {
        scanObject = sObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC)
    {
        int questTalkIndex;
        string talkData = "";

        //��ȭ �ʱ�ȭ

        /*�ΰ�����
        ��ȭ�� ������(Talk()ȣ��)�� ��, ����ڰ� Enter�� ������ ���� ��ȭ�� ��ŵ�ϸ�
        ���� �ؽ�Ʈ�� ������ �ִ� Talk()�Լ��� ȣ���
        ���� ��ȭ�� ��ŵ�ϸ�(��ȭ�� Enter�� ������) ���� �ؽ�Ʈ�� �ѹ��� ��� �����ֱ�  
        */

        //��ȭ�� �������� ���¿��� ����ڰ� Enter�� �� �� �� ������
        if (talkString.isAnimation)
        {
            talkString.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        //��ȭ ������ ��
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questTalk.text = questManager.CheckQuest(id);  //����Ʈ ���� �ǳʶ� ����
            return;
        }

        //
        //NPC�� �繰 ����
        if (isNPC)
        {
            talkString.SetMsg(talkData.Split(':')[0]);

            //�ʻ�ȭ ���̱�
            //�ʻ�ȭ�� ������ �ٸ��� �ִϸ��̼� ����
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            if (prevPortrait != portraitImg.sprite)
            {
                talkPortrait.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }  
        }
        else
        {
            talkString.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;    //�״��� ��ȭ�� �̵�
    }

    //���� ����
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        MenuSet.SetActive(false);
    }

    //���� �ҷ�����
    public void GameLoad()
    {
        //���� �� �� �� ���� ���̺����� �ʾ����� �ε� X
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QeustActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObj();
    }

    //���� ����
    public void GameExit()
    {
        Application.Quit();
    }
}
