using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextReader : MonoBehaviour
{
    [SerializeField] Text textUI;
    [SerializeField] string path;

    void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, "The hermetic lock scenario.txt");
        if (File.Exists(path))
        {
            if (textUI != null)
            {
                textUI.text = File.ReadAllText(path);
            }
        }
        else
        {
            textUI.text = "Файл не найден!";
        }
    }
}
