using UnityEngine;
using System.Collections;

public class BreadAttack : MonoBehaviour 
{
	public GameObject m_bread;
    public GameObject breadTarget;

    public int numOfBread; // Number of miettes de pain created when the character breaks his baguette
	public int breadToThrow; // Number of miettes de pain the character has

    int minPiecesToThrow;

    SwordAttack swordAttack;

    void Start ()
    {
        swordAttack = GetComponent<SwordAttack>();
        minPiecesToThrow = 5;
    } 

	void Update () 
	{
		if (breadToThrow > 0) // throw miettes de pain
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				breadToThrow--;
                minPiecesToThrow--;

				GameObject clone = Instantiate(m_bread, this.transform.position, Quaternion.identity) as GameObject;
				clone.GetComponent<Bread>().moving = false;

                clone.transform.LookAt(breadTarget.transform.position);
                clone.GetComponent<Bread>().gettingThrown = true;
                clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.forward * 500);

                if (minPiecesToThrow <= 0)
                {
                    swordAttack.breadCanAttack = true;
                    minPiecesToThrow = 5;
                }
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) // Get the "miettes de pain" (with a French accent)
	{
		if (other.gameObject.tag == "Bread")
		{
			if (other.GetComponent<Bread>().moving == false)
			{
				breadToThrow += 1;
				Destroy(other.gameObject, 0.01f);
			}
		}
	}

    public void createBreadPieces()
    {
        for (var i = 0; i < numOfBread; i++)
        {
            GameObject clone = Instantiate(m_bread, this.transform.position, Quaternion.identity) as GameObject;
            clone.GetComponent<Bread>().gettingThrown = false;
        }
    }
}