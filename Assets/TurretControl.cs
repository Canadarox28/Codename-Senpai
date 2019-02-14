using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public GameObject CannonballPrefab;

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool firePressed = false;

    private readonly float minAngle = 70f;
    private readonly float maxAngle = 290f;

    private float angularSpeed = 5f;
    private float cannonballSpeed = 1f;
    private float cannonballCooldown = 1f;
    private float timeSinceLastCannonball = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Graphics and Input
    void Update()
    {
        timeSinceLastCannonball += Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            leftPressed = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rightPressed = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            firePressed = true;
        }
    }

    // Results
    void FixedUpdate()
    {
        if (leftPressed)
        {
            leftPressed = false;
            Vector3 rotation = transform.eulerAngles;
            float newRotation = rotation.z + angularSpeed;
            if (newRotation > minAngle && newRotation < maxAngle)
            {
                newRotation = minAngle;
            }
            rotation = new Vector3(0, 0, newRotation);
            transform.eulerAngles = rotation;
        }
        if (rightPressed)
        {
            rightPressed = false;
            Vector3 rotation = transform.eulerAngles;
            float newRotation = rotation.z - angularSpeed;
            if (newRotation > minAngle && newRotation < maxAngle)
            {
                newRotation = maxAngle;
            }
            rotation = new Vector3(0, 0, newRotation);
            transform.eulerAngles = rotation;
        }
        if (firePressed)
        {
            firePressed = false;
            if (timeSinceLastCannonball > cannonballCooldown)
            {
                Instantiate(CannonballPrefab, transform.position, Quaternion.identity);
                Cannonball newCannonball = GetComponentInChildren<Cannonball>();
                newCannonball.SetVector(transform.eulerAngles.z, cannonballSpeed);
            }
        }
    }
}
