using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if(slider == null)
            slider = this.GetComponent<Slider>();
        if(gameObject.GetComponentInParent<Enemy>() != null)
            gameObject.GetComponentInParent<Enemy>().HealthSetting();
        if (gameObject.GetComponentInParent<Health>() != null)
            gameObject.GetComponentInParent<Health>().HealthSetting();
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetSlider(Slider setSlider)
    {
        slider = setSlider;
    }

    public bool GetSlider()
    {
        if(slider != null)
            return true;
        return false;
    }
}
