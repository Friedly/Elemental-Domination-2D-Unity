using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class followtarget : MonoBehaviour 
{
    public Canvas worldSpaceCanvas;
    public Transform targetToFollow;
    public Vector3 offset;

    public void Update() 
    {
        if (!worldSpaceCanvas)
        {
            worldSpaceCanvas = GameObject.Find("worldinterface").GetComponent<Canvas>();
        }

        if (worldSpaceCanvas && targetToFollow && targetToFollow.gameObject.activeInHierarchy)
        {
            RectTransform CanvasRect = worldSpaceCanvas.GetComponent<RectTransform>();

            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint((targetToFollow.position + offset));

            Vector2 WorldObject_ScreenPosition = new Vector2(
                    ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                    ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))
                );

            RectTransform slider = this.gameObject.GetComponent<RectTransform>();
            slider.anchoredPosition = WorldObject_ScreenPosition;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
	}
}
