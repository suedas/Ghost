using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HumanManager : MonoBehaviour
{
    public Animator human;
    #region Singleton
    public static HumanManager instance;
    public int child;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
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
            other.transform.DOScale(1.6f, 1f).OnComplete(()=> { 
                other.transform.DOScale(1f, 1f);
                other.transform.DOMoveY(0, .5f);
             
        });

        yield return new WaitForSeconds(1f);
        StartCoroutine(delay(gameObject));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ghost"))
        {

            StartCoroutine(ghostAnim(other.gameObject));
         //   StartCoroutine(bekle());
          
            child = transform.childCount;
            for (int i = 0; i < child; i++)
            {
                 transform.GetChild(i).GetComponent<Animator>().SetBool("turn", true);
                 transform.GetChild(i).Rotate(0, 180, 0);
            }

           // human.SetBool("turn", true);
         
            //human.SetBool("escape", true);
          
          ///

        }
    }
    IEnumerator bekle()
    {
        yield return new WaitForSeconds(1f);

        // gameObject.GetComponent<Collider>().enabled = true;

    }

    IEnumerator delay(GameObject gh)
    {
       // yield return new WaitForSeconds(.2f);
        //human.SetBool("escape", true);
   
        
        for (int i = 0; i < child; i++)
        {
            
            yield return new WaitForSeconds(.02f);
            //gameObject.transform.Rotate(0, 180, 0);
            gh.transform.GetChild(i).GetComponent<Animator>().SetBool("escape", true);
            //tekrar d�zenle
            if (gh.transform.GetChild(i).position.x >= 0)
            {
                Debug.Log("sola");
                
                gh.transform.GetChild(i).DOMove(new Vector3(Random.Range(-1.9f, 0), 0, 200), 19f);
                //gh.transform.GetChild(i).DOMoveX(Random.Range(-3f,0), 5f).OnComplete(()=>
                //{ gh.transform.GetChild(i).DOMove(new Vector3(gh.transform.GetChild(i).position.x, 0, 200), 10f); });

            }
            else if(gh.transform.GetChild(i).position.x < 0)
            {
                Debug.Log("saga");
                gh.transform.GetChild(i).DOMove(new Vector3(Random.Range(0, 1.9f), 0, 200), 19f);
                //gh.transform.GetChild(i).DOMoveX(Random.Range(-1f, 1), .5f).OnComplete(() =>
                //{ gh.transform.GetChild(i).DOMove(new Vector3(gh.transform.GetChild(i).position.x, 0, 200), 10f); });
            }
        }
        //yield return new WaitForSeconds(1f);
        PlayerController.instance.idleGhost.enabled = true;
        TailController.instance.sagTail.TailAnimatorAmount = 0f;
        TailController.instance.solTail.TailAnimatorAmount = 0f;
        PlayerMovement.instance.speed = 6f;
        //gh.transform.DOMove(new Vector3(Random.RandomRange(-1.9f,1.9f),-.95f,200),10f);
    }
}
