using UnityEngine;
using System.Collections;

public class SwordAttack : MonoBehaviour
{
    public float timeNoMove;        // Duration of disabled movement
    public float durationOfHitbox;  // Time the sword hits
    public GameObject swordHitBox;  // The hitbox
	public GameObject clone;        // Hitbox created to hit

    public float distAttack;        // Little div necessary to adjust attack distance
    //private Vector2 lastMovement;

    Character hero;

    void Start()
    {
        hero = GetComponent<Character>();
    }

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space) && !clone ) // Attaque sans mouvement
        {
            Attack(this.GetComponent<Character>().lastMovement / distAttack);
        }

        if (Input.GetKeyUp(KeyCode.Space) && !clone) // Char retrouve sa vitesse quand touche attaque relevée
        {
            GetComponent<Character>().speed = GetComponent<Character>().originalSpeed;
        }
	}

    void Attack(Vector2 dir)
    {
        this.GetComponent<Character>().speed = 0;
        clone = (GameObject)Instantiate(swordHitBox, (hero.rb.position + dir), this.transform.rotation);
        Destroy(clone, durationOfHitbox);
        StartCoroutine(GetSpeedBack());
    }

    IEnumerator GetSpeedBack()
    {
        yield return new WaitForSeconds(timeNoMove);
        GetComponent<Character>().speed = GetComponent<Character>().originalSpeed;
    }
}