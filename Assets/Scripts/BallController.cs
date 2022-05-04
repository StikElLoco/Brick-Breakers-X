using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody ballRb;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision other)
    {
        //Get ball velocity
        var velocity = ballRb.velocity;
        //Accelerate after collision
        velocity += velocity.normalized * 0.1f;
        //Check if completetly vertical
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 1.0f : Vector3.down * 1.0f;
        }
        //Max velocity
        if (velocity.magnitude > 5.0f)
        {
            velocity = velocity.normalized * 5.0f;
        }

        ballRb.velocity = velocity;
    }
}
