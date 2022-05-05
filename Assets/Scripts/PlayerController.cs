using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MainManager mainManager;

    [SerializeField] private float platformSpeed = 24.0f;

    private float horizontalInput;
    private float xRange = 17.25f;

    // Update is called once per frame
    void Update()
    {
        if(mainManager.hasStarted)
        {
            //Get Horizontal input to variable
            horizontalInput = Input.GetAxisRaw("Horizontal");

            //Move platform
            if (horizontalInput != 0)
            {
                transform.Translate(Vector3.right * horizontalInput * platformSpeed * Time.deltaTime);
            }
        }

        //Platform movement bounds
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }
}
