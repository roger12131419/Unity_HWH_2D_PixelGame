﻿using UnityEngine;

public class player : MonoBehaviour
{
   //註解 
   
   //欄位語法
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
   [Header("移動速度"),Range(0, 300)]
   public float speed = 10.5f;
   public bool isDead = false;
   [Tooltip("這是角色的名稱")]
   public string cName = "貓咪";
}

