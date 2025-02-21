using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float sensitivity = 100.0f;
    [SerializeField] float rotationX = 0.0f;
    [SerializeField] float rotationY = 0.0f;
    [SerializeField] Transform _transform;

    void Start()
    {
        //Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        Cursor.visible = true;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Cursor.SetCursor(null, center, CursorMode.Auto);

        // Перемещение камеры
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime /3;
        float translationZ = Input.GetAxis("Vertical") * speed * Time.deltaTime /3;
        transform.Translate(translationX, 0, translationZ);
        if (Input.GetKey(KeyCode.E))
        {
            _transform.position = new Vector3(gameObject.transform.position.x, transform.position.y + 0.1f * Time.deltaTime * speed, transform.position.z);
            gameObject.transform.position = _transform.position;
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            _transform.position = new Vector3(gameObject.transform.position.x, transform.position.y - 0.1f * Time.deltaTime * speed, transform.position.z);
            gameObject.transform.position = _transform.position;
        }
            // Вращение камеры
            rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f); // Ограничение вращения по вертикали
        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0.0f);
    }
}
