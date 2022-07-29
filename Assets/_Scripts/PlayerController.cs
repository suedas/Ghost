using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectibleGhost"))
        {
            ghost.SetActive(true);
            gameObject.tag = "ghost";
            player.SetActive(false);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("collectiblePlayer"))
        {
            player.SetActive(true);
            gameObject.tag = "Player";
            ghost.SetActive(false);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("obstacleghost"))
        {
            if (gameObject.tag=="ghost")
            {
                Debug.Log("ghost animasyonlarý yap");

            }
            else if (gameObject.tag=="Player")
            {
                Debug.Log("player animasyonlarý yap");
            }
        }
        else if (other.CompareTag("obstaclePlayer"))
        {
            if (gameObject.tag == "ghost")
            {
                Debug.Log("ghost animasyonlarý yap");

            }
            else if (gameObject.tag == "Player")
            {
                Debug.Log("player animasyonlarý yap");
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
            GameManager.instance.isContinue = false;
            Debug.Log(GameManager.instance.levelScore);
            if (GameManager.instance.levelScore > 10) UiController.instance.OpenWinPanel();
            else UiController.instance.OpenLosePanel();
        }
    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>
    public void PreStartingEvents()
	{
        PlayerMovement.instance.transform.position = Vector3.zero;
        PlayerController.instance.transform.position = Vector3.zero;
        GameManager.instance.isContinue = false;
        ghost.SetActive(false);
        player.SetActive(true);
        gameObject.tag = "Player";
       
	}

    /// <summary>
    /// taptostart butonuna týklanýnca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlanýr, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
	{
        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
	}
}
