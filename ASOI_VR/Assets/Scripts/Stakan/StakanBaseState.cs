using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Valve.VR.InteractionSystem;

public abstract class StakanBaseState
{
    public abstract void EnterState(StakanStateManager stakan);
    public abstract void UpdateState(StakanStateManager stakan);
    public abstract void OnTriggerEnter(StakanStateManager stakan, Collider collider);
}

public class Untouchable : StakanBaseState
{
    public override void EnterState(StakanStateManager stakan)
    {
        stakan._interactable.enabled = false;
        stakan._circular.enabled = false;
        stakan._linear.enabled = false;
    }
    public override void UpdateState(StakanStateManager stakan)
    {
        stakan.SwitchState(stakan.TouchableState);
    }
    public override void OnTriggerEnter(StakanStateManager stakan, Collider collider)
    {

    }

public class Touchable : StakanBaseState
    {
        public override void EnterState(StakanStateManager stakan)
        {
            stakan._linear.enabled = false;
        }
        public override void UpdateState(StakanStateManager stakan)
        {
            stakan.SwitchState(stakan.LinearMotionState);
        }
        public override void OnTriggerEnter(StakanStateManager stakan, Collider collider)
        {
            collider.gameObject.GetComponent<Tool>().SetParent(true);
            stakan._circular.enabled = true;
            stakan._interactable.enabled = true;
        }
    }

public class CircularMotion : StakanBaseState
    {
        public override void EnterState(StakanStateManager stakan)
        {

        }
        public override void UpdateState(StakanStateManager stakan)
        {

        }
        public override void OnTriggerEnter(StakanStateManager stakan, Collider collider)
        {

        }
    }

public class LinearMotion : StakanBaseState
    {
        public override void EnterState(StakanStateManager stakan)
        {
            stakan._linearMapping.value = 0;
            stakan._circular.enabled = false;
        }
        public override void UpdateState(StakanStateManager stakan)
        {
            Vector3 oldLocalPosition = Vector3.MoveTowards(stakan._transform.localPosition, stakan.FinalPose, Time.fixedDeltaTime);
            stakan._transform.localPosition = oldLocalPosition;
            stakan._linearMapping.value = (stakan._transform.localPosition.y - stakan.FinalPose.y) / (stakan._linear.endPosition.localPosition.y - stakan.FinalPose.y);       
        }
        public override void OnTriggerEnter(StakanStateManager stakan, Collider collider)
        {

        }
    }


}
