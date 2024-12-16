using UnityEngine;
using UnityEngine.SceneManagement;

public class TownMapManager : MonoBehaviour
{
    public void OpenWorkSpace()
    {
        Debug.Log("作業所が開きます");
        // SceneManager.LoadScene("ShopScene"); // シーン遷移のサンプル
    }

    public void OpenWarehouse()
    {
        Debug.Log("倉庫が開きます");
        SceneManager.LoadScene("倉庫");
    }

    public void OpenCity()
    {
        SceneManager.LoadScene("街");
    }
}
