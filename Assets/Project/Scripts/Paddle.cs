using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

  // Paddle Speed
  public float speed = 1f;
	public float speedMultiplier = 2f;

  // Player Paddle
  public int playerIndex = 1;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
		float verticalMovement = Input.GetAxis("Vertical");

    GetComponent<Rigidbody>().velocity = new Vector3(
      0,
      (verticalMovement * speed) * speedMultiplier,
			0
    );
  }
}
