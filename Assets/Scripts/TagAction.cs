using UnityEngine;

public class ElectricSwitch : MonoBehaviour
{
    [SerializeField] RaycastClick raycastClick;
    [SerializeField] string _tagName;
    [SerializeField] Animator _animator;
    [SerializeField] Animator _flapDoorAnimator;
    AnimatorStateInfo _stateInfoflapDoor;
    AnimatorStateInfo _stateInfoAnimator;
    public bool _enabledElectrick;
    [SerializeField] bool _enableflapDoor;
    [SerializeField] ManualDoorOpening manualDoorOpening;

    private void Start()
    {
        _enableflapDoor = false;
        _enabledElectrick = true;
        raycastClick = GameObject.FindObjectOfType<RaycastClick>();

    }
    private void Update()
    {
        _tagName = raycastClick.nameTag;
        ClickSwitchElectric();
        FlapDoor();
        AnimInfo();
    }

    void AnimInfo()
    {
        _stateInfoflapDoor = _flapDoorAnimator.GetCurrentAnimatorStateInfo(0);
        _stateInfoAnimator = _animator.GetCurrentAnimatorStateInfo(0);
    }

    public void ClickSwitchElectric()
    {
        if(_tagName == "TheSwitch")
        {
            if (_stateInfoAnimator.normalizedTime >= 1.0 && Input.GetKeyUp(KeyCode.Mouse0) && manualDoorOpening._switchLeft == false && manualDoorOpening._switchRight == false)
            {
                _enabledElectrick = !_enabledElectrick;
                if (_enabledElectrick == true)
                {
                    _animator.Play("TheSwitchElectricON");
                }
                else if (_enabledElectrick == false)
                {
                    _animator.Play("TheSwitchElectric");
                }
            }
            else if(Input.GetKeyUp(KeyCode.Mouse0) && manualDoorOpening._switchLeft == true ||
                 Input.GetKeyUp(KeyCode.Mouse0) && manualDoorOpening._switchRight == true)
            {
                Debug.Log("Закройте двери гермо-шлюза");
            }

        }   
    }
    public void FlapDoor()
    {
        if (_tagName == "FlapDoor")
        {
            if (_stateInfoflapDoor.normalizedTime >= 1.0 && Input.GetKeyUp(KeyCode.Mouse0))
            {
                _enableflapDoor = !_enableflapDoor;
                if (_enableflapDoor == true)
                {
                    _flapDoorAnimator.Play("FlapDoorOpen");
                }
                else if (_enableflapDoor == false)
                {
                    _flapDoorAnimator.Play("FlapDoorClose");
                }
            }

        }
    }

}
