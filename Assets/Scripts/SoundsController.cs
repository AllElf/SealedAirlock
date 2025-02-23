using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] AudioSource[] _audioSources;
    [SerializeField] AirlockDoorControl _airlockDoorControl;
    [SerializeField] ElectricityController electricityController;
    [SerializeField] GameObject _TheFlashingLight;
    [SerializeField] Material[] _FlashColor;

    private void Start()
    {
        _airlockDoorControl = GameObject.FindObjectOfType<AirlockDoorControl>();
        electricityController = GameObject.FindObjectOfType<ElectricityController>();
       _audioSources = new AudioSource[4];
        _audioSources[0] = GameObject.FindGameObjectWithTag("LeftDoorSound").GetComponent<AudioSource>();
        _audioSources[1] = GameObject.FindGameObjectWithTag("RighrDoorSound").GetComponent<AudioSource>();
        _audioSources[2] = GameObject.FindGameObjectWithTag("ZummerSound").GetComponent<AudioSource>();
        _audioSources[3] = GameObject.FindGameObjectWithTag("ZummerSoundStop").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_audioSources[2].isPlaying || _audioSources[3].isPlaying)
        {
           
                _TheFlashingLight.GetComponent<MeshRenderer>().material = _FlashColor[0];
            
        }
        if (!_audioSources[2].isPlaying && !_audioSources[3].isPlaying)
        {
            
                _TheFlashingLight.GetComponent<MeshRenderer>().material = _FlashColor[1];
          
        }
    }
    public void OpenAndCloseDoor()
    {
        if(_airlockDoorControl != null && _airlockDoorControl.stateInfoLeft.normalizedTime != 1.0f && electricityController.moveSpeed != 0)
        {
            _audioSources[0].Play();
        }
        if (_airlockDoorControl != null && _airlockDoorControl.stateInfoRight.normalizedTime != 1.0f && electricityController.moveSpeed != 0)
        {
            _audioSources[0].Play();
        }
    }
    public void Zummer()
    {
        if (_airlockDoorControl != null && electricityController.moveSpeed != 0)
        {
            _audioSources[2].Play();
        }
    }
    public void ZummerStop()
    {
        if (_airlockDoorControl != null && electricityController.moveSpeed != 0)
        {
            _audioSources[3].Play();
        }
    }
}
