using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] GameObject _rotObj;
   
    void Update()
    {
        gameObject.transform.rotation = _rotObj.transform.rotation;
    }
}
