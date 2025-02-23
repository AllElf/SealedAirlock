using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas canvas; // Ссылка на Canvas
    public Image imagePrefab;
    public string prefabNameLike = "Like";
    public string prefabNameDislike = "Dislike";
    [SerializeField] int prefabIndex = -1;
    Vector3 vec;
    [SerializeField] RectTransform[] rectTransforms;

    public void PlusPointlike()
    {
        if (rectTransforms != null && prefabIndex < rectTransforms.Length -1)
        {
            prefabIndex++;
            Image prefab = Resources.Load<Image>(prefabNameLike);
            imagePrefab = prefab;
            vec = new Vector3(0.4f, 1f, 1f);
            CreateButton(rectTransforms[prefabIndex].anchoredPosition); 
        }

    }

    public void PlusPointDislike()
    {
        if (rectTransforms != null && prefabIndex < rectTransforms.Length -1)
        {
            prefabIndex++;
            Image prefab = Resources.Load<Image>(prefabNameDislike);
            imagePrefab = prefab;
            CreateButton(rectTransforms[prefabIndex].anchoredPosition); 
        }

    }

    void CreateButton(Vector2 position)
    {
        Image newImage = Instantiate(imagePrefab, canvas.transform);
        RectTransform rectTransform = newImage.rectTransform; // Не нужен GetComponent
        rectTransform.anchoredPosition = position;
    }

}
