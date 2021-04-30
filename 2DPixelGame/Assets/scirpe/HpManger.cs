using UnityEngine;
using UnityEngine.UI;

public class HpManger : MonoBehaviour
{
    [Header("血條")]
    public Image bar;

    /// <summary>
    /// 輸入血量與血量最大值並更新血條
    /// </summary>
    /// <param name="hp">當血向</param>
    /// <param name="hpMax">血量最大值</param>
    public void UpdateHpBar(float hp, float hpMax)
    {
        bar.fillAmount = hp / hpMax;
    }
   

}
