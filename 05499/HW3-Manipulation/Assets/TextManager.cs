using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public Transform target;
    public TMP_Text x;
    public TMP_Text y;
    public TMP_Text z;

    public SliderTransform.TransformType transformType;

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 value = Vector3.zero;
        string format = "F2";

        switch (transformType)
        {
            case SliderTransform.TransformType.Translate:
                value = target.position;
                format = "F2";
                break;
            case SliderTransform.TransformType.Scale:
                value = target.localScale;
                format = "F2";
                break;
            case SliderTransform.TransformType.Rotate:
                value = target.eulerAngles;
                format = "F0";
                break;
        }

        x.text = value.x.ToString(format);
        y.text = value.y.ToString(format);
        z.text = value.z.ToString(format);
    }
}
