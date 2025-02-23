using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    public float _rayDistans = 2f;
    [SerializeField] Ray _raycast;
    public RaycastHit _raycastHit;
    [SerializeField] AirlockDoorControl _airlockDoorControl;
    [SerializeField] SoundsController _soundsController;
    [SerializeField] ManualDoorOpening manualDoorOpening;
   
    public string nameTag;

    void Update()
    {
        Ray();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Click();
        }

    }
    void Ray()
    {
        _raycast = new Ray(transform.position, transform.forward * _rayDistans);

        if (Physics.Raycast(_raycast, out _raycastHit))
        {
            nameTag = _raycastHit.collider.tag.ToString();
            Debug.DrawRay(transform.position, transform.forward * _rayDistans, Color.green);
        }
        else
        {
            nameTag = "Empty";
            Debug.DrawRay(transform.position, transform.forward * _rayDistans, Color.red);
        }

    }

    private void Click()
    {
        if (nameTag == "ButtonLeftAirlock")
        {
            _airlockDoorControl.ButtonLeftDoor();
        }
        if (nameTag == "Valve(Left)")
        {
            manualDoorOpening.VavleLeft();
        }
        if (nameTag == "Valve(Right)")
        {
            manualDoorOpening.VavleRight();
        }
        if (nameTag == "ButtonRightAirlock")
        {
            _airlockDoorControl.ButtonRightDoor();
        }
        if (nameTag == "ButtonDisinfection")
        {
            _airlockDoorControl.ButtonDisinfection();
        }
        if (nameTag == "ButtonEmergency")
        {
            _airlockDoorControl.ButtonEmergency();
        }
        if (nameTag == "ButtonLeftAirlock" ||
            nameTag == "ButtonDisinfection" ||
            nameTag == "ButtonRightAirlock" &&
            _airlockDoorControl.stateInfoRight.normalizedTime < 1.0f)
        {
            if (_airlockDoorControl._leftDoorClose == true &&
                _airlockDoorControl._rightDoorClose == false)
            {
                _soundsController.ZummerStop();
            }
        }
        if (nameTag == "ButtonRightAirlock" ||
            nameTag == "ButtonDisinfection" ||
            nameTag == "ButtonLeftAirlock" &&
            _airlockDoorControl.stateInfoLeft.normalizedTime < 1.0f)
        {
            if (_airlockDoorControl._leftDoorClose == false &&
                _airlockDoorControl._rightDoorClose == true)
            {
                _soundsController.ZummerStop();
            }
        }
        if (nameTag == "ButtonRightAirlock" &&
            _airlockDoorControl.stateInfoLeft.normalizedTime < 1.0f ||
            nameTag == "ButtonRightAirlock" &&
            _airlockDoorControl.stateInfoRight.normalizedTime < 1.0f ||
            nameTag == "ButtonDisinfection" &&
            _airlockDoorControl.stateInfoLeft.normalizedTime < 1.0f ||
            nameTag == "ButtonDisinfection" &&
            _airlockDoorControl.stateInfoRight.normalizedTime < 1.0f ||
            nameTag == "ButtonLeftAirlock" &&
            _airlockDoorControl.stateInfoRight.normalizedTime < 1.0f ||
            nameTag == "ButtonLeftAirlock" &&
            _airlockDoorControl.stateInfoLeft.normalizedTime < 1.0f ||
            nameTag == "ButtonLeftAirlock" && _airlockDoorControl._emergency == true ||
            nameTag == "ButtonRightAirlock" && _airlockDoorControl._emergency == true ||
            nameTag == "ButtonDisinfection" && _airlockDoorControl._emergency == true)
        {

            _soundsController.ZummerStop();

        }
    }

}

