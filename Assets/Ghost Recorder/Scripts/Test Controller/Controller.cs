using UnityEngine;

public class Controller : MonoBehaviour
{
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;

    void LateUpdate()
    {
        float h = horizontalSpeed * Input.GetAxis("Horizontal");// *Time.fixedDeltaTime;
        float v = verticalSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;

        transform.Translate(Vector3.forward * v);
        transform.Rotate(Vector3.up, h);
    }
}
