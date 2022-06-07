using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRepository : AimRef
{
    [SerializeField] private AimRef palmAim;
    [SerializeField] private AimRef fingerEndAim;

    public void SetAim(AimEnum e)
    {
        if (e == AimEnum.Palm)
        {
            var (st, en) = palmAim.GetAim();
            start = st;
            end = en;
        }
        else if (e == AimEnum.FingerEnd)
        {
            var (st, en) = fingerEndAim.GetAim();
            start = st;
            end = en;
        }
    }

    public void SetAimPalm()
    {
        SetAim(AimEnum.Palm);
    }

    public void SetAimFingerEnd()
    {
        SetAim(AimEnum.FingerEnd);
    }
}
