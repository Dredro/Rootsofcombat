using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite head_SciFi;
    public Sprite head_modern;
    public Sprite head_pirate;
    public Sprite head_knight;
    public Sprite head_caveman;

    public Image image;
    public Slider slider;

    public int game_state;

    public float health;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetHead(int state)
    {
        game_state = state;
        switch (game_state)
        {
            case 0:
                image.sprite = head_SciFi;
                break;
            case 1:
                image.sprite = head_modern;
                break;
            case 2:
                image.sprite = head_pirate;
                break;
            case 3:
                image.sprite = head_knight;
                break;
            case 4:
                image.sprite = head_caveman;
                break;
        }
    }

    public void SetValue(float hp)
    {
        health = hp;
        slider.value = hp / 100;
    }
}
