using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BossAttack : MonoBehaviour 
{
	public Transform player;

	public float distanceFromPlayer;
	public float rotationSpeed;

	//private Rigidbody2D rb;

	private Vector3 direction;

	[HideInInspector]
	public float lookRotation;

	// Use this for initialization
	void Start () 
	{
		//rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		distanceFromPlayer = Vector2.Distance(transform.position, player.position); // Calcule la distance entre le boss et le joueur
	}

	void FixedUpdate () // Calcule la rotation à appliquer pour que le boss se tourne vers le joueur
	{
		direction = player.position - transform.position;
		lookRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(lookRotation, Vector3.forward), rotationSpeed);
	}
}
