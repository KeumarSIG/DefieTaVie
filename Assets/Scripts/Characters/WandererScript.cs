using UnityEngine;
using System.Collections;
using DG.Tweening;

public class WandererScript : MonoBehaviour 
{
	
	public float distanceMax;
	public float distanceMin;
	public float exactDistance;
	public float marginDistance;

	public bool randomDistance;

	public float waitTime;
	
	public float speed;

	private float movementDuration;
	private Vector2 pathPoint;
	private Vector2 movement;
	//private RaycastHit2D hit;

	// Use this for initialization
	void Start () 
	{
		DOTween.Init();
		FindPath ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	void FindPath ()
	{
		if(randomDistance == true)
		{
			pathPoint.x = Random.Range(transform.position.x - distanceMax, transform.position.x + distanceMax);
			pathPoint.y = Random.Range(transform.position.y - distanceMax, transform.position.y + distanceMax);

			//Debug.Log(pathPoint);

			Vector3 vv2 = pathPoint;
			vv2 -= transform.position;
			vv2.z = 0;

			int layerMask = 1 << 8;

			RaycastHit2D hit = Physics2D.Raycast(transform.position, vv2, Vector2.Distance(transform.position, pathPoint) + 0.5f, layerMask);

			if(hit.collider && hit.collider.tag == "Wall")
			{
				Debug.DrawRay(transform.position, vv2, Color.red, 3);
				//print ("Wall Ray");
				FindPath ();

			}
			else
			{
				if(Vector2.Distance(transform.position, pathPoint) > distanceMin)
				{
					movementDuration = (Vector2.Distance(transform.position, pathPoint)) / speed;
					transform.DOMove(pathPoint, movementDuration).OnComplete(CallWait).SetId("pathTween").SetEase(Ease.Linear);
				}
				else
				{
					FindPath ();
				}
			}


		}
		else
		{
			pathPoint.x = Random.Range(transform.position.x - distanceMax, transform.position.x + distanceMax);
			pathPoint.y = Random.Range(transform.position.y - distanceMax, transform.position.y + distanceMax);

			//Debug.Log(pathPoint);

			Vector3 vv2 = pathPoint;
			vv2 -= transform.position;
			
			int layerMask = 1 << 8;
			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, vv2, Vector2.Distance(transform.position, pathPoint) + 0.5f, layerMask);
			
			if(hit.collider && hit.collider.tag == "Wall")
			{
				Debug.DrawRay(transform.position, vv2, Color.red, 3);
				//print ("Wall Ray");
				FindPath ();
				
			}
			else
			{
				if(exactDistance - marginDistance < Vector2.Distance(transform.position, pathPoint) && Vector2.Distance(transform.position, pathPoint) < exactDistance + marginDistance)
				{
					Debug.Log (Vector2.Distance(transform.position, pathPoint));
					movementDuration = (Vector2.Distance(transform.position, pathPoint)) / speed;
					transform.DOMove(pathPoint, movementDuration).OnComplete(CallWait).SetId("pathTween").SetEase(Ease.Linear);
				}
				else
				{
					FindPath ();
				}
			}

		}

	}

	void CallWait ()
	{
		StartCoroutine("Wait");
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds(waitTime);
		FindPath ();
	}
}
