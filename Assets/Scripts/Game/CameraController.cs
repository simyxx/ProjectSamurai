using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float targetOrthographicSize = 5f; // Zadávejte hodnotu podle potřeby

    void Start()
    {
        AdjustCameraSize();
    }

    void Update()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        Camera mainCamera = Camera.main;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenRatio = screenWidth / screenHeight;
        mainCamera.orthographicSize = targetOrthographicSize / screenRatio;
    }
}
