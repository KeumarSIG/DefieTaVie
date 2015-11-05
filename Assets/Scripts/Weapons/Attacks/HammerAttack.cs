using UnityEngine;
using System.Collections;

public class HammerAttack : MonoBehaviour
{
    private bool isEquipped;        // Char has the hammer equipped
    private bool isThrown;          // The hammer is being thrown

    public GameObject hammer;       // The hammer
    public GameObject clone;        // Hammer clone

    public float distAttack;        // Little div necessary to adjust attack distance
    private Vector2 lastMovement;   // last char direction

    public float arrowSpeed;        // speed of the arrow
    private float OriginalHammerSpeed;        // speed of the arrow

    public float timeNoMove;        // Time to make sure we cannot move

    Character hero;                 // Script hero

    void Start()
    {
        isEquipped = true;

        OriginalHammerSpeed = arrowSpeed;
        lastMovement = new Vector2(0, -1);
        hero = GetComponent<Character>();
    }

    void OnTriggerEnter2D(Collider2D other) // Get the hammer back
	{
        if (other.gameObject.tag == "Hammer")
        {
			if (other.GetComponent<Hammer>().isBeingThrown == false)
			{
            	isEquipped = true;
            	Destroy(other.gameObject, 0.1f);
			}
        }
    }

    void Update()
    {
        if (hero.movement.magnitude != 0) // Get the last dir (to attack in Idle mode before hero.movement == 0)
        {
            lastMovement = hero.movement;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isEquipped == true) // Char retrouve sa vitesse quand touche attaque relevée
        {
            isEquipped = false;
            ThrowHammer();
        }
    }

    // Hammer is thrown
    void ThrowHammer()
    {
        GetComponent<Character>().speed = GetComponent<Character>().originalSpeed;
        Attack(lastMovement / distAttack);
    }

    void Attack(Vector2 dir)
    {
        this.GetComponent<Character>().speed = 0;
        clone = (GameObject)Instantiate(hammer, (hero.rb.position + dir), this.transform.rotation);

        // Hammer's direction... I couldn't think of something shorter/easier/smarter
        if (lastMovement == new Vector2(1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90f); // RIGHT
        else if (lastMovement == new Vector2(-1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f); // LEFT
        else if (lastMovement == new Vector2(0, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f); // UP
        else if (lastMovement == new Vector2(0, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f); // DOWN
        else if (lastMovement == new Vector2(1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -45f); // UP RIGHT
        else if (lastMovement == new Vector2(-1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45f); // UP LEFT
        else if (lastMovement == new Vector2(1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -135f); // DOWN RIGHT
        else if (lastMovement == new Vector2(-1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 135f); // DOWN LEFT

        // Hammer's movement
        clone.GetComponent<Rigidbody2D>().velocity += lastMovement * arrowSpeed;
        arrowSpeed = OriginalHammerSpeed;

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