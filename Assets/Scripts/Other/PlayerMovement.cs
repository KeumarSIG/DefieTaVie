using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed;

	private Vector3 movement;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		movement = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		movement = movement.normalized * speed * Time.deltaTime;
	}

	void FixedUpdate ()
	{
		rb.MovePosition(transform.position + movement);
	}
}
