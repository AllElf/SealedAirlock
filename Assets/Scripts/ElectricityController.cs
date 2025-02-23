using System.Collections;
using UnityEngine;

public class ElectricityController : MonoBehaviour
{
    public Transform pointStart;  // Стартовая точка
    public Transform pointTwo;    // Вторая точка
    public Transform pointThree;  // Третья точка
    public Transform pointEnd;    // Конечная точка (удаление)
    [SerializeField] ElectricSwitch electricSwitch;
    public string prefabName = "TheElectron"; // Имя префаба в папке Resources
    public float spawnRate = 2f;  // Время между спавнами
    public float moveSpeed = 5f;  // Скорость движения объектов
    [SerializeField] float _speedCurrent;
    [SerializeField] AirlockDoorControl airlockDoorControl;


    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {

            if(electricSwitch._enabledElectrick == true)
            {
                moveSpeed = _speedCurrent;
                SpawnObject();
            }
            else if (electricSwitch._enabledElectrick == false)
            {
                moveSpeed = 0f;
                airlockDoorControl._disinfectionParticle[0].Stop();
                airlockDoorControl._disinfectionParticle[1].Stop();
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnObject()
    {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        if (prefab != null)
        {
            GameObject clone = Instantiate(prefab, pointStart.position, Quaternion.identity);
            MovingObject mover = clone.AddComponent<MovingObject>();
            mover.SetPoints(pointTwo, pointThree, pointEnd, moveSpeed);
        }
        else
        {
            Debug.LogError($"Префаб '{prefabName}' не найден в папке Resources!");
        }
    }
}
