using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnAlly : MonoBehaviour
{
    [SerializeField] GameObject ally;

    [SerializeField] Image imgAlly;
    [SerializeField] Text txtAlly;
    [SerializeField] bool isReady;
    float cooldown;

    Ally a;
    Color oriColor;

    public bool IsReady { get => isReady; set => isReady = value; }

    // Start is called before the first frame update
    void Awake()
    {
        a = ally.GetComponent<Ally>();
        a.Btn = this;
        cooldown = a.TimeCooldown;
        imgAlly.sprite = a.Portrait;
        txtAlly.text = a.RecoursePoints.ToString();
        oriColor = imgAlly.color;

        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Cooldown
        if (isReady)
        {
            imgAlly.fillAmount = 1;
            imgAlly.color = oriColor;
        }
        else
        {
            imgAlly.color = Color.gray;
            imgAlly.fillAmount -= 1.0f / cooldown * Time.deltaTime;
        }


        //Puntos
        if (GameManager.instance.recoursePoints < a.RecoursePoints)
        {
            imgAlly.color = Color.gray;
        }
        else if (isReady)
        {
            imgAlly.color = oriColor;
        }
    }

    public void SelectAlly()
    {
        //if (GameManager.instance.recoursePoints < a.RecoursePoints && isReady)
        GameManager.instance.allySelected = ally;
        Debug.Log("SelectAlly()");
    }
    public void StartCooldown()
    {
        StartCoroutine(CooldownAlly());
    }

    IEnumerator CooldownAlly()
    {
        isReady = false;
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
