using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragonPicker : MonoBehaviour
{
    public Sprite[] dragon1Colors;
    public Sprite[] dragon2Colors;
    public Image preview;
    public InputField dragonName;

    private int type = 0;
    private int color = 0;

    public void ChangeType(int type)
    {
        this.type = type;
        UpdatePreview();
    }

    public void ChangeColor(int color)
    {
        this.color = color;
        UpdatePreview();
    }

    public void UpdatePreview()
    {
        if (type == 0) preview.sprite = dragon1Colors[color];
        else preview.sprite = dragon2Colors[color];
    }

    public void Continue()
    {
        string name = dragonName.text;

        // Save data to manager

        GameManager.gameManager.SetDragon((DragonType)type, (DragonColor)(--color < 0 ? 2 : color));

        // Start level

        SceneManager.LoadScene(2);
    }
}
