using UnityEngine;
public class PlayerAction : MonoBehaviour
{
    public float Speed;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 direVec;

    GameObject scanObject;

    Rigidbody2D rigid;
    Animator anim;

    //기존 Gamemanager 접근
    GameManager manager;

    //모바일 버전
    int up_V;
    int down_V;
    int left_V;
    int right_V;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        //manager = GameObject.Find("manager").GetComponent<GameManager>();
        manager = GameManager.instance;
        Debug.Log("[PlayerAction.cs] GameManager 연결 확인");
    }
    // Update is called once per frame
    void Update()
    {
        //Move Value (PC, Mobile 통합)
        //대화창일 때 캐릭터 움직임 방지
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_V + left_V;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_V + down_V;

        //Check Button Event
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;


        //Check H,V move (Debug)
        if (hDown)
        {
            isHorizonMove = true;
            Debug.Log("[PlayerAction.cs] GameManager : 수평 이동 시작");
        }
        else if (vDown)
        {
            isHorizonMove = false;
            Debug.Log("[PlayerAction.cs] GameManager : 수직 이동 시작");
        }
        else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
            Debug.Log($"[PlayerAction.cs] GameManager : 이동 키 뗌");
        }


        //Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        //Direction
        if (vDown && v == 1)
            direVec = Vector3.up;
        else if (vDown && v == -1)
            direVec = Vector3.down;
        else if (hDown && h == -1)
            direVec = Vector3.left;
        else if (hDown && h == 1)
            direVec = Vector3.right;

        //Scan
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
            Debug.Log($"[PlayerAction.cs] GameManager : 스캔 확인");
        }

        //Mobile Var Init
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
    }

    private void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ?
            new Vector2(h, 0) :
            new Vector2(0, v);
        rigid.linearVelocity = moveVec * Speed;


        //Ray
        //캐릭터와 Ray 붙이기
        Debug.DrawRay(rigid.position, direVec * 0.7f, new Color(0, 1, 0));
        //Ray와 다른 오브젝트 상호작용
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, direVec, 0.7f, LayerMask.GetMask("Object"));

        //상호작용 시
        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_V = 1;
                up_Down = true;
                break;
            case "D":
                down_V = -1;
                down_Down = true;
                break;
            case "L":
                left_V = -1;
                left_Down = true;
                break;
            case "R":
                right_V = 1;
                right_Down = true;
                break;
            case "A":
                if (scanObject != null)
                    manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;
        }
    }
    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_V = 0;
                up_Up = true;
                break;
            case "D":
                down_V = 0;
                down_Up = true;
                break;
            case "L":
                left_V = 0;
                left_Up = true;
                break;
            case "R":
                right_V = 0;
                right_Up = true;
                break;
        }
    }
}
