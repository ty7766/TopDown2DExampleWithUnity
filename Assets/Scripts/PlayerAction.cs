using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;

    Vector3 direction;
    GameObject scanObject;
    Rigidbody2D rigid;
    Animator anim;

    public StateMachine stateMachine;

    int up_V, down_V, left_V, right_V;
    GameManager manager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = new StateMachine();
    }

    void Start()
    {
        manager = GameManager.instance;
        stateMachine.Initialize(new IdleState(this));
    }

    void Update()
    {
        if (!manager.isAction)
        {
            stateMachine.Update();
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                manager.Action(scanObject);
            }
        }
    }

    public void Move(float h, float v)
    {
        Vector2 moveVec = new Vector2(h, v).normalized;
        rigid.linearVelocity = moveVec * Speed;
        UpdateDirection(h, v);
    }

    public void SetAnimation(float h, float v)
    {
        bool isChanged = anim.GetInteger("hAxisRaw") != (int)h || anim.GetInteger("vAxisRaw") != (int)v;
        anim.SetBool("isChange", isChanged);
        anim.SetInteger("hAxisRaw", (int)h);
        anim.SetInteger("vAxisRaw", (int)v);
    }

    public void UpdateDirection(float h, float v)
    {
        if (v == 1)
            direction = Vector3.up;
        else if (v == -1)
            direction = Vector3.down;
        else if (h == -1)
            direction = Vector3.left;
        else if (h == 1)
            direction = Vector3.right;

        Debug.DrawRay(rigid.position, direction * 0.7f, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, direction, 0.7f, LayerMask.GetMask("Object"));
        scanObject = rayHit.collider != null ? rayHit.collider.gameObject : null;
    }

    public void Interact()
    {
        if (scanObject != null)
        {
            manager.Action(scanObject);
        }
    }

    public bool HasScanObject()
    {
        return scanObject != null;
    }

    public float GetHorizontalInput()
    {
        float keyboard = Input.GetAxisRaw("Horizontal");
        float mobile = right_V + left_V;
        return Mathf.Clamp(keyboard + mobile, -1f, 1f);
    }

    public float GetVerticalInput()
    {
        float keyboard = Input.GetAxisRaw("Vertical");
        float mobile = up_V + down_V;
        return Mathf.Clamp(keyboard + mobile, -1f, 1f);
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U": up_V = 1; break;
            case "D": down_V = -1; break;
            case "L": left_V = -1; break;
            case "R": right_V = 1; break;
            case "A":
                if (manager.isAction)
                    manager.Action(scanObject);
                else
                    Interact();
                break;
            case "C": manager.SubMenuActive(); break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U": up_V = 0; break;
            case "D": down_V = 0; break;
            case "L": left_V = 0; break;
            case "R": right_V = 0; break;
        }
    }
}
