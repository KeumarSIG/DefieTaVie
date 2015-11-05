using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public float speed;
    public float originalSpeed;

    public int damage;

    Animator anim;

	[HideInInspector] public bool fishingRope;
	public GameObject fishingRopeTarget;

    [HideInInspector] public bool bumped = false;
    public Vector2 movement;
    public Rigidbody2D rb;
    public Vector2 lastMovement;



    // Init char variables
	void Start () 
    {
		fishingRope = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalSpeed = speed;
	}


	
	void FixedUpdate () 
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // Input.GetAxisRaw returns a BOOL → 0 or 1 ; not 0 to 0.1 to 0.2 to... 1.

        if (movement.magnitude != 0) // Get the last dir (to attack in Idle mode)
        {
            lastMovement = movement;
        }

        // Set animations
        if (movement != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement.x);
            anim.SetFloat("input_y", movement.y);
        }

        else
        {
            anim.SetBool("isWalking", false);
        }

        // Normal moving situation
        if (bumped == false) rb.MovePosition(rb.position + movement * speed * Time.deltaTime); // Moves if the player is not bumped

        //print("Fishing rope: " + fishingRope);
        //print("Fishing rope target: " + fishingRopeTarget);

        // If the player grabbed something with the fishing rope
        if (fishingRope == true) 
		{
			if (Vector3.Distance(this.transform.position, fishingRopeTarget.transform.position) <= 1f) 
			{
                this.rb.velocity = Vector2.zero;
				Destroy(fishingRopeTarget, 0.5f);
				fishingRope = false;
			}

			this.transform.position = Vector3.MoveTowards(this.transform.position, fishingRopeTarget.transform.position, speed * Time.deltaTime * 4);
		}
	}



    // If the player takes damages - Bump the player in opposite direction
    void OnCollisionEnter2D (Collision2D other) 
	{
		if (bumped == false) // If the character is not currently being bumped (gives invulnerability frames)
		{
			if (other.gameObject.tag == "Enemy") 
			{
				bumped = true;
				if (bumped == true) 
				{
					rb.AddForce (-movement * speed * 15);
					StartCoroutine (resetBump ());
				}
			}
		}
	}



    // Go back from taking damages to not taking damages
	IEnumerator resetBump()
	{
		yield return new WaitForSeconds (0.25f);
		bumped = false;
	}
}