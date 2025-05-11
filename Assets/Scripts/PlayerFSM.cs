using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}

public class IdleState : IState
{
    private PlayerAction player;

    public IdleState(PlayerAction player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.SetAnimation(0, 0);
        Debug.Log("[PlayerFSM] : 상태 진입");
    }

    public void Update()
    {
        float h = player.GetHorizontalInput();
        float v = player.GetVerticalInput();

        if (h != 0 || v != 0)
        {
            player.stateMachine.TransitionTo(new MoveState(player));
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (player.HasScanObject())
                player.Interact();
            Debug.Log("[PlayerFSM] : 상호작용 상태 진행");
        }
    }

    public void Exit() { }
}

public class MoveState : IState
{
    private PlayerAction player;

    public MoveState(PlayerAction player)
    {
        this.player = player;
    }

    public void Enter() { }

    public void Update()
    {
        float h = player.GetHorizontalInput();
        float v = player.GetVerticalInput();

        if (h == 0 && v == 0)
        {
            player.stateMachine.TransitionTo(new IdleState(player));
            return;
        }

        player.SetAnimation(h, v);
        player.Move(h, v);

        if (Input.GetButtonDown("Jump"))
        {
            if (player.HasScanObject())
                player.Interact();
            Debug.Log("[PlayerFSM] : 상호 작용 상태 진행");
        }
    }

    public void Exit()
    {
        player.SetAnimation(0, 0);
        Debug.Log("[PlayerFSM] : 상태 종료");
    }
}