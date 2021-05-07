using UnityEngine;
using UnityEngine.UI;
using System.Collections; // 引用 系統集合 API 裡面包含協同程序

public class HpManger : MonoBehaviour
{
    [Header("血條")]
    public Image bar;
    [Header("傷害數值")]
    public RectTransform rectDamage;

    /// <summary>
    /// 輸入血量與血量最大值並更新血條
    /// </summary>
    /// <param name="hp">當血向</param>
    /// <param name="hpMax">血量最大值</param>
    public void UpdateHpBar(float hp, float hpMax)
    {
        bar.fillAmount = hp / hpMax;
    }

    /// <summary>
    /// 顯示傷害值
    /// </summary>
     public IEnumerator ShowDamage(float damage)
    {
        RectTransform rect = Instantiate(rectDamage, transform);     // 生成傷害數值在血調系統內
        rect.anchoredPosition = new Vector2(0, 200);                 // 指定目標
        rect.GetComponent<Text>().text = damage.ToString();          // 更新數值

        float y = rect.anchoredPosition.y;                           // 取得原始 Y 軸
                

        while (y < 400)                                              // 當 Y 小於 400 時持續執行                    
        {
            y += 20;                                                 // 每次上升單位 20  
            rect.anchoredPosition = new Vector2(0, y);               // 更新座標
            yield return new WaitForSeconds(0.02f);                  // 等待幾秒(秒數)
        }

        Destroy(rect.gameObject, 0.5f);                                    // 刪除傷害物值物件
    }
   

}
