using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 30f;
    public float jumpBoost = 5f;
    public bool isGrounded = false;

    public GameObject gameManager;

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

        // When the left mouse button is clicked, it will check if the user attemppted to click on something
        // Source: https://docs.unity3d.com/ScriptReference/Input.GetMouseButton.html

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "QueBlock")
                {
                    gameManager.GetComponent<GameManagerScript>().addCoin();
                }
                else if (hit.collider.gameObject.tag == "Brick")
                {
                    Destroy(hit.collider.gameObject, 1);
                }
            }

        }

    }

}
