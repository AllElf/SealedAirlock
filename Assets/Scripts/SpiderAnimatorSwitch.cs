using UnityEngine;

public class SpiderAnimatorSwitch : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string _clipDead = "Death";
    [SerializeField] string _clipIdle = "Idle";

    public void Dead()
    {
        _animator.Play(_clipDead);
    }
    public void Idle()
    {
        _animator.Play(_clipIdle);
    }
}
