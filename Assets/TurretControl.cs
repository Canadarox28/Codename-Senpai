using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [System.NonSerialized] public float angularSpeed = 5f;

    private bool leftPressed = false;
    private bool rightPressed = false;

    private readonly float minAngle = 70f;
    private readonly float maxAngle = 290f;

    // Graphics and Input
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            leftPressed = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rightPressed = true;
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
    }
}
