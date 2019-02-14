using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private float xSpeed;
    private float ySpeed;

    private float offScreenHeight;
    private float offScreenWidth;
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
            Vector3 position = transform.position;
            position.y += ySpeed * Time.deltaTime;
            position.x += xSpeed * Time.deltaTime;
            transform.position = position;
        }
    }

    public void SetVector(float angle, float speed)
    {
        ySpeed = Mathf.Sin((angle + 90) * Mathf.Deg2Rad) * speed;
        xSpeed = Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * speed;
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

        offScreenHeight = cameraCenterPosition.y + (cameraHeight / 2f);
        offScreenWidth = cameraCenterPosition.x + (cameraWidth / 2f);

        isConfigured = true;
    }
}
