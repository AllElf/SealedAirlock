using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] RaycastClick raycastClick;
    public bool _pause;
    public bool _music;
    [SerializeField] string _nameScene;
    [SerializeField] GameObject _panelPause;
    [SerializeField] GraphicRaycaster _graphicRaycaster;
    public AudioSource[] audioSources;

    void Awake()
    {
        raycastClick  = GameObject.FindObjectOfType<RaycastClick>();
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        _pause = false;
        _music = true;
        _panelPause.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _pause = !_pause;
            Pause();
        }  
    }
    public void paiseButton()
    {
        _pause = !_pause;
        Pause();
    }
    public void PauseButton()
    {
        
        Time.timeScale = 1f;
        _pause = false;
        Pause();
    }
    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_nameScene);
    }
    public void Music()
    {
        
        _music = !_music;
        if (_music == false)
        {
            if (audioSources != null)
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i].tag == "mainSound")
                    {
                        audioSources[i].Stop();
                    }
                }
            }
        }
        else if (_music == true)
        {
                if (audioSources != null)
                {
                    for (int i = 0; i < audioSources.Length; i++)
                    {
                        if (audioSources[i].tag == "mainSound")
                        {
                            audioSources[i].Play();
                        }
                    }
                }
        }
    }
    public void Pause()
    {
        if (_pause == true)
        {
            raycastClick._rayDistans = 0.0f;
            _panelPause.SetActive(true);
            _graphicRaycaster.enabled = true;
            if(audioSources != null)
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i].tag != "mainSound")
                    {
                        audioSources[i].Pause();
                    }
                }
            }
            
            Time.timeScale = 0f;
        }
        else if (_pause == false)
        {
            _panelPause.SetActive(false);
            _graphicRaycaster.enabled = false;
            Time.timeScale = 1.0f;
            raycastClick._rayDistans = 20.0f;
            if (audioSources != null)
            {
                for (int i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i].tag != "mainSound" && !audioSources[i].isPlaying && audioSources[i].time > 0)
                    {
                        audioSources[i].Play();
                    }
                }
            }
        }
    }
}
