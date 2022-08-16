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
    //public TailAnimator2 sagTail, solTail;
   
    private void Start()
    {
        
        anim =player.GetComponent<Animator>();
        anim.SetBool("run", false);

     
        
        chest.instance.confetiP.SetActive(false);
        chest.instance.magicP.SetActive(false);
        chest.instance.dolarP.SetActive(false);
        chest.instance.chestAnim.enabled = false;
        
        //anim.SetBool("idle", true);
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
        if (other.CompareTag("collectibleGhost"))
        {
            //GameManager.instance.IncreaseScore();
            //ghost.SetActive(true);
            //gameObject.tag = "ghost";
            //player.SetActive(false);
            //other.gameObject.SetActive(false);
           // Destroy(other.gameObject);

        }
        else if (other.CompareTag("collectiblePlayer"))
        {
           // GameManager.instance.IncreaseScore();
            //player.SetActive(true);
            //gameObject.tag = "Player";
            //anim.SetBool("run", true);
            //ghost.SetActive(false);
            //other.gameObject.SetActive(false);

           //Destroy(other.gameObject);

        }
        else if (other.CompareTag("diamond"))
        {
            if (gameObject.tag=="Player")
            {
                other.gameObject.transform.DOMove(diamondTarget.transform.position, .5f).OnComplete(()=> { other.gameObject.SetActive(false); });
                other.gameObject.transform.DOScale(.2f, .2f);
                GameManager.instance.IncreaseScore();
            }
        }
        else if (other.CompareTag("duvar"))
        {
           
            if (gameObject.tag=="Player")
            {//buraya girmiyor
                other.gameObject.GetComponent<Collider>().isTrigger = false;
                gameObject.transform.DOMoveZ(gameObject.transform.position.z - 3f, 2f);
                anim.SetBool("run", false);
                anim.SetBool("sad", true);
                //anim.SetBool("idle", true);
                starP.SetActive(true);
                UiController.instance.OpenLosePanel();
            }
        }
        else if (other.CompareTag("basamak"))
        {
            
            if (gameObject.tag=="Player")
            {//buraya girmiyor
                StartCoroutine(stickmanAim(other.gameObject));
           
            }
        }
      
        else if (other.CompareTag("bosluk"))
        {
            if (gameObject.tag=="Player")
            {
                //gameObject.GetComponent<Rigidbody>().useGravity = true;
                anim.SetBool("fall", true);
                anim.SetBool("run", false);
                gameObject.transform.DOMove(new Vector3(transform.position.x, -5f, transform.position.z + 8), 1f);
                UiController.instance.OpenLosePanel();
               
               // cb.enabled = false;

            }
         
        }
        
        else if (other.CompareTag("mazgal"))
        {
            if (gameObject.tag=="ghost")
            {
                idleGhost.enabled = false;
                TailController.instance.sagTail.TailAnimatorAmount = 1.3f;
                TailController.instance.solTail.TailAnimatorAmount = 1.3f;
                skinnedMeshRenderer.SetBlendShapeWeight(0, 0);
                StartCoroutine(huplet());
                gameObject.transform.DOMoveY(-1, .2f);
                UiController.instance.OpenLosePanel();
                // hüpp animasyonu :))
                GameManager.instance.isContinue = false;
                PlayerMovement.instance.speed = 0;
              
            }
          
        }
        else if (other.CompareTag("hunter"))
        {
            if (gameObject.tag=="ghost")
            {
                GameManager.instance.isContinue = false;
                circleP.SetActive(true);
                boomP.SetActive(true);
                StartCoroutine(delay());
            }
           
        }
        else if (other.CompareTag("fan"))
        {
            if (gameObject.tag=="ghost")
            {
                GameManager.instance.isContinue = false;
                gameObject.transform.DOMoveY(9, 2);
                UiController.instance.OpenLosePanel();
               
            }
            
        }
        else if (other.CompareTag("finish"))
        {
            // oyun sonu olaylari... animasyon.. score.. panel acip kapatmak
            // oyunu kazandi mi kaybetti mi kontntrolu gerekirse yapilabilir.
            // player durdurulur. tagi finish olan obje level prefablarinin icinde yolun sonundadýr.
            // ornek olarak asagidaki kodda score 10 dan buyukse kazan degilse kaybet dedik ancak
            // bazý oyunlarda farkli parametlere göre kontrol etmek veya oyun sonunda karakterin yola devam etmesi gibi
            // durumlarda developer burayý kendisi duzenlemelidir.

            PlayerMovement.instance.speed = 10f;
            ruzgar.SetActive(true);
            //if (gameObject.tag == "Player")
            //{
            //    transform.Rotate(0, -180, 0);     
            //    anim.SetBool("run", false);
            //    anim.SetBool("dance", true);
            //}
            
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
                other.gameObject.GetComponent<Collider>().isTrigger = false;
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
                Debug.Log("oyunsonumazgal");
                idleGhost.enabled = false;
                skinnedMeshRenderer.SetBlendShapeWeight(0, 0);
                StartCoroutine(huplet());
                // hüpp animasyonu :))
                GameManager.instance.isContinue = false;
                PlayerMovement.instance.speed = 0;
                UiController.instance.OpenWinPanel();
                GameManager.instance.oyunsonu();
            }
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
        //UiController.instance.OpenLosePanel();

    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>
    public void PreStartingEvents()
	{
        //transform.Rotate(0, 180, 0);
        PlayerMovement.instance.speed = 6f;
        skinnedMeshRenderer.SetBlendShapeWeight(1, 0);
        skinnedMeshRenderer.SetBlendShapeWeight(2, 0);
        blendOne = 0;
        blendTwo = 0;
        PlayerMovement.instance.transform.position = Vector3.zero;
        PlayerController.instance.transform.position = Vector3.zero;
        GameManager.instance.isContinue = false;
        ghost.SetActive(false);
        player.SetActive(true);
        gameObject.tag = "Player";
        anim.SetBool("idle", true);
        anim.SetBool("fall", false);
        anim.SetBool("run ", false);
        anim.SetBool("dance", false);
        anim.SetBool("sad", false);
        anim.SetBool("focus", false);
        circleP.SetActive(false);
        boomP.SetActive(false);
        starP.SetActive(false);
        fýckP.SetActive(false);
        ruzgar.SetActive(false);
       
        //if (chest.instance.chestAnim.enabled ==true)
        //{
        //    chest.instance.confetiP.SetActive(false);
        //    chest.instance.magicP.SetActive(false);
        //    chest.instance.dolarP.SetActive(false);
        //    chest.instance.chestAnim.enabled = false;

        //}




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
        PlayerMovement.instance.speed = 6f;
        //anim.SetBool("idle", false);
        anim.SetBool("run", true);
	}
}
