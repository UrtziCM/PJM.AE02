using UnityEngine;

public class FeedbackHaptics : MonoBehaviour, IEventListener<BombExplodeData>
{
    IHapticsHandler hapticsHandler;

    public void OnEvent(BombExplodeData eventData)
    {
        hapticsHandler.TriggerHaptics();
    }

    void Start()
    {
#if UNITY_ANDROID
        hapticsHandler = new AndroidHapticsHandler();
#endif
#if UNITY_IOS
        hapticsHandler = new IOSHapticsHandler();
#endif
        EventBus.Subscribe<BombExplodeData>(this);
    }

    private void OnDestroy()
    {
        EventBus.Subscribe<BombExplodeData>(this);

    }

}
