using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{

    public GameObject confetiP, magicP, dolarP;
    public Animator chestAnim;

    private void Start()
    {
        confetiP.SetActive(false);
        magicP.SetActive(false);
        dolarP.SetActive(false);
        chestAnim.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.isContinue = false;
            chestAnim.enabled = true;
            StartCoroutine( delay());
            confetiP.SetActive(true);
            magicP.SetActive(true);
            dolarP.SetActive(true);
            PlayerController.instance.anim.SetBool("run", false);
            PlayerController.instance.anim.SetBool("dance", true);
            StartCoroutine(chestDelay());


        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(3f);

    }
    IEnumerator chestDelay()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.oyunsonu();
        UiController.instance.OpenWinPanel();
        //Debug.Log(GameManager.instance.levelScore);
        //if (GameManager.instance.levelScore > 10) UiController.instance.OpenWinPanel();
        //else UiController.instance.OpenLosePanel();
    }
}


