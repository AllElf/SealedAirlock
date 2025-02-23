using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Transform[] points;
    private float moveSpeed;

    public void SetPoints(Transform p2, Transform p3, Transform end, float speed)
    {
        points = new Transform[] { p2, p3, end };
        moveSpeed = speed;
        StartCoroutine(MoveToPoints());
    }

    IEnumerator MoveToPoints()
    {
        foreach (Transform target in points)
        {
            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        Destroy(gameObject); // Удаление при достижении pointEnd
    }
}
