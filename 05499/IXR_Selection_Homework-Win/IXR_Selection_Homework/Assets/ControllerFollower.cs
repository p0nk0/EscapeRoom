using UnityEngine;

// the scope logic was heavily inspired by https://www.youtube.com/watch?v=9g2VqJvWnQI
// added another camera, a render texture, and a "lens" with that render textuere as material

public class ControllerFollower : MonoBehaviour
{
    public Transform controller;
    public Vector3 positionOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = controller.position + positionOffset;
        transform.rotation = controller.rotation;
    }
}
