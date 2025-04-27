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

    public GameManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        //대화창일 때 캐릭터 움직임 방지
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //Check Button Event
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        //Check H,V move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

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
            manager.Action(scanObject);
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
}
