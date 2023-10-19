using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    //public stats
    public float Spd, InitHP, Dmg, AtkCooldown, BullSpd, BullDmg, BullTravelTime;
    public int DmgLvl, SpdLvl, IQ;

    //private stats
    protected float HP, cooldownTimer, bullXDir, bullYDir;

    //public components
    public Transform firePoint;
    public GameObject bulletPrefab;

    //private components
    PlayerController host; //null if !possessed
    Rigidbody2D rb;
    GameObject pl;

    void Start()
    {
        cooldownTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        HP = InitHP;
        pl = GameObject.FindWithTag("Player");
        host = pl.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (cooldownTimer <= 0)
        {
            GameObject go = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(rb.velocity.x, rb.velocity.y, 0));
            BulletPrefabScript bullet = go.GetComponent<BulletPrefabScript>();
            bullet.parent = rb;
            bullet.Spd = BullSpd;
            bullet.Dmg = BullDmg;
            bullet.MaxTravelTime = BullTravelTime;
            bullet.XDir = bullXDir;
            bullet.YDir = bullYDir;
            cooldownTimer = AtkCooldown;
        }
    }

    public void Hurt(float damage)
    {
        HP -= damage;

        Debug.Log("Ow, hurt for " + damage + " damage! HP Remaing: " + HP);

        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaceFirepoint(float xdir, float ydir)
    {
        bullXDir = xdir;
        bullYDir = ydir;
        firePoint.localPosition = new Vector3(xdir * 0.5f, ydir * 0.5f, firePoint.localPosition.z);
    }
}