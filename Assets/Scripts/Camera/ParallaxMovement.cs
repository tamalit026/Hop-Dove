using UnityEngine;

//TP2 - Eileen Moscovich.
public sealed class ParallaxMovement : MonoBehaviour
{
    private Transform cam;
    private Vector3 cameraStartPosition;
    private float distance;

    private GameObject[] backgrounds;
    private Material[] materials;
    private float[] backgroundSpeeds;

    private float farthestBack;

    [Range(0.01f, 1f)]
    [SerializeField] private float parallaxSpeed = 0.5f;

    private void Start()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        cam = Camera.main.transform;
        cameraStartPosition = cam.position;

        int backgroundCount = transform.childCount;

        backgrounds = new GameObject[backgroundCount];
        materials = new Material[backgroundCount];
        backgroundSpeeds = new float[backgroundCount];

        for (int i = 0; i < backgroundCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;

            Renderer backgroundRenderer =
                backgrounds[i].GetComponent<Renderer>();

            if (backgroundRenderer != null)
                materials[i] = backgroundRenderer.material;
        }

        CalculateBackgroundSpeeds(backgroundCount);
    }
    private void CalculateBackgroundSpeeds(int backgroundCount)
    {
        farthestBack = 0f;

        for (int i = 0; i < backgroundCount; i++)
        {
            float depth = Mathf.Abs(
                backgrounds[i].transform.position.z - cam.position.z
            );

            if (depth > farthestBack)
                farthestBack = depth;
        }

        if (farthestBack <= 0.001f)
            farthestBack = 1f;

        for (int i = 0; i < backgroundCount; i++)
        {
            float depth = Mathf.Abs(
                backgrounds[i].transform.position.z - cam.position.z
            );

            backgroundSpeeds[i] = 1f - (depth / farthestBack);
        }
    }
    private void LateUpdate()
    {
        if (cam == null) return;

        distance = cam.position.x - cameraStartPosition.x;

        transform.position = new Vector3(cam.position.x,cam.position.y,transform.position.z);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (materials[i] == null) continue;

            float speed = backgroundSpeeds[i] * parallaxSpeed;

            Vector2 textureOffset = new Vector2(distance * speed,0f);

            materials[i].mainTextureOffset = textureOffset;
        }
    }
}
