using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;


public class STMerge : MonoBehaviour
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
    IEnumerator SortC()
    {
        visualizer.Initialize();
        visualizer.Shufle();

        yield return new WaitForSeconds(visualizer.WaitTime);



        for (int i = 0; i < Columns.Count; i++)
        {
            visualizer.SetColor(i, 2);
            visualizer.PlaySound(Columns[i].Height + 1);
            yield return new WaitForSeconds(visualizer.WaitTime);
        }

        visualizer.StopSound();
    }
    int FindInsertionIndex(int valueToInsert)
    {
        for (int i = 0; i < Columns.Count; i++)
        {
            if (Columns[i].Height.CompareTo(valueToInsert) > 0)
            {
                return i;
            }
        }

        return -1;
    }
}
