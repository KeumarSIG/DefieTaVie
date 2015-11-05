using UnityEngine;
using System.Collections;

public class FishingRod : MonoBehaviour
{
    private float cooldown;
    public float cooldownTime;

    public GameObject hook;         // The hook
    private GameObject clone;        // hook clone

    public float distAttack;        // Little div necessary to adjust attack distance → should be a multiplication
    private Vector2 lastMovement;   // last char direction

    public float hookSpeed;         // speed of the hook

    public float timeNoMove;        // Time to make sure we cannot move

    Character hero;                 // Script hero



    // Init bow thing
    void Start()
    {
        lastMovement = new Vector2(0, -1);
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

        if (cooldown <= 0 && Input.GetKeyUp(KeyCode.Space)) // Throw the hook
        {
            ThrowHook(lastMovement, hook);
        }
    }



    void ThrowHook(Vector2 dir, GameObject objectBeingShot)
    {
        hero.fishingRopeTarget = clone;

        this.GetComponent<Character>().speed = 0;
        clone = Instantiate(objectBeingShot, (hero.rb.position + dir), this.transform.rotation) as GameObject;
        clone.GetComponent<Hook>().target = this.gameObject;

        // Hook's direction...
        if (lastMovement == new Vector2(1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90f); // RIGHT
        else if (lastMovement == new Vector2(-1, 0)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f); // LEFT
        else if (lastMovement == new Vector2(0, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f); // UP
        else if (lastMovement == new Vector2(0, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f); // DOWN
        else if (lastMovement == new Vector2(1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -45f); // UP RIGHT
        else if (lastMovement == new Vector2(-1, 1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45f); // UP LEFT
        else if (lastMovement == new Vector2(1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, -135f); // DOWN RIGHT
        else if (lastMovement == new Vector2(-1, -1)) clone.transform.eulerAngles = new Vector3(0.0f, 0.0f, 135f); // DOWN LEFT

       

        // Hook's movement
        clone.GetComponent<Rigidbody2D>().velocity += lastMovement * hookSpeed;

        // Get char speed back
        StartCoroutine(GetSpeedBack());

        cooldown = 60;
    }



    // Get char speed back
    IEnumerator GetSpeedBack()
    {
        yield return new WaitForSeconds(timeNoMove);
        GetComponent<Character>().speed = GetComponent<Character>().originalSpeed;
    }
}