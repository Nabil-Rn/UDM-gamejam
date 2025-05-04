using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public GameObject fadeScreenIn;

    //Replace this with the name of your image
    public GameObject FirstImage;
    public GameObject SecondImage;
    public GameObject TextBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EventStarter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //This function is called when the script is enabled, it's an iterator
    //Thus it waits.
    IEnumerator  EventStarter()
    {
        //Wait 2 seconds, fate screen on
        yield return  new WaitForSeconds(3);
        fadeScreenIn.SetActive(false);
        FirstImage.SetActive(true);
        //Wait 2 seconds, Dialogue
        yield return new WaitForSeconds(2);
        TextBox.SetActive(true);
        yield return new WaitForSeconds(2);
        SecondImage.SetActive(true);

    }

}
