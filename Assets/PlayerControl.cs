using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    bool leftPressed = false;
    bool rightPressed = false;

    private readonly float angularSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    // Physics
    void FixedUpdate()
    {
        if (leftPressed)
        {
            leftPressed = false;
            Vector3 rotation = transform.eulerAngles;
            float newRotation = rotation.z + angularSpeed;
            if (newRotation > 70 && newRotation < 290)
            {
                newRotation = 70f;
            }
            rotation = new Vector3(0, 0, newRotation);
            transform.eulerAngles = rotation;
        }
        if (rightPressed)
        {
            rightPressed = false;
            Vector3 rotation = transform.eulerAngles;
            float newRotation = rotation.z - angularSpeed;
            if (newRotation > 70 && newRotation < 290)
            {
                newRotation = 290f;
            }
            rotation = new Vector3(0, 0, newRotation);
            transform.eulerAngles = rotation;
        }
    }
}
