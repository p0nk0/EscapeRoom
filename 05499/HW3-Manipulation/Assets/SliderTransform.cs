using UnityEngine;
using UnityEngine.InputSystem;

public class SliderTransform : MonoBehaviour
{
    public TransformationEvaluator evaluator;
    public InputActionReference primaryButtonPress;
    Transform sourceTransform;

    public enum Axis { X, Y, Z };
    public enum TransformType { Scale, Rotate, Translate };

    void ConfirmSelection(InputAction.CallbackContext context)
    {
        evaluator.ConfirmSelection();
    }

    public void UpdateTransform(float value, Axis axis, TransformType transformType)
    {

        if (transformType == TransformType.Scale)
        {
            Vector3 newScale = sourceTransform.localScale;
            newScale[(int)axis] = Mathf.Max(0.1f, value);
            sourceTransform.localScale = newScale;
        }
        else if (transformType == TransformType.Rotate)
        {
            Vector3 newRotation = sourceTransform.localEulerAngles;
            newRotation[(int)axis] = value;
            sourceTransform.localEulerAngles = newRotation;
        }
        else if (transformType == TransformType.Translate)
        {
            Vector3 newValue = sourceTransform.localPosition;
            newValue[(int)axis] = value;
            sourceTransform.localPosition = newValue;
        }
    }

    void Awake()
    {
        primaryButtonPress.action.performed += ConfirmSelection;
        sourceTransform = evaluator.GetSourceTransform();
    }
}
