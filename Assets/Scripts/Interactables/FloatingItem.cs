using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [Header("Configuración de Flotación")]
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatAmplitude = 0.2f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
