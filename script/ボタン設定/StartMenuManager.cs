using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [Header("UI要素")]
    public InputField characterNameInput; // キャラクター名入力フィールド
    public InputField shopNameInput; // 店舗名入力フィールド
    public Button confirmButton; // 決定ボタン

    private void Start()
    {
        // 決定ボタンにリスナーを追加
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    // 決定ボタンが押された時の処理
    private void OnConfirmButtonClicked()
    {
        string characterName = characterNameInput.text.Trim();
        string shopName = shopNameInput.text.Trim();

        // 入力内容のチェック
        if (string.IsNullOrEmpty(characterName) || string.IsNullOrEmpty(shopName))
        {
            Debug.LogWarning("キャラクター名または店舗名が入力されていません！");
            return;
        }

        // データを保存 (PlayerPrefsを使用)
        PlayerPrefs.SetString("CharacterName", characterName);
        PlayerPrefs.SetString("ShopName", shopName);
        PlayerPrefs.Save();

        // ゲームシーンに移動
        SceneManager.LoadScene("倉庫");
    }
}
