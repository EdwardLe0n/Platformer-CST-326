using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 20f;
    public float jumpBoost = 5f;
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    { 



    }

    // Update is called once per frame
    void Update()
    {

        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rbody = GetComponent<Rigidbody>();
        rbody.velocity += Vector3.right * horizontalMovement * Time.deltaTime * speed;

        // Capsule
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.03f;

        Vector3 startPoint = transform.position;
        Vector3 endpoint = startPoint + Vector3.down * halfHeight;

        isGrounded = (Physics.Raycast(startPoint, Vector3.down, halfHeight));
        Color lineColor = (isGrounded) ? Color.red: Color.blue;
        Debug.DrawLine(startPoint, endpoint, lineColor, 0f, true);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space))
        {
            if (rbody.velocity.y > 0f)
            {
                rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
            }
        }

        if (Mathf.Abs(rbody.velocity.x) > maxSpeed)
        {
            Vector3 newVel = rbody.velocity;
            newVel.x = Mathf.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rbody.velocity = newVel;
        }

        if (isGrounded && Mathf.Abs(horizontalMovement) < 0.5f)
        {
            Vector3 newVel = rbody.velocity;
            newVel.x = 1f - Time.deltaTime;
            rbody.velocity = newVel;
        }

        rbody.velocity *= Mathf.Abs(horizontalMovement);

        float yaw = (rbody.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

    }
}
