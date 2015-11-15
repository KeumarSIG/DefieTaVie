using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public float timeBeforeDeath;
    Vector2 arrowSpeed;
    public GameObject hero;
    [HideInInspector] public int bonusDmg;

    void Awake()
    {
        bonusDmg = hero.GetComponent<BowAttack>().bonusArrowDamage;
		this.GetComponent<HeroDamage>().damage += bonusDmg;
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