using UnityEngine;
using UnityEngine.SceneManagement;

public class clotheSelection : MonoBehaviour
{
    public void OnDetectiveClick(){
        GameData.undercover = false;
        SceneManager.LoadScene("BarScene");
    }

    public void OnUndercoverClick(){
        GameData.undercover = true;
        SceneManager.LoadScene("BarScene");
    }

    public static class GameData
    {
        public static bool undercover = false;
    }

}
