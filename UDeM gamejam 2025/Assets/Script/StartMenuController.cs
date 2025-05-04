using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public string startSceneName = "DetectiveCustomiaztion"; // The name of the scene to load
    public void OnStartClick(){
        SceneManager.LoadScene(startSceneName);
    }

    public void OnExitClick(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
