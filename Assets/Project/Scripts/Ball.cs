using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

  // Multiplied on Paddle Collision
  private float difficultyMultiplier = 1.1f;

  // Pong Ball Speed
  private float minXSpeed = 1.8f;
  private float maxXSpeed = 3.2f;
  private float minYSpeed = 1.8f;
  private float maxYSpeed = 3.2f;

  private float rotation = 90f;
  private float rotationSpeed = 3.2f;

  private Rigidbody ballRigidbody;


  // Use this for initialization
  void Start()
  {
    ballRigidbody = GetComponent<Rigidbody>();
    ballRigidbody.velocity = new Vector3(
      Random.Range(minXSpeed, maxXSpeed) * 2 * (Random.value > 0.5f ? -1 : 1) * difficultyMultiplier,
      Random.Range(minYSpeed, maxYSpeed) * 2 * (Random.value > 0.5f ? -1 : 1) * difficultyMultiplier,
      0
    );

    // Rigidbody.velocity.x * difficultyMultiplier;
    rotateCube(rotation, rotationSpeed);
  }


  void rotateCube(float rot, float speed)
  {
    Debug.Log("Rotate Cube" + rot);
    ballRigidbody.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, rot, 0)), Time.deltaTime * speed);
  }


  // Update is called once per frame
  void Update()
  {
    rotateCube(rotation, rotationSpeed);
  }


  void OnTriggerEnter(Collider otherCollider)
  {
    /**
		 * Wall Collision
		 */
    if (otherCollider.tag == "Wall")
    {
      // GetComponent<AudioSource>().Play();
      // Collided with the top limit
      if (otherCollider.transform.position.y > transform.position.y && ballRigidbody.velocity.y > 0)
      {
        ballRigidbody.velocity = new Vector3(
          ballRigidbody.velocity.x,
          -ballRigidbody.velocity.y,
          0
        );
      }

      // Collided with the bottom limit
      if (otherCollider.transform.position.y < transform.position.y && ballRigidbody.velocity.y < 0)
      {
        ballRigidbody.velocity = new Vector3(
          ballRigidbody.velocity.x,
          -ballRigidbody.velocity.y,
          0
        );
      }
    }


    /**
     * Paddle Collision
     */
    if (otherCollider.tag == "Paddle")
    {
      // GetComponent<AudioSource>().Play();
      // Collided with the left paddle
      if (otherCollider.transform.position.x < transform.position.x && ballRigidbody.velocity.x < 0)
      {
        ballRigidbody.velocity = new Vector3(
          -ballRigidbody.velocity.x * difficultyMultiplier,
          ballRigidbody.velocity.y * difficultyMultiplier,
          0
        );

        ballRigidbody.gameObject.GetComponent<ParticleSystem>().Play();
        rotation = -90f;
        // rotate ball
        // float yRotation = -90.0f;
        // ballRigidbody.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
      }
      // Collided with the right paddle
      if (otherCollider.transform.position.x > transform.position.x && ballRigidbody.velocity.x > 0)
      {
        ballRigidbody.velocity = new Vector3(
          -ballRigidbody.velocity.x * difficultyMultiplier,
          ballRigidbody.velocity.y * difficultyMultiplier,
          0
        );
        ballRigidbody.gameObject.GetComponent<ParticleSystem>().Play();
        rotation = 90f;
        // rotate ball
        // float yRotation = 90.0f;
        // ballRigidbody.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
      }
    }


    /**
     * Goal Collision
     */
    if (otherCollider.tag == "Goal")
    {
      GetComponent<AudioSource>().Play();
      // Collided with the left goal
      if (otherCollider.transform.position.x < transform.position.x && ballRigidbody.velocity.x < 0)
      {
        Debug.Log("Left Goal");
        ballRigidbody.velocity = new Vector3(
          -ballRigidbody.velocity.x * difficultyMultiplier,
          ballRigidbody.velocity.y * difficultyMultiplier,
          0
        );

      }
      // Collided with the right goal
      if (otherCollider.transform.position.x > transform.position.x && ballRigidbody.velocity.x > 0)
      {
        Debug.Log("Right Goal");
        ballRigidbody.velocity = new Vector3(
          -ballRigidbody.velocity.x * difficultyMultiplier,
          ballRigidbody.velocity.y * difficultyMultiplier,
          0
        );
      }
    }
  }
}
