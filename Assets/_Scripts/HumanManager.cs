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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ghost"))
        {
            child = transform.childCount;
            for (int i = 0; i < child; i++)
            {
                 transform.GetChild(i).GetComponent<Animator>().SetBool("turn", true);
                 transform.GetChild(i).Rotate(0, 180, 0);
            }

           // human.SetBool("turn", true);
         
            //human.SetBool("escape", true);
            StartCoroutine(delay(gameObject));
        }
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
            //tekrar düzenle
            if (gh.transform.GetChild(i).position.x > -2)
            {
                Debug.Log("sola");
                
                gh.transform.GetChild(i).DOMove(new Vector3(Random.Range(-1f, -3f), 0, 200), 10f);
                //gh.transform.GetChild(i).DOMoveX(Random.Range(-3f,0), 5f).OnComplete(()=>
                //{ gh.transform.GetChild(i).DOMove(new Vector3(gh.transform.GetChild(i).position.x, 0, 200), 10f); });

            }
            else if(gh.transform.GetChild(i).position.x <= -2)
            {
                Debug.Log("saga");
                gh.transform.GetChild(i).DOMove(new Vector3(Random.Range(-2f, 1), 0, 200), 10f);
                //gh.transform.GetChild(i).DOMoveX(Random.Range(-1f, 1), .5f).OnComplete(() =>
                //{ gh.transform.GetChild(i).DOMove(new Vector3(gh.transform.GetChild(i).position.x, 0, 200), 10f); });
            }
        }
        //gh.transform.DOMove(new Vector3(Random.RandomRange(-1.9f,1.9f),-.95f,200),10f);
    }
}
