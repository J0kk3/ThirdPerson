using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);
        //Player is not moving, dont rotate
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        //Player is moving, Animate!
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        //Player is moving, Rotate!
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    void OnTarget()
    {
        //Only enter Targeting state if we succesfully selected a Target
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        //We don't care where the camera is looking vertically
        forward.y = 0f;
        right.y = 0f;
        //Make sure they both have a magnitude of 1
        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;
    }
    void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }
}