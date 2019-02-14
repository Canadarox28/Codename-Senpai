using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    public GameObject RocketPrefab;

    public float RocketSpawnDelaySecondsMin = 2f;
    public float RocketSpawnDelaySecondsMax = 5f;

    public float RocketSpawnSpeedMin = 0.5f;
    public float RocketSpawnSpeedMax = 1f;

    public float RocketSpawnAngleMin = -80f;
    public float RocketSpawnAngleMax = -100f;

    public float RocketSpawnEdgeBuffer = 1f;

    private float RocketSpawnPositionMin;
    private float RocketSpawnPositionMax;

    private float timeSinceLastSpawn = 0f;
    private float timeToNextSpawn = 0f;

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
            GetNextSpawnTime();
        }

        timeSinceLastSpawn += Time.deltaTime;
    }

    private void GetNextSpawnTime()
    {
        timeToNextSpawn = Random.Range(RocketSpawnDelaySecondsMin, RocketSpawnDelaySecondsMax);
    }

    private void SpawnNewRocket()
    {
        timeSinceLastSpawn = 0;
        Vector3 spawnPosition = this.transform.position;
        spawnPosition.x = Random.Range(RocketSpawnPositionMin, RocketSpawnPositionMax);
        GameObject newRocket = Instantiate(RocketPrefab, spawnPosition, Quaternion.identity, this.transform);
        Rocket rocket = newRocket.GetComponentInChildren<Rocket>();
        if (rocket == null)
        {
            Debug.LogError("Rocket component not found!");
        }
        rocket.speed = Random.Range(RocketSpawnSpeedMin, RocketSpawnSpeedMax);
        rocket.SetAngle(Random.Range(RocketSpawnAngleMin, RocketSpawnAngleMax));
    }
}
