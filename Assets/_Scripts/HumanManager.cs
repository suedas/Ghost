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
            other.transform.DOScale(1.6f, 1f).OnComplete(()=> { 
                other.transform.DOScale(1f, 1f);
                other.transform.DOMoveY(0, .5f);
             
        });

       // yield return new WaitForSeconds(.2f);
        StartCoroutine(delay());
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
        // yield return new WaitForSeconds(.2f);
        //human.SetBool("escape", true);
        //gameObject.transform.Rotate(0, 180, 0);
        //yield return new WaitForSeconds(.02f);

        child = transform.childCount;
        for (int i = 0; i < child; i++)
        {
            yield return new WaitForSeconds(.001f);
            
            GameObject dusman = transform.GetChild(i).gameObject;
            dusman.GetComponent<Animator>().SetBool("escape", true);
            dusman.transform.Rotate(0, 180, 0);

          
                if (dusman.transform.position.x >= 0)
                {
                    Debug.Log("saga");
                    //(Random.Range(0, 1.9f)
                         dusman.transform.DOMove(new Vector3(Random.Range(-5.5f, -4.4f), 0, Random.Range(transform.position.z + 20, transform.position.z + 35)), .5f).OnComplete(() => {
                        dusman.transform.GetComponent<Animator>().SetBool("ss", true);
                        //gh.transform.GetChild(i).GetComponent<Animator>().enabled = false;
                        dusman.transform.DOMove(new Vector3(Random.Range(-12, -6), -8, Random.Range(transform.position.z + 20, transform.position.z + 35)), 3f).OnComplete(() => {  Destroy(gameObject); }) ;
                        

                    });
                    //Debug.Log("sola");
                    ////Random.Range(-1.9f, 0)
                    //gh.transform.GetChild(i).DOMove(new Vector3(4.5f, 0, 50), .4f);


                    //gh.transform.GetChild(i).DOMove(new Vector3(3.5f, 0, 50), .5f).OnComplete(()=> {  });

                    //gh.transform.GetChild(i).DOMoveY(-5f, 2f).OnComplete(() => { transform.DOKill(); Destroy(gameObject); });


                    //gh.transform.GetChild(i).DOMoveX(Random.Range(-3f,0), 5f).OnComplete(()=>
                    //{ gh.transform.GetChild(i).DOMove(new Vector3(gh.transform.GetChild(i).position.x, 0, 200), 10f); });

                }
                else if (dusman.transform.position.x < 0)
                {
                    Debug.Log("saga");
                    //(Random.Range(0, 1.9f)
                    dusman.transform.DOMove(new Vector3(Random.Range(5.5f,4.4f), 0, Random.Range(transform.position.z+20,transform.position.z+35)), .5f).OnComplete(() => {
                    dusman.transform.GetComponent<Animator>().SetBool("ss", true);
                    //gh.transform.GetChild(i).GetComponent<Animator>().enabled = false;
                    dusman.transform.DOMove(new Vector3( Random.Range(12,6), -8, Random.Range(transform.position.z + 20, transform.position.z + 35)),3f).OnComplete(() => {  Destroy(gameObject); });

                    });

                    // gh.transform.GetChild(i).DOMove(new Vector3(-3.5f, 0, 50), .5f);


                    //gh.transform.GetChild(i).DOMoveY(-5f, 2f).OnComplete(()=> { transform.DOKill(); Destroy(gameObject); });

                    //gh.transform.GetChild(i).DOMoveX(Random.Range(-1f, 1), .5f).OnComplete(() =>
                    //{ gh.transform.GetChild(i).DOMove(new Vector3(gh.transform.GetChild(i).position.x, 0, 200), 10f); });
                }
              
            
          
          
        }
       // yield return new WaitForSeconds(1f);
        PlayerController.instance.idleGhost.enabled = true;
        TailController.instance.sagTail.TailAnimatorAmount = 0f;
        TailController.instance.solTail.TailAnimatorAmount = 0f;
        PlayerMovement.instance.speed = 6f;
        //gh.transform.DOMove(new Vector3(Random.RandomRange(-1.9f,1.9f),-.95f,200),10f);
   
    }

}
