using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public float HP = 100f;
    public float damageInterval = 2f;
    public float hurtForce = 10f;
    public float damageAmout = 10f;

    private SpriteRenderer healthBar;   // 血条
    private float lastHurtTime;         // 受伤时刻          
    private Vector3 healthScale;        // 血条比例，控制长度
    private PlayerControl playerControl;// 控制脚本
    private Rigidbody2D hero;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        healthScale = healthBar.transform.localScale;
        hero = GetComponent<Rigidbody2D>();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - HP * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * HP * 0.01f, healthScale.y, healthScale.z);
    }



void TakeDamage(Transform enemy)
    {
        playerControl.jump = false;// 让玩家无法跳跃
        // 创建一个敌人把玩家向上推的向量
        Vector3 hurtVector3 = transform.position - enemy.position + Vector3.up * 5f;
        // 为玩家添加刚体力，使用上一步的向量
        hero.AddForce(hurtVector3 * hurtForce);
        // 减少玩家的生命值
        HP -= damageAmout;
        UpdateHealthBar();
    }
    void Death()
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); //注意，加s
        foreach (Collider2D c in colliders)
        {
            c.isTrigger = true;
        }

        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }
        GetComponent<PlayerControl>().enabled = false;
        GetComponentInChildren<Gun>().enabled = false;
        // 触发死亡动画
        anim.SetTrigger("Die");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Time.time > lastHurtTime + damageInterval)
            {
                if (HP > 0f)
                {
                    TakeDamage(collision.transform);
                    lastHurtTime = Time.time;
                    if (HP <= 0f)
                        Death();
                }
                else
                {
                    Death();
                }
            }
        }
    }
}
