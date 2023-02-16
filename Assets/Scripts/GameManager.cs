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
        S = this;
        DontDestroyOnLoad(this);
        foreach (var img in collectables)
        {
            img.color = new Color32(0, 0, 0, 90);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
  
    }

    public void UpdateCollection(string eggName)
    {
        foreach (var img in collectables)
        {
            Debug.Log(img.name);
            if (img.name == eggName)
            {
                img.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
