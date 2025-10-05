using Banchy;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraTransform;

    public void _Update()
    {
        cameraTransform.position = Vars.Instance.player.transform.position;
    }
}
