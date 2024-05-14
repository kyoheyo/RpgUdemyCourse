using System.Threading.Tasks;
using UnityEngine;

public class PlayerState : CharacterState<Player>, IState
{
    private int airDashCounter;

    protected static float dashCooldownTimer;
    protected static bool isBusy;
    protected float dashDir;
    protected InputController Input { get; private set; }

    public PlayerState(FSM fsm, Player character, string animBoolName) : base(fsm, character, animBoolName)
    {
        Input = character.Input;
    }

    public virtual void Enter(IState lastState)
    {
        BaseEnter();

        SetDashDir();
    }

    public virtual void Update()
    {
        BaseUpdate();

        dashCooldownTimer -= Time.deltaTime;

        Anim.SetFloat("VelocityY", Rb.velocity.y);

        if (ColDetect.isGrounded)
        {
            airDashCounter = 0;
        }

        if (airDashCounter < Character.airDashCount && dashCooldownTimer <= 0 && !ColDetect.isWallDetected && Input.isDashDown)
        {
            if (!ColDetect.isGrounded)
            {
                airDashCounter++;
            }
            SetDashDir();
            Fsm.SwitchState(Character.DashState);
            dashCooldownTimer = Character.dashCooldown;
        }
    }

    public virtual void Exit(IState newState)
    {
        BaseExit();
    }

    private void SetDashDir()
    {
        dashDir = Input.xAxis == 0 ? Flip.facingDir : Input.xAxis;
    }

    public async void BusyFor(float seconds)
    {
        isBusy = true;
        await Task.Delay((int)(seconds * 1000));
        isBusy = false;
    }
}
