using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject scanObject;
    public Animator talkPanel;
    public GameObject player;
    public bool isAction;

    //대화
    public TalkManager talkManager;
    public TypeEffect talkString;
    public int talkIndex;

    //초상화
    public Image portraitImg;
    public Animator talkPortrait;
    public Sprite prevPortrait; //이전 초상화

    //퀘스트
    public QuestManager questManager;
    public Text questTalk;

    //서브메뉴
    public GameObject MenuSet;

    //싱글톤
    public static GameManager instance = null;

    void Awake()
    {
        //인스턴스가 없으면 생성
        if (instance == null)
        {
            instance = this;
            //다른 씬으로 넘어가도 현재 게임오브젝트가 삭제되지 않고 유지
            DontDestroyOnLoad(this.gameObject);
        }

        //인스턴스가 기존에 생성되어있으면 현재 생성된 인스턴스 삭제
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
    //대화 출력
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

        //대화 초기화

        /*부가설명
        대화가 진행중(Talk()호출)일 때, 사용자가 Enter를 눌러서 현재 대화를 스킵하면
        다음 텍스트를 가지고 있는 Talk()함수가 호출됨
        현재 대화를 스킵하면(대화중 Enter를 누르면) 현재 텍스트를 한번에 모두 보여주기  
        */

        //대화가 진행중인 상태에서 사용자가 Enter를 한 번 더 누르면
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

        //대화 끝났을 때
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questTalk.text = questManager.CheckQuest(id);  //퀘스트 절차 건너뜀 방지
            return;
        }

        //
        //NPC와 사물 구분
        if (isNPC)
        {
            talkString.SetMsg(talkData.Split(':')[0]);

            //초상화 보이기
            //초상화가 이전과 다르면 애니메이션 삽입
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
        talkIndex++;    //그다음 대화로 이동
    }

    //게임 저장
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        MenuSet.SetActive(false);
    }

    //게임 불러오기
    public void GameLoad()
    {
        //게임 내 단 한 번도 세이브하지 않았으면 로드 X
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

    //게임 종료
    public void GameExit()
    {
        Application.Quit();
    }
}
