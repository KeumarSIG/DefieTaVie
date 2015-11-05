using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PushBack : MonoBehaviour 
{
	public float coolDownPushBack = 0.25f;
	public float pushBackForce = 15;
	public float pushDuration;
	public Ease ease;

	public Transform player;

	private bool bumped = false;
	private Vector3 bump;

	
	void Start () 
	{
		DOTween.Init();
	}

	void Update () // Calcule le vector3 entre le boss et le joueur correspondant au bump
	{
		bump = player.position - transform.position;
		bump = bump.normalized;
	}

	void OnTriggerStay2D (Collider2D other) // Bump the player in opposite direction of the boss
	{
		if (bumped == false) // If the character is not currently being bumped (gives invulnerability frames)
		{
			if (other.gameObject.tag == "Player") 
			{
				bumped = true;

				float speed = other.gameObject.GetComponent<Character>().speed;

				other.gameObject.GetComponent<Rigidbody2D>().DOMove(bump * speed * pushBackForce, pushDuration).SetEase(ease);
				StartCoroutine ("resetBump");
			}
		}
	}

	IEnumerator resetBump()
	{
		yield return new WaitForSeconds (pushDuration);
		bumped = false;
	}
}
