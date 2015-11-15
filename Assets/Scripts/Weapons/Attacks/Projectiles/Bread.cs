using UnityEngine;
using System.Collections;

public class Bread : MonoBehaviour 
{
	public float breadSpd;
	public float timeBeforeStop;

	[HideInInspector] public bool gettingThrown;
	
	public float minRotation;
	public float maxRotation;
	private float rotation;
	private float randomSign;

	public GameObject target;

	private Vector2 rndDir;
	public Rigidbody2D rb;

	[HideInInspector] public bool moving;
	
	void Awake () 
	{
        gettingThrown = false;

        // Throws the bread pieces in random directions
		randomSign = Random.value;
		if (randomSign <= 0.5) randomSign = -1;
		else randomSign = 1;

		moving = true;

		rb = GetComponent<Rigidbody2D>();
		rndDir = new Vector2(Random.Range (-breadSpd, breadSpd), Random.Range (-breadSpd, breadSpd));

		rotation = Random.Range (minRotation, maxRotation) * randomSign;

		StartCoroutine(StopBread()); // Prevents the bread from moving forever
	}

	void FixedUpdate () 
	{
		if (moving == true)
		{
			rb.velocity = rndDir;
			this.transform.Rotate(new Vector3(0, 0, rotation));
		}

        if (transform.eulerAngles.y != 0 || transform.eulerAngles.x != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z); // Sans ça, cela ne marche plus mais... WTF cette ligne de code ?
        }
	}

	IEnumerator StopBread()
	{
		yield return new WaitForSeconds(timeBeforeStop);
		moving = false;
		rb.velocity = new Vector2(0, 0);
		this.transform.Rotate(new Vector3(0, 0, 0));
		gettingThrown = false;
	}

    //
    // Je n'arrive pas à faire marcher cette merde.
    //
    /*
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			rb.velocity = new Vector2(0, 0);
			print ("Collision");
			Destroy(this.gameObject, 0.1f);
		}
	}
    */
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			rb.velocity = new Vector2(0, 0);
			print ("Trigger");
			Destroy(this.gameObject, 0.1f);
		}
	}
}