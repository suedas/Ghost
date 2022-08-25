using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HumanManager : MonoBehaviour
{
    public Animator human;
    #region Singleton
    public int child;
   // public GameObject scared;

    #endregion
    private void Start()
    {
    
    }


    IEnumerator ghostAnim(GameObject other)
    {
            gameObject.GetComponent<Collider>().enabled = false;///
            PlayerController.instance.idleGhost.enabled = false;
            PlayerMovement.instance.speed = 0;
            yield return new WaitForSeconds(.2f);
            TailController.instance.sagTail.TailAnimatorAmount = 1.3f;
            TailController.instance.solTail.TailAnimatorAmount = 1.3f;
            other.transform.DOMoveY(1f, .5f);
        other.transform.DOScale(1.6f, 1f).OnComplete(() =>
        {
            Debug.Log("küçül");
            other.transform.DOScale(1f, 1f);
            other.transform.DOMoveY(0, .5f);
            StartCoroutine(delay());

        });
       

       // yield return new WaitForSeconds(.2f);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ghost"))
        {
            StartCoroutine(PlayerController.instance.swipeController());
           
            StartCoroutine(ghostAnim(other.gameObject));          
            child = transform.childCount;
          
            for (int i = 0; i < child; i++)
            {
                 transform.GetChild(i).GetComponent<Animator>().SetBool("turn", true);
                transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                 //transform.GetChild(i).Rotate(0, 180, 0);
            }
           
            GameManager.instance.IncreaseScore();
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player girdi");
            other.transform.DOMoveZ(other.transform.position.z + 2, .2f);
            SwerveMovement.instance.isSwipe = false;
            PlayerMovement.instance.speed = 0;
            PlayerController.instance.anim.SetBool("run", false);
            PlayerController.instance.anim.SetBool("focus", true);
            int humanChild=transform.childCount;
            for (int i = 0; i < humanChild; i++)
            {
                Debug.Log("dövme gerceklesti");
                transform.GetChild(i).GetComponent<Animator>().SetBool("hit", true);
                transform.GetChild(i).GetChild(3).gameObject.SetActive(true);
                    if (transform.GetChild(i).position.x > 0)
                    {
                        transform.GetChild(i).Rotate(0, 90, 0);
                    }
                    else if (transform.GetChild(i).position.x < 0)
                    {
                        transform.GetChild(i).Rotate(0, -90, 0);
                    }
               
                     //transform.GetChild(i).LookAt(2*transform.position- other.gameObject.transform.position);

                //transform.GetChild(i).Rotate(0, 180, 0);
            }
            StartCoroutine(bekle());
        }
     
     
    }
   
    IEnumerator bekle()
    {
        yield return new WaitForSeconds(2f);
        UiController.instance.OpenLosePanel();
        // gameObject.GetComponent<Collider>().enabled = true;

    }
  
    IEnumerator delay()
    {
        yield return new WaitForSeconds(.001f);
        child = transform.childCount;
        for (int i = 0; i < child; i++)
        {
            
            
            GameObject dusman = transform.GetChild(i).gameObject;
            dusman.GetComponent<Animator>().SetBool("escape", true);
            dusman.transform.Rotate(0, 180, 0);
       
                if (dusman.transform.position.x >= 0)
                {
                    Debug.Log("saga");
                    //(Random.Range(0, 1.9f)
                         dusman.transform.DOMove(new Vector3(Random.Range(5.5f, 4.4f), 0, Random.Range(transform.position.z + 20, transform.position.z + 35)), 1.5f).OnComplete(() => {
                        dusman.transform.GetComponent<Animator>().SetBool("ss", true);
                        //gh.transform.GetChild(i).GetComponent<Animator>().enabled = false;
                        dusman.transform.DOMove(new Vector3(Random.Range(12, 6), -8, Random.Range(transform.position.z + 20, transform.position.z + 35)), 3f).OnComplete(() => {  Destroy(gameObject); }) ;
                        
                    });

                }
                else if (dusman.transform.position.x < 0)
                {
                    Debug.Log("saga");
                    //(Random.Range(0, 1.9f)
                    dusman.transform.DOMove(new Vector3(Random.Range(-5.5f,-4.4f), 0, Random.Range(transform.position.z+20,transform.position.z+35)), 1.5f).OnComplete(() => {
                    dusman.transform.GetComponent<Animator>().SetBool("ss", true);
                    dusman.transform.DOMove(new Vector3( Random.Range(-12,-6), -8, Random.Range(transform.position.z + 20, transform.position.z + 35)),3f).OnComplete(() => {  Destroy(gameObject); });

                    });
                }     
        }
     
        PlayerController.instance.idleGhost.enabled = true;
        TailController.instance.sagTail.TailAnimatorAmount = 0f;
        TailController.instance.solTail.TailAnimatorAmount = 0f;
        PlayerMovement.instance.speed = 10f;
   
    }

}
