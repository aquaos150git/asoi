using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSeals : MonoBehaviour
{
    private int _count;
    public Seal_trigger _sealTrigger;

    private bool _oldS1;
    private bool _oldS2;
    private bool _oldS3;

    private void OnTriggerExit(Collider other)
    {
        var Name = other.gameObject.name;

        if (Name.Equals("old_s1") && !_oldS1)
        {
            _count++;
            _oldS1 = true;
        }

        else if (Name.Equals("old_s2") && !_oldS2)
        {
            _count++;
            _oldS2 = true;
        }

        else if (Name.Equals("old_s3") && !_oldS3)
        {
            _count++;
            _oldS3 = true;
        }

        if (_count == 3)
        {
            _sealTrigger.IsTrigger = enabled;
        }
    }
}
