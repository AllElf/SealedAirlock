using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    [SerializeField] float _rayDistans = 2f;
    [SerializeField]Ray _raycast;
    public RaycastHit _raycastHit;
    [SerializeField] bool _enabled = false;
    [SerializeField] AirlockDoorControl _airlockDoorControl;
    [SerializeField] string nameTag;

    void Update()
    {
        Ray();
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Click();
        }

    }
    void Ray()
    {
        _raycast = new Ray(transform.position, transform.forward * _rayDistans);
        
        if (Physics.Raycast(_raycast, out _raycastHit))
        {
            _enabled = true;
            nameTag = _raycastHit.collider.tag.ToString();
            Debug.DrawRay(transform.position, transform.forward * _rayDistans, Color.green);
        }
        else
        {
            _enabled = false;
            Debug.DrawRay(transform.position, transform.forward * _rayDistans, Color.red);
        }
        
    }

    private void Click()
    {
        if(_raycastHit.collider.tag == "ButtonLeftAirlock")
        {
            _airlockDoorControl.ButtonOne();
        }
        if (_raycastHit.collider.tag == "ButtonRightAirlock")
        {
            _airlockDoorControl.ButtonTwo();
        }
        if (_raycastHit.collider.tag == "ButtonDisinfection")
        {
            _airlockDoorControl.ButtonThree();
        }
    }

}

