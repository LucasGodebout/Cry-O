using UnityEngine;
using Unity.Cinemachine;

public class CameraGameStart : MonoBehaviour
{
    public CinemachineCamera startCam;
    public CinemachineCamera gameplayCam;
    public Transform player;

    Camera mainCam;
    bool switched = false;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (switched) return;

        Vector3 viewPos = mainCam.WorldToViewportPoint(player.position);

        if (viewPos.y >= 0.75f)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        switched = true;

        startCam.Priority = 0;
        gameplayCam.Priority = 20;
    }
}