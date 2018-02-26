using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour 
{
    public int scrollSpeed;
    public int border;

    private float _minimumPositionX;
    private float _minimumPositionY;
    private float _maximumPositionX;
    private float _maximumPositionY;

    public void Start()
    {
        gamemanager.instance.initiate();
    }

    public void Update()
    {
        float verticalExtent = Camera.main.orthographicSize; ;
        float horizontalExtent = verticalExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        _minimumPositionX = horizontalExtent - 0.5f;
        _maximumPositionX = gamemanager.instance.mapSize - horizontalExtent - 0.5f;
        _minimumPositionY = verticalExtent - 0.5f;
        _maximumPositionY = gamemanager.instance.mapSize - verticalExtent - 0.5f;

        Vector3 cameraPosition = Camera.main.transform.position;

        if (Input.mousePosition.x > Screen.width - border || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            cameraPosition.x += scrollSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x < 0 + border || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            cameraPosition.x -= scrollSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - border || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            cameraPosition.y += scrollSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y < 0 + border || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            cameraPosition.y -= scrollSpeed * Time.deltaTime;
        }

        Camera.main.transform.position = cameraPosition;
    }

    public void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, _minimumPositionX, _maximumPositionX);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, _minimumPositionY, _maximumPositionY);
        transform.position = cameraPosition;
    }
}
