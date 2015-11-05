using UnityEngine;
using System.Collections;

public class bread : MonoBehaviour 
{
	public float breadSpd;
	public float timeBeforeStop;

	[HideInInspector] public bool gettingThrown;
	
	public float minRotation;
	public float maxRotation;
	private float rotation;
	private float randomSign;

	public GameObject target;
	private Vector3 targetPos;

	private Vector2 rndDir;
	public Rigidbody2D rb;

	[HideInInspector] public bool moving;
	
	void Awake () 
	{		
		randomSign = Random.value;
		if (randomSign <= 0.5) randomSign = -1;
		else randomSign = 1;

		moving = true;

		rb = GetComponent<Rigidbody2D>();
		rndDir = new Vector2(Random.Range (-breadSpd, breadSpd), Random.Range (-breadSpd, breadSpd));

		rotation = Random.Range (minRotation, maxRotation) * randomSign;

		StartCoroutine(StopBread());

	}

	void FixedUpdate () 
	{
		if (moving == true)
		{
			rb.velocity = rndDir;
			this.transform.Rotate(new Vector3(0, 0, rotation));
		}

		if (this.transform.eulerAngles.y != 0 || this.transform.eulerAngles.x != 0) this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z);
	}

	IEnumerator StopBread()
	{
		yield return new WaitForSeconds(timeBeforeStop);
		moving = false;
		rb.velocity = new Vector2(0, 0);
		this.transform.Rotate(new Vector3(0, 0, 0));
		gettingThrown = false;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			print ("collision");
			Destroy(this.gameObject, 0.01f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			print ("collision");
			Destroy(this.gameObject, 0.01f);
		}
	}
}