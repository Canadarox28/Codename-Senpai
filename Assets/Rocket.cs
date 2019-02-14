using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public float angle;

    public float DestroyBufferHeight = 1f;

    public float SideBuffer = 1f;

    private float yFactor = 1;
    private float xFactor = 1;

    private bool isAngleSet = false;
    private bool isConfigured = false;

    private float offScreenHeight;
    private float offScreenWidth;

    // Start is called before the first frame update
    void Start()
    {
        if (!isConfigured)
        {
            ConfigureCameraPosition();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAngleSet)
        {
            Vector3 position = this.transform.position;
            position.x += speed * xFactor * Time.deltaTime;
            position.y += speed * yFactor * Time.deltaTime;
            this.transform.position = position;
        }

        if (this.transform.position.y <= offScreenHeight)
        {
            // Rocket hits base
            Destroy(gameObject);
        }
    }

    public void SetAngle(float angle)
    {
        if (!isConfigured)
        {
            ConfigureCameraPosition();
        }

        //Ensure rocket will not fly off screen to the sides
        float top = this.transform.position.y;
        float bottom = offScreenHeight;
        float width = offScreenWidth - Mathf.Abs(this.transform.position.x);
        
        // Ensure the rocket does not spawn with an angle that will bring it off screen
        float maxAngle = (Mathf.Atan(width / (top - bottom)) * Mathf.Rad2Deg);
        float clampedAngle;

        if (this.transform.position.x < 0)
        {
            maxAngle = -90 - maxAngle;
            clampedAngle = Mathf.Max(maxAngle, angle);
        }
        else
        {
            maxAngle = maxAngle - 90;
            clampedAngle = Mathf.Min(maxAngle, angle);
        }

        yFactor = Mathf.Sin(clampedAngle * Mathf.Deg2Rad);
        xFactor = Mathf.Cos(clampedAngle * Mathf.Deg2Rad);
        isAngleSet = true;
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

        offScreenHeight = cameraCenterPosition.y - (cameraHeight / 2f) + DestroyBufferHeight;
        offScreenWidth = cameraCenterPosition.x + (cameraWidth / 2f) - SideBuffer;

        isConfigured = true;
    }
}
