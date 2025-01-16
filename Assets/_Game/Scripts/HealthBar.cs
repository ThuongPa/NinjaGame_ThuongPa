using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private Vector3 offset;

    private float hp;
    private float maxHp;
    // Start is called before the first frame update

    private Transform target;
    private void Start()
    {
        offset = new Vector3(1.1f, 1, 0);
    }
    void Update()
    {
     
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
        transform.position = target.position + offset;
    }

    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp  = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;

    }
    public void SetNewHp(float hp)
    {
        this.hp = hp;
        imageFill.fillAmount = hp/maxHp;
    }
    // Update is called once per frame
  
}
