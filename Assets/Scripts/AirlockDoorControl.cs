using System.Collections;
using TMPro;
using UnityEngine;

public class AirlockDoorControl : MonoBehaviour
{
    [SerializeField] SoundsController soundsController;
    public Animator _animatorLeftController;
    public AnimatorStateInfo stateInfoLeft;
    public Animator _animatorRighttController;
    public AnimatorStateInfo stateInfoRight;
    public ParticleSystem[] _disinfectionParticle;
    public SpiderAnimatorSwitch spiderAnimatorSwitch;
    [SerializeField] ElectricityController electricityController;
    public bool _emergency;
    public bool _leftDoorClose;
    public bool _rightDoorClose;
    public bool _disinfection;
    public bool _disinfectionParticlePlay;
    [SerializeField] string _doorLeftAnimatorClipClose = "LeftGetwayClose";
    [SerializeField] string _doorLeftAnimatorClipOpen = "LeftGetway";
    [SerializeField] string _doorRightAnimatorClipClose = "RightGetwayClose";
    [SerializeField] string _doorRightAnimatorClipOpen = "RightGetway";
    public TextMeshPro _emergencytText;
    void Start()
    {
        _leftDoorClose = true;
        _rightDoorClose = true;
        _disinfection = true;
        _emergency = false;

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
        TextInfo();
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
        soundsController.OpenAndCloseDoor();
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
        soundsController.OpenAndCloseDoor();
    }


    public void ButtonLeftDoor()
    {
        if (_emergency == false && stateInfoLeft.normalizedTime >= 1.0f && stateInfoRight.normalizedTime >= 1.0f && _rightDoorClose == true && _disinfectionParticlePlay == false && electricityController.moveSpeed !=0)
        {
            AnimationLeftDoor();
        }
    }
    public void ButtonRightDoor()
    {
        if (_emergency == false && stateInfoRight.normalizedTime >= 1.0f && stateInfoLeft.normalizedTime >= 1.0f && _leftDoorClose == true && _disinfectionParticlePlay == false && electricityController.moveSpeed !=0)
        {
            AnimationRightDoor();
        }
    }
    public void ButtonDisinfection()
    {
        if (_emergency == false && _leftDoorClose == true && _rightDoorClose == true && _disinfection == true && electricityController.moveSpeed != 0)
        {
            if (stateInfoLeft.normalizedTime >= 1.0f && stateInfoRight.normalizedTime >= 1.0f)
            {
                StopAllCoroutines();
                StartCoroutine(DisinfectionPlatCoroutine());
            }
        }
    }

    IEnumerator DisinfectionPlatCoroutine()
    {
        if (!_disinfectionParticle[0].isPlaying && !_disinfectionParticle[1].isPlaying && electricityController.moveSpeed !=0)
        {
            _disinfectionParticlePlay = true;
            soundsController.Zummer();
            yield return new WaitForSeconds(1.1f);
            soundsController.Zummer();
            yield return new WaitForSeconds(1.1f);
            _disinfectionParticle[0].Play();
            _disinfectionParticle[1].Play();
            if (spiderAnimatorSwitch != null)
            {
                spiderAnimatorSwitch.Dead();
            }

        }
        else if(_disinfectionParticle[0].isPlaying && _disinfectionParticle[1].isPlaying && electricityController.moveSpeed != 0)
        {
            _disinfectionParticle[0].Stop();
            _disinfectionParticle[1].Stop();
            yield return new WaitForSeconds(2f);
            soundsController.Zummer();
            yield return new WaitForSeconds(1.1f);
            soundsController.Zummer();
            yield return new WaitForSeconds(1.1f);
            _disinfectionParticlePlay = false;
            if (spiderAnimatorSwitch != null)
            {
                spiderAnimatorSwitch.Idle();
            }
        }
    }
    public void ButtonEmergency()
    {
        if(stateInfoRight.normalizedTime >= 1.0f && stateInfoLeft.normalizedTime >= 1.0f && electricityController.moveSpeed !=0)
        {
            _emergency = !_emergency;
            if (_emergency == true && _leftDoorClose == false)
            {
                _disinfectionParticlePlay = false;
                StopAllCoroutines();
                StartCoroutine(AnimFalsLeft());
            }
            else if (_emergency == true && _rightDoorClose == false)
            {
                _disinfectionParticlePlay = false;
                StopAllCoroutines();
                StartCoroutine(AnimFalsRight());
            }
            else if (_emergency == true && _rightDoorClose == true && _leftDoorClose == true)
            {
                _disinfectionParticlePlay = false;
                AnimatorFals();
            }
            else if (_emergency == false)
            {
                AnimatorTrue();
            }
        }
        else
        {
            Debug.Log("Идёт анимация");
        }
    }
    IEnumerator AnimFalsLeft()
    {
        AnimationLeftDoor();
        yield return new WaitForSeconds(4.5f);
        AnimatorFals();
        yield break;
    }
    IEnumerator AnimFalsRight()
    {
        AnimationRightDoor();
        yield return new WaitForSeconds(4.5f);
        AnimatorFals();
        yield break;
    }
    void AnimatorFals()
    {
        _disinfectionParticle[0].GetComponent<ParticleSystem>().Stop();
        _disinfectionParticle[1].GetComponent<ParticleSystem>().Stop();
        _animatorLeftController.enabled = false;
        _animatorRighttController.enabled = false;
        spiderAnimatorSwitch.Dead();
    }
    public void AnimatorTrue()
    {
        _animatorLeftController.enabled = true;
        _animatorRighttController.enabled = true;
        spiderAnimatorSwitch.Idle();
    }
    void TextInfo()
    {
        if (_emergency == true && electricityController.moveSpeed != 0)
        {
            _emergencytText.color = Color.red;
            _emergencytText.text = "Аварийная блокировка гермо-шлюза активна";
        }
        else if (_emergency == false && electricityController.moveSpeed != 0)
        {
            _emergencytText.color = Color.green;
            _emergencytText.text = "Аварийная блокировка гермо-шлюза неактивна";
        }
        else if(electricityController.moveSpeed == 0)
        {
            _emergencytText.color = Color.red;
            _emergencytText.text = "Отсутствует электричество"; 
        }
    }
}
