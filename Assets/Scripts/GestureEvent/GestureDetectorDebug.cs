using NaughtyAttributes;

public class GestureDetectorDebug : RecognizeGesture
{
    public Gesture AimGesture;

    [Button]
    public void InvokeAim()
    {
        onRecognizeGesture.Invoke(AimGesture);
    }

    public Gesture EarthPrisonGesture;

    [Button]
    public void InvokeEarthPrison()
    {
        onRecognizeGesture.Invoke(EarthPrisonGesture);
    }

    public Gesture FireGesture;

    [Button]
    public void InvokeFire()
    {
        onRecognizeGesture.Invoke(FireGesture);
    }

    public Gesture GustGesture;

    [Button]
    public void InvokeGust()
    {
        onRecognizeGesture.Invoke(GustGesture);
    }

    public Gesture IdleGesture;

    [Button]
    public void InvokeIdle()
    {
        onRecognizeGesture.Invoke(IdleGesture);
    }

    public Gesture LightningGesture;

    [Button]
    public void InvokeLightning()
    {
        onRecognizeGesture.Invoke(LightningGesture);
    }

    public Gesture RecoverGesture;

    [Button]
    public void InvokeRecover()
    {
        onRecognizeGesture.Invoke(RecoverGesture);
    }

    public Gesture ShieldGesture;

    [Button]
    public void InvokeShield()
    {
        onRecognizeGesture.Invoke(ShieldGesture);
    }
}