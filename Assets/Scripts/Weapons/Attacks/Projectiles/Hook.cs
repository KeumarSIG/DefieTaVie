using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour 
{
    public float timerBeforeDeath;
	public GameObject target;
	Character charScript;



    // Finds the player
	void Start()
	{
		charScript = target.GetComponent<Character>();
	}



    // Time before the hook dies.
    void Update()
    {
        timerBeforeDeath--;
        if (timerBeforeDeath <= 0) Destroy(gameObject);
    }



    // If the hook hits something
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Wall")
		{
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			charScript.fishingRope = true;
            charScript.fishingRopeTarget = this.gameObject;
			Destroy(this.gameObject, 2f);
		}
	}



    // On hook's destruction
	void OnDestroy()
	{
		charScript.fishingRope = false;
        charScript.fishingRopeTarget = null;
    }
}