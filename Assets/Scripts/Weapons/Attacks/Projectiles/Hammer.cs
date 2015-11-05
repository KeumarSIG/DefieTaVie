using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour 
{
    public float rotation;
    public float maxDistance; // max distance the hammer can travel;
    
    [HideInInspector] public bool isBeingThrown;
    Vector3 originalPosition;
    Character hero;

    void Awake()
    {
        hero = GameObject.Find("Char").GetComponent<Character>();
        isBeingThrown = true;
        originalPosition = this.transform.position;
    }
	
	void Update () 
    {
        if (isBeingThrown == true)
        {
            this.transform.Rotate(new Vector3(0.0f, 0.0f, Mathf.Sign(hero.lastMovement.magnitude) * rotation));

            if (Vector3.Distance(originalPosition, this.transform.position) > maxDistance)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
                isBeingThrown = false;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy") 
		{
			this.transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			isBeingThrown = false;
		}
    }
}
