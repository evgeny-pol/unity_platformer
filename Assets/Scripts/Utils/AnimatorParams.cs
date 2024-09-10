using UnityEngine;

public static class AnimatorParams
{
    public static int IsOnGround = Animator.StringToHash(nameof(IsOnGround));
    public static int IsRunning = Animator.StringToHash(nameof(IsRunning));
    public static int IsDead = Animator.StringToHash(nameof(IsDead));
    public static int IsHurt = Animator.StringToHash(nameof(IsHurt));
    public static int IsCollected = Animator.StringToHash(nameof(IsCollected));
    public static int VerticalVelocity = Animator.StringToHash(nameof(VerticalVelocity));
}
