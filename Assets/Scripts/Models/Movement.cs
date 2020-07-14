using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public void Move(Vector3 newPosition)
    {
        float x = newPosition.x * 0.01f;
        float y = newPosition.y * 0.01f;
        float z = newPosition.z * 0.01f;

        transform.position = new Vector3(x, y, z);
    }
}
