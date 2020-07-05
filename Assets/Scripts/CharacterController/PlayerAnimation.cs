using UnityEngine;

public sealed class PlayerAnimation : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    private static readonly int RotationEnable = Animator.StringToHash("RotationEnable");
    private static readonly int RotationDisable = Animator.StringToHash("RotationDisable");

    private static readonly int FireDisable = Animator.StringToHash("FireDisable");
    private static readonly int FireEnable = Animator.StringToHash("FireEnable");

    private static readonly int SitingEnable = Animator.StringToHash("SitingEnable");
    private static readonly int SitingDisable = Animator.StringToHash("SitingDisable");
    private static readonly int LyingEnable = Animator.StringToHash("LyingEnable");
    private static readonly int LyingDisable = Animator.StringToHash("LyingDisable");

    private static readonly int JumpUpEnable = Animator.StringToHash("JumpUpEnable");
    private static readonly int JumpUpDisable = Animator.StringToHash("JumpUpDisable");
    private static readonly int JumpDownEnable = Animator.StringToHash("JumpDownEnable");
    private static readonly int JumpDownDisable = Animator.StringToHash("JumpDownDisable");

    private static readonly int LyingBool = Animator.StringToHash("LyingBool");

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void SetMove(Vector3 movement)
    {        
        Animator.SetFloat(Vertical, movement.z);
        Animator.SetFloat(Horizontal, movement.x);
    }

    public void OnFireEnable()
    {
        Animator.SetTrigger(FireEnable);
    }

    public void OnFireDisable()
    {
        Animator.SetTrigger(FireDisable);
    }

    public void OnRotationEnable(float angle)
    {
        Animator.SetTrigger(RotationEnable);
    }

    public void OnRotationDisable()
    {
        Animator.SetTrigger(RotationDisable);
    }

    public void SitEnable()
    {
        Animator.SetTrigger(SitingEnable);
    }

    public void SitDisable()
    {
        Animator.SetTrigger(SitingDisable);
    }

    public void OnLieEnable()
    {
        Animator.SetTrigger(LyingEnable);
    }

    public void OnLieDisable()
    {
        Animator.SetTrigger(LyingDisable);
    }

    public void OnJumpUpEnable()
    {
        Animator.SetTrigger(JumpUpEnable);
    }

    public void OnJumpUpDisable()
    {
        Animator.SetTrigger(JumpUpDisable);
    }

    public void OnJumpDownEnable()
    {
        Animator.SetTrigger(JumpDownEnable);
    }

    public void OnJumpDownDisable()
    {
        Animator.SetTrigger(JumpDownDisable);
    }


    public void SetLieBool(bool p_trigger)
    {
        Animator.SetBool(LyingBool, p_trigger);
    }
}
