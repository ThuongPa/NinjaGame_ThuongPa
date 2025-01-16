using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static UIManager instance;
    [SerializeField] Text coinText;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }
   
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
