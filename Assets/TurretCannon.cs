using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    public GameObject CannonballPrefab;

    private Animator animator;
    private Renderer sprite;

    private bool firePressed = false;

    private float cannonballSpeed = 1f;
    private float cannonballCooldown = 1f;
    private float timeSinceLastCannonball = 0f;

    private float spriteWidth = 0f;

    private readonly string animateFire = "Fire";

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Renderer>();
        animator = GetComponent<Animator>();

        spriteWidth = sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCannonball += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            firePressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (firePressed)
        {
            firePressed = false;
            if (timeSinceLastCannonball > cannonballCooldown)
            {
                animator.SetTrigger(animateFire);
                float spawnX;
                if (transform.rotation.w < 0)       // Turret facing right
                {
                    spawnX = sprite.bounds.max.x;
                }
                else                                // Turret facing left
                {
                    spawnX = sprite.bounds.min.x;
                }
                Vector3 spawnVector = new Vector3(spawnX - spriteWidth, sprite.bounds.max.y, transform.position.z);
                GameObject initialisedCannonball = Instantiate(CannonballPrefab, spawnVector, transform.rotation);
                Cannonball newCannonball = initialisedCannonball.GetComponentInChildren<Cannonball>();
                if (newCannonball == null)
                {
                    Debug.LogError("Could not find component Cannonball!");
                }
                newCannonball.SetVector(transform.eulerAngles.z, cannonballSpeed);

                timeSinceLastCannonball = 0f;
            }
        }
    }
}
