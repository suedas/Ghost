using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using FIMSpace.FTail;


public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion

    public GameObject ghost;
    public GameObject player;
    public CinemachineVirtualCamera cb;

    public  SkinnedMeshRenderer skinnedMeshRenderer;
    public Mesh skinnedMesh;
    float blendOne = 0;
    float blendTwo = 0;
    public GameObject circleP;
    public GameObject boomP;
    public GameObject starP;
    public GameObject fýckP,ruzgar;
    public Animator anim;
    public  Animator idleGhost;
    public int count;
    public Transform diamondTarget;
    public GameObject box;
    public GameObject humans;
   
    
    
    //public TailAnimator2 sagTail, solTail;
   
    private void Start()
    {
        
        anim =player.GetComponent<Animator>();      
        
    }
    public void Ghost()
    {
        SwerveMovement.instance.isHuman = false;
        ghost.SetActive(true);
        gameObject.tag = "ghost";
        player.SetActive(false);
    }
    public void Human()
    {
        SwerveMovement.instance.isHuman = true;
        player.SetActive(true);
        gameObject.tag = "Player";
        anim.SetBool("run", true);
        ghost.SetActive(false);
    
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("diamond"))
        {
            if (gameObject.tag=="Player")
            {
                other.gameObject.transform.DOMove(diamondTarget.transform.position, .5f).OnComplete(()=> { Destroy(other.gameObject); });
                other.gameObject.transform.DOScale(.25f, .2f);
                GameManager.instance.IncreaseScore();
            }
        }
        else if (other.CompareTag("duvar"))
        {
            StartCoroutine(swipeController());
            if (gameObject.tag=="Player")
            {  
                //SwerveMovement.instance.isSwipe = false;
                StartCoroutine(stickmanAim(other.gameObject));
            }
        }
        else if (other.CompareTag("basamak"))
        {
            StartCoroutine(swipeController());


            if (gameObject.tag=="Player")
            {
                //SwerveMovement.instance.isSwipe = false;
                StartCoroutine(stickmanAim(other.gameObject));
            }
        }
      
        else if (other.CompareTag("bosluk"))
        {
            StartCoroutine(swipeController());

            if (gameObject.tag=="Player")
            {
                //gameObject.GetComponent<Rigidbody>().useGravity = true;
                cb.enabled = false;
                //SwerveMovement.instance.isSwipe = false;
                anim.SetBool("fall", true);
                anim.SetBool("run", false);
                gameObject.transform.DOMove(new Vector3(transform.position.x, -10f, transform.position.z + 25), 1.2f).OnComplete(()=> {
                    UiController.instance.OpenLosePanel();
                });
                  
               

            }
            else if (gameObject.tag=="ghost")
            {
                Debug.Log("burda");
                StartCoroutine(swipeController());
            }
         
        }
        
        else if (other.CompareTag("mazgal"))
        {
            StartCoroutine(swipeController());

            if (gameObject.tag=="ghost")
            {
               // SwerveMovement.instance.isSwipe = false;
                idleGhost.enabled = false;
                TailController.instance.sagTail.TailAnimatorAmount = 1.3f;
                TailController.instance.solTail.TailAnimatorAmount = 1.3f;
                skinnedMeshRenderer.SetBlendShapeWeight(0, 0);
                StartCoroutine(huplet());
                gameObject.transform.DOMoveY(-1, .2f);
              
                // hüpp animasyonu :))
                GameManager.instance.isContinue = false;
                PlayerMovement.instance.speed = 0;
              
            }
          
        }
        else if (other.CompareTag("hunter"))
        {
            StartCoroutine(swipeController());

            if (gameObject.tag=="ghost")
            {
                //SwerveMovement.instance.isSwipe = false;
                GameManager.instance.isContinue = false;
                circleP.SetActive(true);
                boomP.SetActive(true);
                StartCoroutine(delay());
            }
           
        }
        else if (other.CompareTag("fan"))
        {
            StartCoroutine(swipeController());

            if (gameObject.tag=="ghost")
            {
                //SwerveMovement.instance.isSwipe = false;
                GameManager.instance.isContinue = false;
                gameObject.transform.DOMoveY(12 , 1).SetEase(Ease.Linear);
                UiController.instance.OpenLosePanel();
               
            }
            
        }
        else if (other.CompareTag("boxbutton"))
        {//olmuyorrrrrrrrrrrrrr
            if (gameObject.tag == "Player")
            {
                SwerveMovement.instance.isSwipe = false;
                other.gameObject.GetComponent<Animator>().enabled = true;
                PlayerMovement.instance.speed = 0;
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                other.transform.GetChild(2).GetChild(0).GetComponent<Animator>().enabled = false;
                int boxChild = other.transform.GetChild(2).GetChild(0).childCount;

                other.transform.GetChild(2).GetChild(0).GetChild(0).DOLocalMove(new Vector3(0, 5.1f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(1).DOLocalMove(new Vector3(-1, 3.4f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(2).DOLocalMove(new Vector3(1, 3.4f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(3).DOLocalMove(new Vector3(-2,1.7f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(4).DOLocalMove(new Vector3(2, 1.7f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(5).DOLocalMove(new Vector3(0, 1.7f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(6).DOLocalMove(new Vector3(-1,0, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(7).DOLocalMove(new Vector3(1, 0f, -11.9f), .5f).SetEase(Ease.OutBounce);
                other.transform.GetChild(2).GetChild(0).GetChild(8).DOLocalMove(new Vector3(0, -1.7f, -11.9f), .5f).SetEase(Ease.OutBounce).OnComplete(() =>
                    {
                        SwerveMovement.instance.isSwipe = true;
                        anim.SetBool("run", true);
                        anim.SetBool("idle", false);
                        PlayerMovement.instance.speed = 10f; }); 




                        //for (int i = 0; i < boxChild; i++)
                        //{
                        //    other.transform.GetChild(2).GetChild(0).GetChild(i).DOLocalMove(new Vector3(Random.Range(-2.08f, 1.7f), Random.Range(-1, 1.2f), -9f), 1f).SetEase(Ease.OutBounce).OnComplete(() =>
                        //    {
                        //        SwerveMovement.instance.isSwipe = true;
                        //        anim.SetBool("run", true);
                        //        anim.SetBool("idle", false);
                        //        PlayerMovement.instance.speed = 6f;
                        //    }); ;
                        //}
                        //StartCoroutine(delirmelik());
                        // box.transform.GetChild(0).GetComponent<Animator>().SetBool("box",true);
                        //boxAnim.SetBool("box", true);
                    }


        }
        else if (other.CompareTag("basamakbutton"))
        {
            if (gameObject.tag == "Player")
            {
                SwerveMovement.instance.isSwipe = false;

                other.gameObject.GetComponent<Animator>().enabled = true;
                PlayerMovement.instance.speed = 0;
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                other.transform.GetChild(2).DOMoveY(-1.58f, 1).OnComplete(() => {
                    SwerveMovement.instance.isSwipe = true;

                    anim.SetBool("run", true);
                    anim.SetBool("idle", false);
                    PlayerMovement.instance.speed = 10f;
                });  
                    
            }
        }
        else if (other.CompareTag("boslukbutton"))
        {
            if (gameObject.tag=="Player")
            {
                SwerveMovement.instance.isSwipe = false;

                other.gameObject.GetComponent<Animator>().enabled = true;
                PlayerMovement.instance.speed = 0;
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                other.transform.GetChild(2).GetComponent<Collider>().enabled =false;
                other.transform.GetChild(3).DOMoveZ(other.transform.GetChild(3).position.z + 8f, 1).OnComplete(() => {
                    SwerveMovement.instance.isSwipe = true;
                    anim.SetBool("run", true);
                    anim.SetBool("idle", false);
                    PlayerMovement.instance.speed = 10;
                }); ;
            }
        }
        else if (other.CompareTag("fanbutton"))
        {
            if (gameObject.tag == "Player")
            {
                SwerveMovement.instance.isSwipe = false;

                other.gameObject.GetComponent<Animator>().enabled = true;
                PlayerMovement.instance.speed = 0;
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                other.transform.GetChild(2).DOMoveY(12f, .5f).SetEase(Ease.InFlash).OnComplete(() => {
                    SwerveMovement.instance.isSwipe = true;
                    anim.SetBool("run", true);
                    anim.SetBool("idle", false);
                    PlayerMovement.instance.speed = 10;
                }); ;
            }
        }
        else if (other.CompareTag("cagebutton"))
        {
            if (gameObject.tag=="Player")
            {
                SwerveMovement.instance.isSwipe = false;
                StartCoroutine(swipeController());
                other.gameObject.GetComponent<Animator>().enabled = true;

                PlayerMovement.instance.speed = 0;
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
                
                other.gameObject.transform.GetChild(2).DOMoveY(-1, .5f).OnComplete(() => {
                    SwerveMovement.instance.isSwipe = true;
                    other.gameObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                    other.gameObject.transform.GetChild(3).parent = other.gameObject.transform.GetChild(2);

                    //HumanManager.instance.transform.parent = other.gameObject.transform.GetChild(2);
                    other.gameObject.transform.GetChild(2).DOMoveY(17.5f, 2f);
                    anim.SetBool("run", true);
                    anim.SetBool("idle", false);
                    PlayerMovement.instance.speed = 10;
                });

            }

        }
        else if (other.CompareTag("finish"))
        {

            PlayerMovement.instance.speed = 12f;
            ruzgar.SetActive(true);
        
        }
        else if (other.CompareTag("1x"))
        {
            count++;
        } else if (other.CompareTag("2x"))
        {
            count++;

        }
        else if (other.CompareTag("3x"))
        {
            count++;

        }
        else if (other.CompareTag("4x"))
        {
            count++;

        }
        else if (other.CompareTag("5x"))
        {
            count++;

        }
        else if (other.CompareTag("6x"))
        {
            count++;

        }
        else if (other.CompareTag("7x"))
        {
            count++;

        }
        else if (other.CompareTag("8x"))
        {
            count++;
        } else if (other.CompareTag("9x"))
        {
            count++;
        } else if (other.CompareTag("10x"))
        {
            count++;
        }
        else if (other.CompareTag("oyunsonubasamak"))
        {
            if (gameObject.tag=="Player")
            {

                //SwerveMovement.instance.isSwipe = false;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
                gameObject.transform.DOMoveZ(gameObject.transform.position.z - 2f, .5f);

                anim.SetBool("run", false);
                anim.SetBool("sad", true);
                //anim.SetBool("idle", true);
                starP.SetActive(true);
                GameManager.instance.isContinue = false;
                PlayerMovement.instance.speed = 0;
                UiController.instance.OpenWinPanel();
                GameManager.instance.oyunsonu();
            }
        }
        else if (other.CompareTag("oyunsonumazgal"))
        {
            if (gameObject.tag == "ghost")
            {
                //SwerveMovement.instance.isSwipe = false;
                Debug.Log("oyunsonumazgal");
                idleGhost.enabled = false;
                TailController.instance.sagTail.TailAnimatorAmount = 1.3f;
                TailController.instance.solTail.TailAnimatorAmount = 1.3f;
                skinnedMeshRenderer.SetBlendShapeWeight(0, 0);
                StartCoroutine(OyunSonuhuplet ());
                gameObject.transform.DOMoveY(-1, .2f);
                // hüpp animasyonu :))
                GameManager.instance.isContinue = false;
                PlayerMovement.instance.speed = 0;
                UiController.instance.OpenWinPanel();
                GameManager.instance.oyunsonu();



               
            }
        }

    }
    //public IEnumerator delirmelik()
    //{
    //    //int boxChild = box.transform.childCount;
    //    int boxChild = box.transform.GetChild(0).childCount;
    //    Debug.Log(boxChild);
    //    // box.transform.GetChild(0).DOMove(new Vector3(Random.Range(-2.7f, 6f), -7, box.transform.localPosition.z), 1f);
    //    for (int i = 1; i < boxChild; i++)
    //    {
    //        Debug.Log("burda");
    //        Debug.Log(i);
    //        //box.transform.GetChild(0).GetChild(i).DOLocalMove(new Vector3(Random.Range(-2.7f, 6f), -7, box.transform.localPosition.z), 1f);
    //        yield return new WaitForSeconds(.5f);

    //        // box.transform.GetChild(i).DOMove(new Vector3( Random.Range(-2.7f, 6f), -7, box.transform.localPosition.z),1f);
    //    }
    //    yield return new WaitForSeconds(.5f);
    //}
   public IEnumerator swipeController()
    {
        Debug.Log("swipe false");
        SwerveMovement.instance.isSwipe = false;
        yield return new WaitForSeconds(1f);
        if (!UiController.instance.losePanel.activeInHierarchy)
        {
            Debug.Log("swipe true");
            SwerveMovement.instance.isSwipe = true;
        }
    }
    public IEnumerator stickmanAim(GameObject other) 
    {
        other.gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.transform.DOMoveZ(gameObject.transform.position.z - 2f, .5f);
        anim.SetBool("run", false);
        anim.SetBool("sad", true);
        //anim.SetBool("idle", true);
        starP.SetActive(true);
        yield return new WaitForSeconds(.1f);
        UiController.instance.OpenLosePanel();
    }
   
    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        ghost.SetActive(false);
        UiController.instance.OpenLosePanel();
    }

    public IEnumerator huplet()
    {

        while (blendOne<100)
        {

            Debug.Log("huplet");
                skinnedMeshRenderer.SetBlendShapeWeight(1, blendOne);
                blendOne+=3;
                if (blendOne >= 70)
                {
                    StartCoroutine(scale());
                }

            yield return new WaitForSeconds(.01f);
        }
        idleGhost.enabled = true;
        TailController.instance.sagTail.TailAnimatorAmount = 0;
        TailController.instance.solTail.TailAnimatorAmount = 0;
        UiController.instance.OpenLosePanel();
    }
    public IEnumerator OyunSonuhuplet()
    {

        while (blendOne<100)
        {

            Debug.Log("huplet");
                skinnedMeshRenderer.SetBlendShapeWeight(1, blendOne);
                blendOne+=3;
                if (blendOne >= 70)
                {
                    StartCoroutine(scale());
                }

            yield return new WaitForSeconds(.01f);
        }
        idleGhost.enabled = true;
        TailController.instance.sagTail.TailAnimatorAmount = 0;
        TailController.instance.solTail.TailAnimatorAmount = 0;
        UiController.instance.OpenWinPanel();
    }

    public IEnumerator scale()
    {

        while (blendTwo < 100)
        {

            skinnedMeshRenderer.SetBlendShapeWeight(2, blendTwo);
            blendTwo+=3;

          
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(0.01f);
        fýckP.SetActive(true);
        
    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>
    public void PreStartingEvents()
	{
        //transform.Rotate(0, 180, 0);
        cb.enabled =true;
        PlayerMovement.instance.speed = 10f;
        skinnedMeshRenderer.SetBlendShapeWeight(1, 0);
        skinnedMeshRenderer.SetBlendShapeWeight(2, 0);
        blendOne = 0;
        blendTwo = 0;
        PlayerMovement.instance.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
        GameManager.instance.isContinue = false;
        ghost.SetActive(false);
        player.SetActive(true);
        gameObject.tag = "Player";
        anim.SetBool("idle", true);
        anim.SetBool("fall", false);
        //anim.SetBool("run ", false);
        anim.SetBool("dance", false);
        anim.SetBool("sad", false);
        anim.SetBool("focus", false);
        circleP.SetActive(false);
        boomP.SetActive(false);
        starP.SetActive(false);
        fýckP.SetActive(false);
        ruzgar.SetActive(false);
        SwerveMovement.instance.isSwipe = true;
        UiController.instance.gamePanel.SetActive(true);
    }

    /// <summary>
    /// taptostart butonuna týklanýnca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlanýr, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
    {
        

        skinnedMeshRenderer.SetBlendShapeWeight(1, 0);
        skinnedMeshRenderer.SetBlendShapeWeight(2, 0);

        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
        PlayerMovement.instance.speed = 10f;
        //anim.SetBool("idle", false);
        anim.SetBool("run", true);
	}
}
