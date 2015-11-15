using UnityEngine;
using System.Collections;

public class BowAttack : MonoBehaviour
{
    private float cooldown;         // Time the sword hits
    public float cooldownTime;      

    public GameObject arrow;        // The arrow
    public GameObject clone;        // Arrow clone

    public float distAttack;        // Little div necessary to adjust attack distance
    private Vector2 lastMovement;   // last char direction

    public float arrowSpeed;        // speed of the arrow
    private float OriginalArrowSpeed;        // speed of the arrow

    public float timeNoMove;        // Time to make sure we cannot move

    public float charge;
    private int stack;

	public int bonusArrowDamage;

    Character hero;                 // Script hero



    // Init bow thing
    void Start()
    {
        hero = GetComponent<Character>();
        OriginalArrowSpeed = arrowSpeed;
        lastMovement = hero.movement;
        hero = GetComponent<Character>();
        cooldown = 0;
    }

    void Update()
    {
        if (hero.movement.magnitude != 0) // Get the last dir (to attack in Idle mode before hero.movement == 0)
        {
            lastMovement = hero.movement;
        }

        if (cooldown > 0) cooldown--; // Cooldown reset

        if (cooldown <= 0) // Attack
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (stack <= 15)
                {
                    charge++;

                    if (charge > 15) // Stacks damages and speed of the projectile
                    {
                        charge = 0;
                        arrowSpeed += 0.25f;
						bonusArrowDamage += 2;
                        stack += 1;
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) // Throw the arrow
        {
            ThrowArrow();
        }
    }

    // Arrow is shot
    void ThrowArrow()
    {
        // Reset character' stacks
        charge = 0;
        stack = 0;
        cooldown = cooldownTime;
        GetComponent<Character>().speed = GetComponent<Character>().originalSpeed;
        Attack(lastMovement / distAttack, arrow);
    }

    void Attack(Vector2 dir, GameObject objectBeingShot)
    {
        this.GetComponent<Character>().speed = 0;
		clone = Instantiate(objectBeingShot, (hero.rb.position + dir), this.transform.rotation) as GameObject;
		clone.GetComponent<HeroDamage>().damage += this.bonusArrowDamage;

        // Arrow's direction... I couldn't think of something shorter/easier/smarter
        if (lastMovement == new Vector2(1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90f); // RIGHT
        else if (lastMovement == new Vector2(-1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f); // LEFT
        else if (lastMovement == new Vector2(0, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f); // UP
        else if (lastMovement == new Vector2(0, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f); // DOWN
        else if (lastMovement == new Vector2(1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -45f); // UP RIGHT
        else if (lastMovement == new Vector2(-1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45f); // UP LEFT
        else if (lastMovement == new Vector2(1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -135f); // DOWN RIGHT
        else if (lastMovement == new Vector2(-1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 135f); // DOWN LEFT

        // Arrow's movement
        clone.GetComponent<Rigidbody2D>().velocity += lastMovement * arrowSpeed;
		bonusArrowDamage = 0;
        arrowSpeed = OriginalArrowSpeed;

        // Get char speed back
        StartCoroutine(GetSpeedBack());
    }

    // Get char speed back
    IEnumerator GetSpeedBack()
    {
        yield return new WaitForSeconds(timeNoMove);
        GetComponent<Character>().speed = GetComponent<Character>().originalSpeed;
    }
}