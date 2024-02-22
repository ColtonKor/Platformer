using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLive : MonoBehaviour
{
    public float acceleration = 15f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 51f;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate(){
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += Vector3.right * horizontalMovement * Time.deltaTime * acceleration;

        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.01f;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * 2f;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);

        // Color lineColor = (isGrounded) ? Color.red : Color.blue;
        // Debug.DrawLine(startPoint, endPoint, lineColor, 0f, true);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }

        // if(Math.Abs(rb.velocity.x) > maxSpeed){
        //     Vector3 newVel = rb.velocity;
        //     newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
        //     rb.velocity = newVel;
        // }

    }
}
