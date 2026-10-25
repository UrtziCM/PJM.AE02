using UnityEngine;

public class LifeManager : MonoBehaviour, IEventListener<BombExplodeData>
{
    int lifes = 3;
    public void OnEvent(BombExplodeData eventData)
    {
        EventBus.Call(new LifeLostData(oldLifes: lifes, currentLifes: --lifes));
    }

    private void Start()
    {
        EventBus.Subscribe<BombExplodeData>(this);
    }

}
