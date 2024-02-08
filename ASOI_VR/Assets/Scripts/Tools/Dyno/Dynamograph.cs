using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

[RequireComponent(typeof(DynamographButton))]
[RequireComponent(typeof(DinoRoutines))]
public class Dynamograph : MonoBehaviour
{
    public IWindowState State { get; set; }
    public Text _text;
    public GameObject _canvas;
    public DinamogrammRenderer dinamogrammRenderer;
    public Dynamometer _dynamometer;

    private void Start()
    {
        State = new DisabledDynamographState(this);
    }

    public void Redo() => State.Redo(this);

    public void TurnOff() => State = new DisabledDynamographState(this);
}

public interface IWindowState
{
    int WindowId { get; }
    void Redo(Dynamograph dynamograph);
}

class DisabledDynamographState : IWindowState
{
    private Coroutine coroutine;
    int IWindowState.WindowId => 0;

    public DisabledDynamographState(Dynamograph dynamograph) => 
        Undo(dynamograph);

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 200;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "��������...";
        dynamograph._canvas.SetActive(true);
        coroutine = DinoRoutines.Instance.ChangeScreen(5f, dynamograph);
    }

    public void Undo(Dynamograph dynamograph)
    {
        dynamograph._text.text = "";
        dynamograph._canvas.SetActive(false);
        if (coroutine != null) DinoRoutines.Instance.StopCoroutine(coroutine);
    }
}

// ����� ������ ������ - ����������, ������, ������...
class SelectionModeDynamographState : IWindowState
{
    int IWindowState.WindowId => 1;

    public SelectionModeDynamographState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 200;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.lineSpacing = 0.9f;
        dynamograph._text.text = "1 - ������ \n" +
            "2,9 - ���������� \n" +
            "3 - ������ \n" +
            "4 - �.�������� \n" +
            "��������� ���.";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new SelectionDataDynamographState(dynamograph);
    }

}

// ����� ���������� ��� ����� � ������ "����������"
class SelectionDataDynamographState : IWindowState
{
    int IWindowState.WindowId => 2;

    public SelectionDataDynamographState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "ENTER - ������� � ����� ����������. \n" +
            "[0..5, 7] - ����, ������� � ����������. \n" +
            "SHIFT - ������� � ����� ������������. \n";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterWellNumberState(dynamograph);
    }

}

class EnterWellNumberState: IWindowState
{
    int IWindowState.WindowId => 3;

    public EnterWellNumberState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "0 �����  319";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterBushNumberState(dynamograph);
    }

}

class EnterBushNumberState : IWindowState 
{
    int IWindowState.WindowId => 4;

    public EnterBushNumberState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "1 ����  28";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterFieldNumberState(dynamograph);
    }
}

class EnterFieldNumberState: IWindowState
{
    int IWindowState.WindowId => 5;

    public EnterFieldNumberState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "2 ���������  271";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterResearchTypeState(dynamograph);
    }
}


class EnterResearchTypeState : IWindowState
{
    int IWindowState.WindowId => 6;

    public EnterResearchTypeState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "3   ���   1�2Z3�4�    1";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterStemDiametrState(dynamograph);
    }
}

class EnterStemDiametrState: IWindowState
{
    int IWindowState.WindowId => 7;

    public EnterStemDiametrState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "4   �������   50";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterCrankHoleState(dynamograph);
    }
}

class EnterCrankHoleState: IWindowState
{
    int IWindowState.WindowId => 8;

    public EnterCrankHoleState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "5   �������.   0";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterCyclesState(dynamograph);
    }
}

class EnterCyclesState: IWindowState
{
    int IWindowState.WindowId => 9;

    public EnterCyclesState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "7   �.�. 2..9   2";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterBufferPressureState(dynamograph);
    }
}

class EnterBufferPressureState: IWindowState
{
    int IWindowState.WindowId => 10;

    public EnterBufferPressureState(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "�   ��������   2.2";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterShift1State(dynamograph);
    }

}

class EnterShift1State : IWindowState
{
    public int WindowId => 11;

    public EnterShift1State(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "1. T. �������";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new EnterShift2State(dynamograph);
    }

}

class EnterShift2State : IWindowState
{
    public int WindowId => 12;
    private float time;

    public EnterShift2State(Dynamograph dynamograph)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "2. T. �������";
        time = Time.time; // ����� ������� ������� Shift (�� ���������������� ������� �� ������ Shift)
    }

    public void Redo(Dynamograph dynamograph)
    {
        float TimeBetweenShift = Time.time - time;
        TimeBetweenShift = Mathf.Round(TimeBetweenShift * 10.0f) * 0.1f; // ���������� �� �������
        dynamograph.State = new EnterShift3State(dynamograph, TimeBetweenShift);
    }

}

class EnterShift3State : IWindowState
{
    public int WindowId => 13;

    public EnterShift3State(Dynamograph dynamograph, float time)
    {
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "������������ ������������ �������: " + time +
            "\n" + "������� Shift ��� ������ ���������";
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph._text.text = "";

        if (!dynamograph._dynamometer.CompareTag("Dynamometer"))
        {
            dynamograph.State = new PaintingDynomogramState(dynamograph);
        }
    }
}

class PaintingDynomogramState: IWindowState
{
    public int WindowId => 14;

    public PaintingDynomogramState(Dynamograph dynamograph)
    {
        dynamograph.dinamogrammRenderer.SetDraw();
    }

    public void Redo(Dynamograph dynamograph)
    {
        dynamograph.State = new SaveDynomogramState(dynamograph);
    }
}

class SaveDynomogramState: IWindowState
{
    public int WindowId => 15;

    public SaveDynomogramState(Dynamograph dynamograph)
    {
        dynamograph.dinamogrammRenderer.DinoVar.SetActive(false);
        dynamograph._text.fontSize = 50;
        dynamograph._text.alignment = TextAnchor.MiddleCenter;
        dynamograph._text.text = "������������ ���������";
        dynamograph._dynamometer.Unttach();
    }

    public void Redo(Dynamograph dynamograph)
    {
        // �����?
    }
}
