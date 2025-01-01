// using UnityEngine;
// using System.Collections.Generic;
// using UnityEngine.UI;
// using TMPro;


// public class Chinretu_Tana : MonoBehaviour
// {
//     public int currentShelfLimit = 1;  // 初期の陳列棚の上限
//     public int maxShelfLimit = 10;    // 最大の陳列棚の上限
//     public Button IncreaseShelfLimitButton; // 陳列棚の上限を増やすボタン

//     private void Start()
//     {
//         // 陳列棚アイテム追加時に上限チェックを行う
//         AddToShelfButton.onClick.AddListener(() => AddItemToShelf(3, 1));

//         // 陳列棚の上限を増やすボタンのクリックイベント
//         IncreaseShelfLimitButton.onClick.AddListener(IncreaseShelfLimit);
//     }

//     private void AddItemToShelf(int itemID, int quantity)
//     {
//         if (陳列棚アイテムリスト.Count >= currentShelfLimit)
//         {
//             Debug.LogWarning("陳列棚が上限に達しています。");
//             return;
//         }


//         AddItemToList(陳列棚アイテムリスト, itemID, quantity);
//     }

//     private void IncreaseShelfLimit()
//     {
//         if (currentShelfLimit < maxShelfLimit)
//         {
//             currentShelfLimit++;
//             Debug.Log($"陳列棚の上限が増加しました。現在の上限: {currentShelfLimit}");
//         }
//         else
//         {
//             Debug.LogWarning("陳列棚の上限はすでに最大値です。");
//         }
//     }
// }