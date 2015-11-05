using UnityEngine;
using System.Collections;

public class BreadAttack : MonoBehaviour 
{
	public GameObject bread;
	public int numOfBread; // Number of miettes de pain created when the character breaks his baguette

	public int breadToThrow; // Number of miettes de pain the character has

	Character hero;
	//Vector2 lastMovement;
	GameObject clone;

	void Start()
	{
		hero = GetComponent<Character>();
	}

	void Update () 
	{
		//lastMovement = hero.movement;

		if (Input.GetKeyDown(KeyCode.E)) 
		{
			for (var i = 0 ; i < numOfBread ; i++)
			{
				clone = Instantiate(bread, this.transform.position, Quaternion.identity) as GameObject;
				clone.GetComponent<bread>().gettingThrown = false;
			}
		}

		if (breadToThrow > 0) // throw miettes de pain
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				breadToThrow--;
				clone = Instantiate(bread, this.transform.position, Quaternion.identity) as GameObject;
				clone.GetComponent<bread>().moving = false;


				GameObject target = GameObject.FindGameObjectWithTag("Enemy");
				clone.transform.LookAt(target.transform.position);
				clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.forward * 200);
			}
		}
		
	}

	void OnTriggerEnter2D(Collider2D other) // Get the "miettes de pain" (with a French accent)
	{
		if (other.gameObject.tag == "Bread")
		{
			if (other.GetComponent<bread>().moving == false)
			{
				breadToThrow += 1;
				Destroy(other.gameObject, 0.01f);
			}
		}
	}
}
;