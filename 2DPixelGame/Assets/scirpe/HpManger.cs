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

    public IEnumerator ShowDamage()
    {

    }
   

}
