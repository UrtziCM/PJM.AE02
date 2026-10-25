using UnityEngine;

public class AndroidHapticsHandler : IHapticsHandler
{
    public void TriggerHaptics()
    {
        Handheld.Vibrate();
    }
}
