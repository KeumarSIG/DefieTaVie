using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public float timeBeforeDeath;
	public int bonusDamage;
    Vector2 arrowSpeed;

    void Awake()
    {
		print ("Bonus: " + bonusDamage);
		this.GetComponent<HeroDamage>().damage += bonusDamage;
		Destroy(this.gameObject, timeBeforeDeath);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject, 0.05f);
        }
    }
}