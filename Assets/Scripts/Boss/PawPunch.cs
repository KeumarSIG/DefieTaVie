using UnityEngine;
using System.Collections;

public class PawPunch : MonoBehaviour 
{
	public GameObject leftPaw;
	public GameObject rightPaw;
	
	public float pawCoolDown;
	public float minDistance;
	public float maxDistance;
	public float timeStuck;
	public float timeBetweenTwoPawPunch;
	
	//private Transform player;
	private float distanceFromPlayer;
	
	private bool pawDispo = true;

	[HideInInspector]
	public bool pawPunching = false;

	// Use this for initialization
	void Start () 
	{
		//player = GetComponent<BossAttack>().player;
	}

	void Update () // Vérifie si le joueur est assez pret et qu'aucune autre attaque est en cours
	{
		distanceFromPlayer = GetComponent<BossAttack>().distanceFromPlayer;
		
		if(transform.childCount == 3)
		{
			if(minDistance < distanceFromPlayer && distanceFromPlayer < maxDistance && pawDispo == true)
			{
				pawDispo = false;
				StartCoroutine("Attack");
			}
		}

	}

	IEnumerator Attack () // Alternance attaque patte droite, patte gauche, avec un temps entre les deux et des fenetres d'attaque pour le joueur
	{
		pawPunching = true;
		rightPaw.SetActive(true);
		GetComponent<BossAttack>().rotationSpeed = 0f;
		yield return new WaitForSeconds(timeStuck);
		rightPaw.SetActive(false);
		GetComponent<BossAttack>().rotationSpeed = 0.05f;

		yield return new WaitForSeconds(timeBetweenTwoPawPunch);

		leftPaw.SetActive(true);
		GetComponent<BossAttack>().rotationSpeed = 0f;
		yield return new WaitForSeconds(timeStuck);
		leftPaw.SetActive(false);
		GetComponent<BossAttack>().rotationSpeed = 0.05f;

		yield return new WaitForSeconds(1);
		GetComponent<BossAttack>().rotationSpeed = 0.2f;
		pawPunching = false;

		yield return new WaitForSeconds(pawCoolDown);
		pawDispo = true;
	}
}
