using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UILifeHandler : MonoBehaviour, IEventListener<LifeLostData>
{
    [SerializeField]
    Image[] lifeImages;
    [SerializeField]
    Sprite lifeFullTexture;
    [SerializeField]
    Sprite lifeEmptyTexture;

    public void OnEvent(LifeLostData eventData)
    {
        SetRemainingLifes(eventData.currentLifes);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Subscribe<LifeLostData>(this);
    }

    private void SetRemainingLifes(int lifes)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i >= lifeImages.Length - lifes)
            {
                lifeImages[i].sprite = lifeFullTexture;
                lifeImages[i].enabled = true;        
            }
            else
            {
                lifeImages[i].sprite = lifeEmptyTexture;
            }
        }
    }
}
