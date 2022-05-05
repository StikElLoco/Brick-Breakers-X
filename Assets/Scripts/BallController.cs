using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] MainManager mainManager;

    private Rigidbody ballRb;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        mainManager.PlayAudioClip(1);
    }

    private void OnCollisionExit(Collision other)
    {
        //Get ball velocity
        var velocity = ballRb.velocity;
        //Accelerate after collision
        velocity += velocity.normalized * 0.1f;
        //Check if completetly vertical, if so add some force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }
        //Check if completetly horizontal, if so add some force
        if (Vector3.Dot(velocity.normalized, Vector3.right) < 0.1f)
        {
            velocity += velocity.x > 0 ? Vector3.right * 0.5f : Vector3.left * 0.5f;
        }
        //Max velocity
        if (velocity.magnitude > 10.0f * mainManager.difficultyMultiplier)
        {
            velocity = velocity.normalized * 10.0f * mainManager.difficultyMultiplier;
        }
        ballRb.velocity = velocity;
    }
}
