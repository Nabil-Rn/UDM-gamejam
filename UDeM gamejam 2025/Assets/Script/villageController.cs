using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnClickCatMiniGame()
    {
        SceneManager.LoadScene("CatMiniGame");
    }

    public void OnClickTalkToOldLady()
    {
        Debug.Log("Talking to the old lady...");
    }

    public void OnClickLeave()
    {
        SceneManager.LoadScene("BarScene");
    }
}
