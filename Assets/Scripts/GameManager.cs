using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Image[] collectables;

    private void Awake()
    {
        gameManager = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var img in collectables)
        {
            img.color = new Color32(0,0,0,90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
