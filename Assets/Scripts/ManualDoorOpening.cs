using UnityEngine;

public class ManualDoorOpening : MonoBehaviour
{
    [SerializeField] AirlockDoorControl airlockDoorControl;
    [SerializeField] ElectricityController _electrocontroller;
    [SerializeField] Animator animatorLeft;
    [SerializeField] Animator animatorRight;
    [SerializeField] AnimatorStateInfo stateInfoLeft;
    [SerializeField] AnimatorStateInfo stateInfoRight;
    public bool _switchLeft = false;
    public bool _switchRight = false;

    private void Start()
    {
        _switchLeft = false;
        _switchRight = false;
    }

    private void Update()
    {
        stateInfoLeft = animatorLeft.GetCurrentAnimatorStateInfo(0);
        stateInfoRight = animatorRight.GetCurrentAnimatorStateInfo(0);

        
    }
    public void VavleLeft()
    {
        if(_electrocontroller.moveSpeed == 0f)
        {
            _switchLeft = !_switchLeft;
            if (_switchLeft == true)
            {
                animatorLeft.Play("VavleLeftOpen");
                airlockDoorControl._leftDoorClose = false;
                Debug.Log("Левая двери открыта");
                
            }
            else if (_switchLeft == false)
            {
                animatorLeft.Play("VavleLeftClose");
                airlockDoorControl._leftDoorClose = true;
                Debug.Log("Левая двери закрыта");
            }
        } 
    }
    public void VavleRight()
    {
        if (_electrocontroller.moveSpeed == 0f)
        {
            _switchRight = !_switchRight;
            if (_switchRight == true)
            {
                animatorRight.Play("VavleLeRightOpen");
                airlockDoorControl._rightDoorClose = false;
                Debug.Log("Правая двери открыта");
            }
            else if (_switchRight == false)
            {
                animatorRight.Play("VavleLeRightClose");
                airlockDoorControl._rightDoorClose = true;
                Debug.Log("Правая двери закрыта");
            }
        }
    }

    
}
