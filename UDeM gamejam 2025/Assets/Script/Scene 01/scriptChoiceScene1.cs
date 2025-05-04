using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scriptChoiceScene1 : MonoBehaviour
{
    public Dialogue script;
    public GameObject fadeScreenIn;

    //Replace this with the name of your image
    public GameObject FirstImage;
    public GameObject SecondImage;
    public GameObject TextBox;
    public GameObject ScreenDim;	
    public GameObject fadeScreenOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Coroutine Works?");
        TextBox.SetActive(false);
        SecondImage.SetActive(false);
        FirstImage.SetActive(false);
        fadeScreenIn.SetActive(true);
        ScreenDim.SetActive(false);
        fadeScreenOut.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //This function is called when the script is enabled, it's an iterator
    //Thus it waits.



}
