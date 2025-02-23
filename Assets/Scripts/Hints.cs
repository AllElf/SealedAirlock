using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    [SerializeField] RaycastClick raycastClick;
    [SerializeField] Text _hints;
    [SerializeField] string _hintTeg;
    [SerializeField] UIManager uIManager;
    [SerializeField] MenuManager menuManager;
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] ElectricityController electricityController;
    [SerializeField] Light Light;
    [SerializeField] Color _energyDeclineColor, _energyReturnColor, _emergencyColor;
    [SerializeField] AirlockDoorControl airlockDoorControl;
    [SerializeField] ParticleSystem _oxigen;
    void Start()
    {
        raycastClick = GameObject.FindObjectOfType<RaycastClick>();
        _hints = GameObject.FindGameObjectWithTag("HintsText").GetComponent<Text>();
        uIManager = GameObject.FindObjectOfType<UIManager>();
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        audioSources = menuManager.audioSources;
        electricityController = GameObject.FindObjectOfType<ElectricityController>();
        Light = GameObject.FindGameObjectWithTag("Direction").GetComponent<Light>();
        airlockDoorControl = GameObject.FindObjectOfType<AirlockDoorControl>();
        _oxigen = GameObject.FindGameObjectWithTag("Oxigen").GetComponent <ParticleSystem>();
    }
    void Update()
    {
        _oxigen.Pause();
        _hintTeg = raycastClick.nameTag;
        HintsFunction();
    }

    void HintsFunction()
    {
        if(_hintTeg == "ButtonLeftAirlock")
        {
            _hints.text = "Кнопка левой двери\n гермошлюза";
        }
        else if (_hintTeg == "ButtonDisinfection")
        {
            _hints.text = "Кнопка для запуска\n дезинфекции";
        }
        else if (_hintTeg == "ButtonRightAirlock")
        {
            _hints.text = "Кнопка правой двери\n гермошлюза";
        }
        else if (_hintTeg == "ButtonEmergency")
        {
            _hints.text = "Аварийная кнопка";
            if (Input.GetKeyUp(KeyCode.Mouse0) && airlockDoorControl._emergencytText.text == "Аварийная блокировка гермо-шлюза активна")
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i].tag == "Emergency")
                    {
                        audioSources[i].Play();
                        Light.color = _emergencyColor;
                        StartCoroutine(Oxigen());
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && airlockDoorControl._emergencytText.text != "Аварийная блокировка гермо-шлюза активна")
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                   
                    if (audioSources[i].tag == "Emergency")
                    {
                        audioSources[i].Stop();
                        Light.color = _energyReturnColor;
                        _oxigen.Stop();
                    }
                }
            }
        }
        else if (_hintTeg == "InstructionButton")
        {
            _hints.text = "Кнопка инструкции";
        }
        else if (_hintTeg == "ZUMMER")
        {
            _hints.text = "Зумер";
        }
        else if (_hintTeg == "FlapDoor")
        {
            _hints.text = "Электрощиток";
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i].tag == "OpenFlapDoor")
                    {
                        audioSources[i].Play();
                        Debug.Log("Сработало!");
                    }
                }
            }
        }
        else if (_hintTeg == "BoxFlapDoor")
        {
            _hints.text = "Электрощиток";
        }
        else if (_hintTeg == "TheSwitch")
        {
            _hints.text = "Переключатель электричества";
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i].tag == "Lever")
                    {
                        audioSources[i].Play();  
                    }
                    if (electricityController.moveSpeed == 0 && audioSources[i].tag == "PowerIncreased")
                    {
                        audioSources[i].Play();
                        Light.color = _energyReturnColor;  
                    }
                    else if (electricityController.moveSpeed != 0 && audioSources[i].tag == "PowerDropped")
                    {
                        audioSources[i].Play();
                        Light.color = _energyDeclineColor;
                    }
                }
            }
        }
        else if (_hintTeg == "Valve(Left)")
        {
            _hints.text = "Левая шаровая ручка\nГермошлюза";
        }
        else if (_hintTeg == "Valve(Right)")
        {
            _hints.text = "Правая шаровая ручка\nГермошлюза";
        }
        else if (_hintTeg == "Insect")
        {
            _hints.text = "Насекомое";
        }
        else if (_hintTeg == "Hermetic lock")
        {
            _hints.text = "Гермошлюз";
        }
        else if (_hintTeg == "MainMenu")
        {
            _hints.text = "Главное меню";
        }
        else if (_hintTeg == "Fallout")
        {
            _hints.text = "Нажми сюда если всё\n работает";
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                uIManager.PlusPointlike();
            }
        }
        else if (_hintTeg == "Fallout2")
        {
            _hints.text = "Нажми сюда если что-то\nне работает";
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                uIManager.PlusPointDislike();
            }
        }
        else
        {
            _hints.text = "";
        }
    }
    IEnumerator Oxigen()
    {
        _oxigen.Play();
        yield return new WaitForSeconds(4);
        _oxigen.Stop();
        yield break;
    }
}
