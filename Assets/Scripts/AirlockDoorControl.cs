using UnityEngine;

public class AirlockDoorControl : MonoBehaviour
{
    [SerializeField] Animator _animatorLeftController;
    [SerializeField] AnimatorStateInfo stateInfoLeft;
    [SerializeField] Animator _animatorRighttController;
    [SerializeField] AnimatorStateInfo stateInfoRight;
    [SerializeField] ParticleSystem[] _disinfectionParticle;
    [SerializeField] SpiderAnimatorSwitch spiderAnimatorSwitch;
    [SerializeField] bool _leftDoorClose;
    [SerializeField] bool _rightDoorClose;
    [SerializeField] bool _disinfection;
    [SerializeField] string _doorLeftAnimatorClipClose = "LeftGetwayClose";
    [SerializeField] string _doorLeftAnimatorClipOpen = "LeftGetway";
    [SerializeField] string _doorRightAnimatorClipClose = "RightGetwayClose";
    [SerializeField] string _doorRightAnimatorClipOpen = "RightGetway";
    void Start()
    {
        _leftDoorClose = true;
        _rightDoorClose = true;
        _disinfection = true;

        for (int i = 0; i < _disinfectionParticle.Length; i++)
        {
            _disinfectionParticle[i].Stop();
            if (spiderAnimatorSwitch != null)
            {
                spiderAnimatorSwitch.Idle();
            }
        }
    }

    private void Update()
    {
        stateInfoLeft = _animatorLeftController.GetCurrentAnimatorStateInfo(0);
        stateInfoRight = _animatorRighttController.GetCurrentAnimatorStateInfo(0);

        if (stateInfoLeft.normalizedTime >= 1.0f && stateInfoRight.normalizedTime >= 1.0f && _rightDoorClose == true && Input.GetKeyUp(KeyCode.Alpha1))
        {
            AnimationLeftDoor();
        }
        if (stateInfoRight.normalizedTime >= 1.0f && stateInfoLeft.normalizedTime >= 1.0f && _leftDoorClose == true && Input.GetKeyUp(KeyCode.Alpha3))
        {
            AnimationRightDoor();
        }
        Disinfection();
    }
    void FixedUpdate()
    {
        if(_leftDoorClose == false && _rightDoorClose == false)
        {
            _disinfection = false;
        }
        else if(_leftDoorClose == true && _rightDoorClose == true)
        {
            _disinfection = true;
        } 
    }
    
    public void AnimationLeftDoor()
    {
        _leftDoorClose = !_leftDoorClose;
        if ( _animatorLeftController != null && _leftDoorClose == true)
        {
            _animatorLeftController.Play(_doorLeftAnimatorClipClose);
        }
        else if(_animatorLeftController != null && _leftDoorClose == false)
        {
            _animatorLeftController.Play(_doorLeftAnimatorClipOpen);
        }
    }
    public void AnimationRightDoor()
    {
        _rightDoorClose = !_rightDoorClose;
        if (_animatorRighttController != null && _rightDoorClose == true)
        {
            _animatorRighttController.Play(_doorRightAnimatorClipClose);
        }
        else if (_animatorRighttController != null && _rightDoorClose == false)
        {
            _animatorRighttController.Play(_doorRightAnimatorClipOpen);
        }
    }

    public void Disinfection()
    {
        if (_leftDoorClose == true && _rightDoorClose == true && _disinfection == true)
        {
            if(stateInfoLeft.normalizedTime >= 1.0f && stateInfoRight.normalizedTime >= 1.0f && Input.GetKeyUp(KeyCode.Alpha2))
            {
                for (int i = 0; i < _disinfectionParticle.Length; i++)
                {
                    if (!_disinfectionParticle[i].isPlaying)
                    {
                        _disinfectionParticle[i].Play();
                        if (spiderAnimatorSwitch != null)
                        {
                            spiderAnimatorSwitch.Dead();
                        }
                    }
                    else if (_disinfectionParticle[i].isPlaying)
                    {
                        _disinfectionParticle[i].Stop();
                        if (spiderAnimatorSwitch != null)
                        {
                            spiderAnimatorSwitch.Idle();
                        }
                    }
                }
            } 
        }
        else
        {
            for (int i = 0; i < _disinfectionParticle.Length; i++)
            {
                _disinfectionParticle[i].Stop();
                if (spiderAnimatorSwitch != null)
                {
                    spiderAnimatorSwitch.Idle();
                }
            }
        }
    }

    public void ButtonOne()
    {
        if (stateInfoLeft.normalizedTime >= 1.0f && stateInfoRight.normalizedTime >= 1.0f && _rightDoorClose == true)
        {
            AnimationLeftDoor();
        }
    }
    public void ButtonTwo()
    {
        if (stateInfoRight.normalizedTime >= 1.0f && stateInfoLeft.normalizedTime >= 1.0f && _leftDoorClose == true)
        {
            AnimationRightDoor();
        }
    }
    public void ButtonThree()
    {
        if (_leftDoorClose == true && _rightDoorClose == true && _disinfection == true)
        {
            if (stateInfoLeft.normalizedTime >= 1.0f && stateInfoRight.normalizedTime >= 1.0f)
            {
                for (int i = 0; i < _disinfectionParticle.Length; i++)
                {
                    if (!_disinfectionParticle[i].isPlaying)
                    {
                        _disinfectionParticle[i].Play();
                        if (spiderAnimatorSwitch != null)
                        {
                            spiderAnimatorSwitch.Dead();
                        }
                    }
                    else if (_disinfectionParticle[i].isPlaying)
                    {
                        _disinfectionParticle[i].Stop();
                        if (spiderAnimatorSwitch != null)
                        {
                            spiderAnimatorSwitch.Idle();
                        }
                    }
                }
            }
        }
    }
}
