using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScriptFisherMan : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject fadeScreenIn;

    //Replace this with the name of your image
    public GameObject FirstImage;
    public GameObject SecondImage;
    public GameObject TextBox;
    public GameObject ScreenDim;	
    public GameObject fadeScreenOut;
    public GameObject ThirdImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Coroutine Works?");
        StartCoroutine(EventStarter());
        TextBox.SetActive(false);
        SecondImage.SetActive(false);
        FirstImage.SetActive(false);
        ThirdImage.SetActive(false);
        fadeScreenIn.SetActive(true);
        ScreenDim.SetActive(false);
        fadeScreenOut.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.index == 3){
            FirstImage.SetActive(false);
            SecondImage.SetActive(true);
            
            
        };
        if (dialogue.index == 4){
            SecondImage.SetActive(false);
            ThirdImage.SetActive(true);
        };
        if (dialogue.index == 5){
            ThirdImage.SetActive(false);
            FirstImage.SetActive(true);
        };
    }
    //This function is called when the script is enabled, it's an iterator
    //Thus it waits.
    IEnumerator EventStarter()
    {
        //event 1 (whatever that means)
        //Wait 2 seconds, fate screen on
        Debug.Log("Coroutine Ws");
        yield return  new WaitForSeconds(4);
        fadeScreenIn.SetActive(false);
        //FirstImage.SetActive(true);
        //Wait 2 seconds, Dialogue
        FirstImage.SetActive(true);
        ScreenDim.SetActive(true);
        yield return new WaitForSeconds(1);
        TextBox.SetActive(true);
        Debug.Log("Coroutine Works");
        yield return new WaitForSeconds(2);
        //Wait till dialogus is finished
        yield return new WaitUntil(() => dialogue.isFinished);
        Debug.Log("dialogue finished");
        //Transition
        fadeScreenOut.SetActive(true);
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("CatMiniGame");
        


    }


}
