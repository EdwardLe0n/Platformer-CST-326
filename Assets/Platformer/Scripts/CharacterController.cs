using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed = 2f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 30f;
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
        float halfHeight = col.bounds.extents.y;

        Vector3 startPoint = transform.position;
        startPoint.y += 0.01f;
        Vector3 endpoint = startPoint + Vector3.down * halfHeight;


        RaycastHit hit;
        Ray ray = new Ray(startPoint, endpoint);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "QueBlock" || hit.collider.gameObject.tag == "Brick" || hit.collider.gameObject.tag == "WObject")
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }

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
            Debug.Log("Clamped");
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

        float xSpeed = System.Math.Abs(rbody.velocity.x);

        Animator anim = GetComponent<Animator>();

        anim.SetFloat("Speed", xSpeed);
        anim.SetBool("In Air", !isGrounded);

    }
}
