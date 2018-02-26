using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playermanager : MonoBehaviour 
{
    [readonlyAttribute]
    public int lives;
    public resources resources = new resources();

    public Text livesText;
    public Text resourcesText;

    public void initiate()
    {
        livesText = GameObject.Find("livestext").GetComponent<Text>();
        resourcesText = GameObject.Find("resourcestext").GetComponent<Text>();
    }

    public void updatePlayerUI()
    {
        if (livesText)
            livesText.text = "Lives: " + lives;
        if (resourcesText)
            resourcesText.text = "F: " + resources.fireResources + " A: " + resources.airResources + " E: " + resources.earthResources + " W: " + resources.waterResources;
    }
}
