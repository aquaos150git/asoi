using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerStakan : MonoBehaviour
{
    private FixedInteractable _interactable;
    private FixedCircularDrive _circular;
    public Collider DopCollider;
    private GameObject _tool;

    public StakanStateManager StakanManager;
    public Tool Tool;

    private bool _isCircular; // true - можно откручивать нижний стакан

    private void Start()
    {
        _interactable = GetComponent<FixedInteractable>();
        _circular = GetComponent<FixedCircularDrive>();

        _circular.onMinAngle.AddListener(OnMinAngle);
    }

    public void OnMinAngle()
    {
        StakanManager.LowerStakanMin();
        _interactable.enabled = false;
        Tool.LowerPosition();
        Tool.GetTool(false);
    }

    public void StopPumpJack(int CrankPosition)
    {
        if (CrankPosition == 1) _isCircular = true;
        else _isCircular = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject tool = other.gameObject;

        if (tool.name == "tool#1" && (_isCircular || tool.GetComponent<Tool>().IsLowerStakanEnabled))
        {
                _interactable.enabled = true;
                _circular.enabled = true;
                DopCollider.enabled = true;
                other.GetComponent<Tool>().SetParent(false);
                _isCircular = false;

            if (tool.GetComponent<Tool>().IsLowerStakanEnabled)
            {
                _tool = tool;
                _circular.onMaxAngle.RemoveAllListeners();
                _circular.onMaxAngle.AddListener(NewLowerStakanMin);
            }
        }
    }

    private void NewLowerStakanMin()
    {
        _tool.GetComponent<Tool>().GetTool(true);
        _circular.enabled = false;
        _interactable.enabled = false;
        _tool.GetComponent<Tool>().IsLowerStakanEnabled = false;
    }

    public void StopLowerStakan()
    {
        _interactable.enabled = false;
        _circular.enabled = false;
    }

    public void DisableDopCollider() => DopCollider.enabled = false;
}
