using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject RocketPrefab;

    [System.NonSerialized] public float RocketSpawnDelaySecondsMin = 2f;
    [System.NonSerialized] public float RocketSpawnDelaySecondsMax = 5f;

    [System.NonSerialized] public float RocketSpawnSpeedMin = 0.5f;
    [System.NonSerialized] public float RocketSpawnSpeedMax = 1f;

    [System.NonSerialized] public float RocketSpawnAngleMaxLeft = -10f;
    [System.NonSerialized] public float RocketSpawnAngleMaxRight = 10f;

    [System.NonSerialized] public float RocketSpawnEdgeBuffer = 1f;

    private float RocketSpawnPositionMin;
    private float RocketSpawnPositionMax;

    private float timeSinceLastSpawn = 0f;
    private float timeToNextSpawn = 0f;

    private readonly float SideBuffer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get x coordinate range for rocket spawning
        Camera camera = FindObjectOfType<Camera>();

        if (camera == null)
        {
            Debug.LogError("Camera not found!");
        }

        float cameraHeight = camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * camera.aspect;
        Vector3 cameraPosition = camera.transform.position;
        cameraPosition.x -= cameraWidth / 2f;

        RocketSpawnPositionMin = cameraPosition.x + RocketSpawnEdgeBuffer;
        RocketSpawnPositionMax = cameraPosition.x + cameraWidth - RocketSpawnEdgeBuffer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeSinceLastSpawn >= timeToNextSpawn)
        {
            SpawnNewRocket();
            timeSinceLastSpawn = 0;
            timeToNextSpawn = Random.Range(RocketSpawnDelaySecondsMin, RocketSpawnDelaySecondsMax);
        }

        timeSinceLastSpawn += Time.deltaTime;
    }

    private void SpawnNewRocket()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.x = Random.Range(RocketSpawnPositionMin, RocketSpawnPositionMax);
        float angle = GetRocketAngle(spawnPosition.x);
        GameObject newRocket = Instantiate(RocketPrefab, spawnPosition, Quaternion.Euler(0f, 0f, angle));
        Rocket rocket = newRocket.GetComponentInChildren<Rocket>();
        rocket.name = "Rocket";
        if (rocket == null)
        {
            Debug.LogError("Rocket component not found!");
        }
        rocket.SetSpeed(Random.Range(RocketSpawnSpeedMin, RocketSpawnSpeedMin));
    }

    private float GetRocketAngle(float spawnPosition)
    {
        Camera camera = FindObjectOfType<Camera>();

        if (camera == null)
        {
            Debug.LogError("Camera not found!");
        }

        float cameraHeight = camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * camera.aspect;
        float top = transform.position.y;
        float bottom = camera.transform.position.y - (cameraHeight / 2f);
        float screenEndpointRight = camera.transform.position.x + (cameraWidth / 2f) - SideBuffer;
        float screenEndpointLeft = camera.transform.position.x - (cameraWidth / 2f) + SideBuffer;

        float widthToRight = screenEndpointRight - spawnPosition;
        float maxAngleRight = (Mathf.Atan(widthToRight / (top - bottom)) * Mathf.Rad2Deg);
        maxAngleRight = Mathf.Min(maxAngleRight, RocketSpawnAngleMaxLeft);

        float widthToLeft = spawnPosition - screenEndpointLeft;
        float maxAngleLeft = -(Mathf.Atan(widthToLeft / (top - bottom)) * Mathf.Rad2Deg);
        maxAngleLeft = Mathf.Max(maxAngleLeft, RocketSpawnAngleMaxRight);

        float clampedAngle = Random.Range(maxAngleRight, maxAngleLeft);

        return clampedAngle;
    }
}
