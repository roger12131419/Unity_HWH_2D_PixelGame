using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    [Header("追蹤速度"), Range(0, 50)]
    public float speed = 1.5f;
    [Header("上下邊界")]
    public Vector2 limitY = new Vector2(-5, 5);
    [Header("左右邊界")]
    public Vector2 limitX = new Vector2(-5, 5);
    [Header("玩家")]
    public Transform player;
    
    private void Update()
    {
       Track();
    }
    
    /// <summary>
    /// 追蹤
    /// </summary>
    
    private void Track()
    {
        Vector3 posCam = transform.position; //取得 攝影座標
        Vector3 posCla = player.position;    //取得 玩家座標
       
        posCam = Vector3.Lerp(posCam, posCla, 0.5f * speed * Time.deltaTime);
        posCam.z = -10;
        //Mathf.Clamp(要夾住值 ， 最小值 ， 最大值)
        
        posCam.x = Mathf.Clamp(posCam.x, limitX.x, limitX.y);
        posCam.y = Mathf.Clamp(posCam.y, limitY.x, limitY.y);
        
        transform.position = posCam;          //更新攝影座標


     }
}



 





