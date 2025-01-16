using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatText : MonoBehaviour
{
    [SerializeField] Text hpText;

    // Start is called before the first frame update
    public void OnInit(float hp)
    {
        hpText.text = hp.ToString();
        Invoke(nameof(OnDespawn), 1f);

    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
