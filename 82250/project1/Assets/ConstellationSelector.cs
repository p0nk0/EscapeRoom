using UnityEngine;

public class ConstellationSelector : MonoBehaviour
{
    public GameObject[] Constellations;

    public void SelectConstellation(int index)
    {
        for (int i = 0; i < Constellations.Length; i++)
        {
            Constellations[i].SetActive(i == index);
        }
    }
}
