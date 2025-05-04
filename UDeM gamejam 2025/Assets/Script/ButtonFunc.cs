using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunc : MonoBehaviour
{
    public string newGameLevel; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(newGameLevel);
    }
}