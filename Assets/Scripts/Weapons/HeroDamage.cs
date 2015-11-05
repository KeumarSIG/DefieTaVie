using UnityEngine;
using System.Collections;

public class HeroDamage : MonoBehaviour 
{	
	public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") // DAMAGE
        {
            var otherHealth = other.GetComponent<Enemy>().health;
            other.GetComponent<Enemy>().health -= damage;
            print("HP= " + otherHealth);
			if (this.gameObject.tag == "Arrow" || this.gameObject.tag == "Bread") Destroy (this.gameObject, 0.01f);
            if (otherHealth <= 0) Destroy(other.gameObject);
        }
    }  
}