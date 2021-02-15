using UnityEngine;
using System.Collections;

public class Playermovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    private bool isGrounded = false;

    private Rigidbody rigidbodyCache;

    private void Awake()
    {
        rigidbodyCache = GetComponent<Rigidbody>();
    }

    void Update() {
        rigidbodyCache.velocity = new Vector3(0, rigidbodyCache.velocity.y, 0); //Set X and Z velocity to 0
 
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0, 0);

        /*if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(); //Manual jumping
        }*/
	}

    void Jump()
    {
        if (!isGrounded) { return; }
        isGrounded = false;
        rigidbodyCache.velocity = new Vector3(0, 0, 0);
        rigidbodyCache.AddForce(new Vector3(0, 700, 0), ForceMode.Force);        
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.0f);
        if (isGrounded)
        {
            Jump(); //Automatic jumping
        }
    }
       

}
