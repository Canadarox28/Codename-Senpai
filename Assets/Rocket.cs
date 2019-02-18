using UnityEngine;

public class Rocket : MonoBehaviour
{
    private readonly float DestroyBufferHeight = 1f;
    private readonly float SideBuffer = 1f;

    private float speed;

    private bool isVectorSet = false;
    private bool isConfigured = false;

    private float offScreenHeight;
    private float offScreenWidth;
    private float cameraCenterX;

    // Start is called before the first frame update
    void Start()
    {
        if (!isConfigured)
        {
            ConfigureCameraPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isVectorSet)
        {
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }

        if (transform.position.y <= offScreenHeight)
        {
            // Rocket hits base
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        isVectorSet = true;
    }

    /// <summary>
    /// Get the position and size of the camera, with a buffer.
    /// Save values to globals offScreenHeight and offScreenWidth.
    /// </summary>
    private void ConfigureCameraPosition()
    {
        Camera camera = FindObjectOfType<Camera>();

        if (camera == null)
        {
            Debug.LogError("Camera not found!");
        }

        float cameraHeight = camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * camera.aspect;
        Vector3 cameraCenterPosition = camera.transform.position;
        cameraCenterX = cameraCenterPosition.x;

        offScreenHeight = cameraCenterPosition.y - (cameraHeight / 2f) + DestroyBufferHeight;
        offScreenWidth = cameraCenterPosition.x + (cameraWidth / 2f) - SideBuffer;

        isConfigured = true;
    }
}
