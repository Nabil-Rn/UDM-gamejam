using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CatMiniGame : MonoBehaviour
{
    public Button catButton;
    public Button cheeseButton;
    public Button wineButton;
    public Button fishButton;

    private string selectedFood = "";

    private Color normalColor = Color.white;
    private Color highlightColor = Color.green;
    private Color wrongColor = Color.red;

    public void OnClickCheese()
    {
        selectedFood = "Cheese";
        ResetButtonColors();
        cheeseButton.image.color = highlightColor;

    }

    public void OnClickWine()
    {
        selectedFood = "Wine";
        ResetButtonColors();
        wineButton.image.color = highlightColor;
    }

    public void OnClickFish()
    {
        selectedFood = "Fish";
        ResetButtonColors();
        fishButton.image.color = highlightColor;
    }

    public void OnClickCat()
    {
        if (selectedFood == "Fish")
        {
            SceneManager.LoadScene("VillageScene");
        }
        else if (selectedFood == "Cheese")
        {
            StartCoroutine(ShakeAndRed(cheeseButton));
        }
        else if (selectedFood == "Wine")
        {
            StartCoroutine(ShakeAndRed(wineButton));
        }
    }


    private void ResetButtonColors()
    {
        cheeseButton.image.color = normalColor;
        wineButton.image.color = normalColor;
        fishButton.image.color = normalColor;
    }

    private IEnumerator ShakeAndRed(Button button)
    {
        button.image.color = wrongColor;

        Vector3 originalPos = button.transform.localPosition;
        float shakeAmount = 10f;
        float duration = 0.2f;

        for (float t = 0; t < duration; t += 0.05f)
        {
            float offset = Mathf.Sin(t * 50) * shakeAmount;
            button.transform.localPosition = originalPos + new Vector3(offset, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        button.transform.localPosition = originalPos;
        yield return new WaitForSeconds(0.2f);
        button.image.color = normalColor;
    }
}
