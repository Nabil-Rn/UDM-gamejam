using UnityEngine;
using UnityEngine.SceneManagement;

public class clotheSelection : MonoBehaviour
{
    public void OnDetectiveClick()
    {
        GameData.SetFlag("undercover", false);
        SceneManager.LoadScene("BarScene");
    }

    public void OnUndercoverClick()
    {
        GameData.SetFlag("undercover", true);
        SceneManager.LoadScene("BarScene");
    }
}
