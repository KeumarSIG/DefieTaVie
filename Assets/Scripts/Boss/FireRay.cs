using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FireRay : MonoBehaviour 
{
	public GameObject fireRay;
	public Transform fireRaySpawn;
	
	public float timeDuration;
	public float fireRayCoolDown;
	public float minDistance;
	public float maxDistance;
	public float fireRayDuration;
	public Ease ease;

	private Transform fireRayTf;
	//private Transform player;
	private float distanceFromPlayer;
	private Quaternion rotation;
	

	private bool fireRayDispo = true;
	private bool pawPunching;

	[HideInInspector]
	public bool playerTooFar = false;

	// Use this for initialization
	void Start () 
	{
		DOTween.Init();
		//player = GetComponent<BossAttack>().player;
	}
		
	void Update () // Vérifie si le joueur est assez pret et qu'aucune autre attaque est en cours
	{
		distanceFromPlayer = GetComponent<BossAttack>().distanceFromPlayer;
		pawPunching = GetComponent<PawPunch>().pawPunching;

		if(pawPunching == false)
		{
			if(minDistance < distanceFromPlayer && distanceFromPlayer < maxDistance && fireRayDispo == true)
			{
				fireRayDispo = false;
				playerTooFar = false;
				StartCoroutine("Attack");
			}
		}


		if(distanceFromPlayer > (maxDistance + 0.5f) && fireRayDispo == false && playerTooFar == false) // Arrete l'attaque quand le joueur est trop loin
		{
			playerTooFar = true;
			StartCoroutine("DestroyAndCoolDown");
		}
	}

	IEnumerator Attack () // Instantie et modifie la taille du rayon
	{
		if(playerTooFar == false)
		{
			GetComponent<BossAttack>().rotationSpeed = 0.05f;
			fireRayTf = (Instantiate(fireRay, fireRaySpawn.position, transform.rotation) as GameObject).GetComponent<Transform>();
			fireRayTf.SetParent(transform);
			fireRayTf.DOScale(1, 0);
			fireRayTf.GetChild(0).DOScale(new Vector3(0.178734f, 1.87f, 0f), timeDuration).SetEase(ease);
			yield return new WaitForSeconds(fireRayDuration);
			StartCoroutine("DestroyAndCoolDown");
		}

		yield break;
	}

	IEnumerator DestroyAndCoolDown () // Détruit le rayon et lance le cooldown
	{
		if(fireRayTf)
		{
			fireRayTf.GetChild(0).DOScale(new Vector3(0.1f, 0.1f, 0f), timeDuration).SetEase(ease);
			yield return new WaitForSeconds(timeDuration);
			Destroy(fireRayTf.gameObject);
			GetComponent<BossAttack>().rotationSpeed = 0.2f;
			yield return new WaitForSeconds(fireRayCoolDown);
			fireRayDispo = true;
		}

	}
}
