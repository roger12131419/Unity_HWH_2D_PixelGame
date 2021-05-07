﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("追蹤範圍"), Range(0, 500)]
    public float rangeTrack = 2;
    [Header("攻擊範圍"), Range(0, 50)]
    public float rangeAttack = 0.5f;
    [Header("移動速度"), Range(0, 50)]
    public float speed = 2;
    [Header("攻擊特效")]
    public ParticleSystem psAttack;
    [Header("攻擊冷卻時間"), Range(0, 10)]
    public float cdAttack = 3;
    [Header("攻擊力"), Range(0, 1000)]
    public float attack = 20;
    [Header("經驗值"), Range(0, 500)]
    public float exp = 30;
    
    private Transform player;
    private player m_player;
    //[Header("角色是否死亡")]
    private bool isDead = false;
    /// <summary>
    ///  計時器
    /// </summary>
    private float timer;
   
    [Header("血量")]
    public float hp = 200;
    [Header("血調系統")]
    public HpManger hpManger;

    private float hpMax;

    private void Start()
    {
        hpMax = hp;   // 取得血量最大值
        
        // 玩家變形 = 尋找遊戲物件("物件名稱").變形
        player = GameObject.Find("玩家").transform;
        m_player = player.GetComponent<player>();
    }
   
    // 繪製圖示事件 : 在 Unity 內顯示輔助開發
    private void OnDrawGizmos()
    {
        //  先指定顏色在畫圖
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        // 繪製球體(中心點，半徑)
        Gizmos.DrawSphere(transform.position, rangeTrack);

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }

    private void Update()
    {
        Track();
    }

    /// <summary>
    /// 追蹤玩家
    /// <summary>
    private void Track()
    {
        if (isDead) return;
        
        // 距離 等於 三圍向量 的 距離(A 點，B點)
        float dis = Vector3.Distance(transform.position, player.position);

        // 如果 距離 小魚等魚 攻擊範圍 進入攻擊狀態
        // 否則 距離 小於等於 追蹤範圍 才開始追蹤
        if (dis <= rangeAttack)
        {
            Attack();
        }
        else if (dis <= rangeTrack)
        {
            // 物件 的 座標 更新為 三維向量 的 往前移動(物件 的 座標，目標 的座標，速度 * 一禎的時間)
          transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        timer += Time.deltaTime;    // 累加時間
        // 如果 計時器 大於等於 冷卻時間 就攻擊
        if (timer >= cdAttack)
        {
            timer = 0;                  // 計時器 歸零   
            psAttack.Play();            // 播放 攻擊特效

            // 2D 碰撞 = 2D 物理.覆蓋圓形範圍(中心點，半圓，圖層)
            Collider2D hit = Physics2D.OverlapCircle(transform.position, rangeAttack, 1 << 9);
            // 碰到的物件 取得元件<玩家>().受傷(攻擊力)
            hit.GetComponent<player>().Hit(attack);
        }
 
       
    }
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害值</param>
    public void Hit(float damage)
    {
        hp -= damage;                                  // 扣除傷害值
        hpManger.UpdateHpBar(hp, hpMax);               // 更新血量
        StartCoroutine(hpManger.ShowDamage(damage));   // 啟動協同程序(顯示傷害值())

        if (hp <= 0) Dead();                           // 如果 血量 <= 0 就死亡    
    }

    /// <summary>
    ///  死亡
    /// </summary>
    private void Dead()
    {
        hp = 0;
        isDead = true;
        Destroy(gameObject, 1.5f);
    }
}

