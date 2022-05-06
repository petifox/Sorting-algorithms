using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using SortingLibrary;
using UnityEngine.UI;
[RequireComponent(typeof(Visualizer))]
public class STBubble : MonoBehaviour
{
    public List<SortingItem> Columns => visualizer.Columns;
    public Visualizer visualizer;

    public Slider ResolutionSlider;
    public Slider SpeedSlider;

    private void OnEnable()
    {
        ResolutionSlider.value = visualizer.Length;
        ResolutionSlider.onValueChanged.AddListener(SetResolution);
        SpeedSlider.value = 1 - visualizer.WaitTime;
        SpeedSlider.onValueChanged.AddListener(SetSpeed);
        Sort();
    }
    [Button]
    public void Stop()
    {
        StopAllCoroutines();
        visualizer.Initialize();
        visualizer.StopSound();
    }
    public void SetSpeed(float v)
    {
        visualizer.WaitTime = 1 - v;
    }
    public void SetResolution(float v)
    {
        visualizer.Length = Mathf.RoundToInt(v);
        Stop();
    }
    [Button]
    public void Sort()
    {
        StartCoroutine(SortC());
    }
    public IEnumerator SortC()
    {
        visualizer.Initialize();
        visualizer.Shufle();

        yield return new WaitForSeconds(visualizer.WaitTime);

        bool swapped = true;

        while (swapped)
        {
            swapped = false;

            for (int i = 1; i < Columns.Count; i++)
            {
                visualizer.PlaySound(Columns[i].Height + 1);

                if (Columns[i - 1].Height.CompareTo(Columns[i].Height) > 0)
                {
                    visualizer.SetColor(i - 1, 1);
                    visualizer.SetColor(i, 1);

                    visualizer.Swap(i - 1, i);

                    yield return new WaitForSeconds(visualizer.WaitTime);

                    swapped = true;
                }
                else
                {
                    visualizer.SetColor(i - 1, 2);
                    visualizer.SetColor(i, 2);

                    yield return new WaitForSeconds(visualizer.WaitTime);
                }

                visualizer.ResetColor();
            }
        }

        for (int i = 0; i < Columns.Count; i++)
        {
            visualizer.SetColor(i, 2);
            visualizer.PlaySound(Columns[i].Height + 1);
            yield return new WaitForSeconds(visualizer.WaitTime);
        }

        visualizer.StopSound();
    }
}
