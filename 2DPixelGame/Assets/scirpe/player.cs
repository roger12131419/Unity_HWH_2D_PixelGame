using UnityEngine;
using UnityEngine.UI; // 引用 介面 API
public class player : MonoBehaviour
{
    //註解 

    //欄位語法 Field - 儲存資料
    //修飾詞 類型 名稱 (指定 值);
    //公開 public 顯示
    //私人 private 不顯示 (預設值)

    //類型 四大類型
    //整數   int
    //浮點數 float
    //布林值 bool true 是、false 否
    //字串   string
    [Header("等級")]
    [Tooltip("這是角色的等級")]
    public int lv = 1;
    [Header("移動速度"), Range(0, 300)]
    public float speed = 10.5f;
    public bool isDead = false;
    [Tooltip("這是角色的名稱")]
    public string cName = "貓咪";
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("攻擊音效")]
    public AudioClip soundAttack;
    [Header("血量")]
    public float hp = 200;
    [Header("血調系統")]
    public HpManger hpManger;

    private float hpMax;
    
    // 事件:繪製圖示
    private void OnDrawGizmos()
    {
        // 指定圖示顏色 (紅，綠，藍，透明)
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        // 繪製圖示 球體(中心點，半徑)
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }

    // 方位語法 Method 儲存複雜的程式區塊或演算法
    // 修飾詞 類型 名稱 () [程式區塊或演算法]
    // void 無類型

    /// <summary>
    /// 移動
    /// </summary>
    private void Move() 
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        //變形元件.位移(水平 * 速度 * 一禎的時間，垂直 * 速度 * 一禎的時間,0)
        tra.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime , 0);

        ani.SetFloat("水平", h);
        ani.SetFloat("垂直", v);
    }
    
    // 要被按鈕呼叫必須設定公開 public
    public void Attack()
    {
        //音效來源.撥放一次(音效片段，音量)
        aud.PlayOneShot(soundAttack, 0.7f);

        // 2D 物理 圓形碰撞(中心點，半徑，方向，距離，圖層編號)
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rangeAttack, -transform.up, 0, 1 <<8);

        // 如果 碰到的物件存在 並且 碰到的物件 標籤 為 道具 就 取得道具腳本並呼叫掉落道具方法
        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<ltem>().DropProp();
    }
    
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害值</param>
    private void Hit(float damage)
    {
        hp -= damage;                            // 扣除傷害值
        hpManger.UpdateHpBar(hp, hpMax);         // 更新血量
    }
    private void Dead()
    {

    }
    // 事件 - 特定時間會執行的方法
    // 事件開始 : 播放後執行一次
    private void start() 
    {
        
    }
    // 更新事件 : 大約一秒執行六十次 60FPS
    private void Update()
    {
        // 呼叫方法
        // 方法名稱
        Move();
    }

    [Header("吃金條音效")]
    public AudioClip soundEat;
    [Header("金幣數量")]
    public Text textCoin;

    private int coin;
    //觸發事件 - 進入 : 兩個物件必須有一個勾選 Is Trigeer 
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "金條")
       {
            coin++;
            aud.PlayOneShot(soundEat);
            Destroy(collision.gameObject);
            textCoin.text = "金幣 : " + coin;
       }
    }
}

    

