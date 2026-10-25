using UnityEngine;

public class OnTapShotRaycaster : MonoBehaviour , IEventListener<ScreenTappedData>
{
    public void OnEvent(ScreenTappedData eventData)
    {
        Ray rayFromCamera = Camera.main.ScreenPointToRay(eventData.pos);
        if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IShootable shootable))
            {
                shootable.Shoot();
            }
        }

    }

    private void Start()
    {
        EventBus.Subscribe<ScreenTappedData>(this);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ScreenTappedData>(this);
    }
}
