using System;

public class SkeletonStunState : SkeletonState
{
    public SkeletonStunState(FSM fsm, Skeleton character, string animBoolName) : base(fsm, character, animBoolName)
    {
    }

    public override void Enter(IState lastState)
    {
        base.Enter(lastState);

        StateTimer = Character.stunTime;

        Character.FlashFX.InvokeRepeating(nameof(FlashFX.RedBlink), 0, 0.1f);

        if (Character.damageFrom)
        {
            var isRight = Character.damageFrom.transform.position.x > Character.transform.position.x;
            var isLeft = Character.damageFrom.transform.position.x < Character.transform.position.x;
            var faceDir = isRight ? 1 : isLeft ? -1 : 0;
            SetVelocity(faceDir * -1 * Character.knockbackXSpeed, Character.knockbackYSpeed, false);
            if (faceDir != 0 && faceDir != Flip.facingDir) Flip.Flip();
        }
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
            Fsm.SwitchState(Character.ChaseState);
    }

    public override void Exit(IState newState)
    {
        base.Exit(newState);

        Character.FlashFX.Reset();
    }
}
