using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private float speed;

    private float offScreenUp;
    private float offScreenRight;
    private float offScreenLeft;
    private float cameraCenterX;

    private bool isConfigured = false;
    private bool isVectorSet = false;

    private void Start()
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
            transform.Translate(0f, speed * Time.deltaTime, 0f);
        }
        if (transform.position.y >= offScreenUp 
            || transform.position.x >= offScreenRight 
            || transform.position.x <= offScreenLeft)
        {
            // Cannonball is off screen
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Rocket")         // bullet hits rocket
        {
            Rocket rocket = collision.gameObject.GetComponent<Rocket>();
            rocket.OnHit();
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

        offScreenUp = cameraCenterPosition.y + (cameraHeight / 2f);
        offScreenRight = cameraCenterPosition.x + (cameraWidth / 2f);
        offScreenLeft = cameraCenterPosition.x - (cameraWidth / 2f);

        isConfigured = true;
    }
}
