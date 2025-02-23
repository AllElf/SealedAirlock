using UnityEngine;

public class ZummerSencor : MonoBehaviour
{
    [SerializeField] GameObject _zummer;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] AudioSource[] _audioSources;

    private void Start()
    {
        _audioSources = new AudioSource[2];
        if (GameObject.FindGameObjectWithTag("ZunnerSound") != null)
        {
            _zummer = GameObject.FindGameObjectWithTag("ZunnerSound");
            _spriteRenderer = _zummer.GetComponent<SpriteRenderer>();
        } 
        if(GameObject.FindGameObjectWithTag("ZummerSound") != null && GameObject.FindGameObjectWithTag("ZummerSoundStop") != null)
        {
            _audioSources[0] = GameObject.FindGameObjectWithTag("ZummerSound").GetComponent<AudioSource>();
            _audioSources[1] = GameObject.FindGameObjectWithTag("ZummerSoundStop").GetComponent<AudioSource>();
        }
    }
    private void FixedUpdate()
    {
        if (_audioSources[0].isPlaying || _audioSources[1].isPlaying)
        {
            _spriteRenderer.enabled = true;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}
