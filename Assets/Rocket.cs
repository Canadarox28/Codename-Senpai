using UnityEngine;

public class Rocket : MonoBehaviour
{
    private readonly float DestroyBufferHeight = 1f;

    private float speed;
    private bool isSpeedSet = false;
    private float offScreenHeight;
    public ScoreController ScoreController;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureCameraPosition();
        ScoreController = FindObjectOfType<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpeedSet)
        {
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }

        if (transform.position.y <= offScreenHeight)
        {
            // Rocket hits base
            ScoreController.AddHealth(-10);
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        isSpeedSet = true;
    }

    public void OnHit()
    {
        // Todo: Variable health, etc
        ScoreController.AddScore(10);
        Destroy(gameObject);
    }

    /// <summary>
    /// Get the position and size of the camera, with a buffer to determine where the rocket should explode.
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
    }
}
