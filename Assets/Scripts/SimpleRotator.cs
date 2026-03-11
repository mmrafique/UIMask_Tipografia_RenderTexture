using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [Header("Configuración")]
    public float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
