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

    public bool bread; // the bread is activated

    [HideInInspector]
    public bool breadCanAttack; // if the the baguette has not been destroyed, then it's true

    int breadAttacks; // current number of hits done with the bread
    public int maxHitWithBread; // maximum number of hits before the bread breaks

    [HideInInspector]
    public bool createBreadPieces; 

    Character hero;
    BreadAttack heroBread;



    void Start()
    { 
        bread = false;
        breadAttacks = 0;
        breadCanAttack = false;

        hero = GetComponent<Character>();
        heroBread = GetComponent<BreadAttack>();

        if (heroBread.isActiveAndEnabled == true)
        {
            breadCanAttack = true;
            bread = true; // if baguette = true, baguette = true... 
        }
    }



	void Update()
	{
        if (bread == true)
        {
            AttackButtonWithBread();
        }

        else
        {
            AttackButtonWithoutBread();
        }
	}



    void AttackButtonWithBread()
    {
        if (breadCanAttack == true)
        { 
            if (Input.GetKeyDown(KeyCode.Space) && !clone) // Attaque sans mouvement
            {
                Attack(hero.lastMovement / distAttack);

                breadAttacks++;

                if (breadAttacks >= maxHitWithBread)
                {
                    breadCanAttack = false; // You have to throw the bread now cause baguette broke
                    heroBread.createBreadPieces();
                    breadAttacks = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && !clone) // Char retrouve sa vitesse quand touche attaque relevée
            {
                hero.speed = hero.originalSpeed;
            }
        }
    }



    void AttackButtonWithoutBread()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !clone) // Attaque sans mouvement
        {
            Attack(hero.lastMovement / distAttack);
        }

        if (Input.GetKeyUp(KeyCode.Space) && !clone) // Char retrouve sa vitesse quand touche attaque relevée
        {
            hero.speed = hero.originalSpeed;
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