using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletHell : MonoBehaviour 
{
	public GameObject bulletHell;
	public Transform bulletHellInstances;

	public float timeDuration;
	public float bulletCoolDown;
	public float minDistance;
	public float maxDistance;
	public Ease ease;

	private Transform bulletTf;
	private Transform player;
	private float distanceFromPlayer;

	private bool bulletDispo = true;
	private bool pawPunching;

	// Use this for initialization
	void Start () 
	{
		DOTween.Init();
		player = GetComponent<BossAttack>().player;
	}

	void Update () // Vérifie si le joueur est assez pret et qu'aucune autre attaque est en cours
	{
		distanceFromPlayer = GetComponent<BossAttack>().distanceFromPlayer;
		pawPunching = GetComponent<PawPunch>().pawPunching;

		if(transform.childCount == 3 && pawPunching == false)
		{
			if(minDistance < distanceFromPlayer && distanceFromPlayer < maxDistance && bulletDispo == true)
			{
				bulletDispo = false;
				StartCoroutine("Attack");
			}
		}


	}

	IEnumerator Attack () // L'attaque qui instantie des boules de feu
	{
		bulletTf = (Instantiate(bulletHell, transform.position, transform.rotation) as GameObject).GetComponent<Transform>();
		bulletTf.SetParent(bulletHellInstances);
		bulletTf.DOMove(player.position, timeDuration).SetEase(ease);
		bulletTf.rotation = Quaternion.AngleAxis(GetComponent<BossAttack>().lookRotation, Vector3.forward);
		yield return new WaitForSeconds(bulletCoolDown);
		bulletDispo = true;
	}
}
