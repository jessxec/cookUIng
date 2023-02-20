using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    public Image[] collectables;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }


    public void UpdateCollection(string eggName)
    {
        foreach (var img in collectables)
        {
            if (img.name == eggName)
            {
                img.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
