using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;


public class CameraController : MonoBehaviour
{
    public List<State> States;
    public float Duration;
    public Ease EaseMode;

    public int Target;
    [Button]
    public void LoadSate(int index = -1)
    {
        if (index == -1)
            index = Target;

        foreach (var item in States)
        {
            item.Menu.SetActive(false);
        }
        States[index].Menu.SetActive(true);

        transform.DOMove(States[index].Position, Duration).SetEase(EaseMode);
        transform.DORotate(States[index].Rotation, Duration).SetEase(EaseMode);
    }
    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }
}

[System.Serializable]
public class State
{
    public Vector3 Position;
    public Vector3 Rotation;
    public GameObject Menu;
}
