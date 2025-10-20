using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// raycast logic modified from https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Physics.Raycast.html
// glow logic from lots of googling and chatGPT

public class Pointing : MonoBehaviour
{

    public InputActionReference triggerPress;
    public SelectionEvaluator selectionEvaluator;
    public Transform controller;
    public LineRenderer lineRenderer;

    Transform selected;

    // Color prevColor;

    public float glowIntensity;

    Color glowColor;

    void Update()
    {
        Ray ray = new Ray(controller.position, controller.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100.0f);

        Vector3 endPoint = controller.position + controller.forward * 100.0f;

        lineRenderer.SetPosition(0, controller.position);
        lineRenderer.SetPosition(1, endPoint);

        hits = System.Array.FindAll(hits, hit => hit.transform.CompareTag("Sphere"));

        if (hits.Length > 0)
        {
            RaycastHit closestHit = hits[0];
            foreach (RaycastHit hit in hits)
            {


                if (hit.distance < closestHit.distance)
                {
                    closestHit = hit;
                }
            }

            Transform target = closestHit.transform;

            if (selected != target)
            {
                if (selected)
                {
                    // selected.GetComponent<Renderer>().material.color = prevColor;
                    selected.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

                }
                selected = target;
                // prevColor = selected.GetComponent<Renderer>().material.color;
                // selected.GetComponent<Renderer>().material.color = Color.magenta;
                selected.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                selected.GetComponent<Renderer>().material.SetColor("_EmissionColor", glowColor);
                selectionEvaluator.SetSelection(selected);
            }
        }
        else
        {
            if (selected)
            {
                // selected.GetComponent<Renderer>().material.color = prevColor;
                selected.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                selected = null;
            }
        }

    }

    void ConfirmSelection(InputAction.CallbackContext context)
    {
        if (!selected)
        {
            return;
        }
        // selected.GetComponent<Renderer>().material.color = prevColor;
        selected.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        selected = null;
        selectionEvaluator.ConfirmSelection();
    }

    void Awake()
    {
        triggerPress.action.performed += ConfirmSelection;
        glowColor = Color.magenta * glowIntensity;
    }
}
