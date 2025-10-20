using UnityEngine;
using UnityEngine.UI;
public class SliderTransformCaller : MonoBehaviour
{
    public SliderTransform sliderTransform;
    public SliderTransform.Axis axis;
    public SliderTransform.TransformType transformType;


    public void OnSliderChanged(float value)
    {
        sliderTransform.UpdateTransform(value, axis, transformType);
    }
}
