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
                Debug.Log("ghost animasyonlar� yap");

            }
            else if (gameObject.tag=="Player")
            {
                Debug.Log("player animasyonlar� yap");
            }
        }
        else if (other.CompareTag("obstaclePlayer"))
        {
            if (gameObject.tag == "ghost")
            {
                Debug.Log("ghost animasyonlar� yap");

            }
            else if (gameObject.tag == "Player")
            {
                Debug.Log("player animasyonlar� yap");
            }
        }
        else if (other.CompareTag("finish"))
        {
            // oyun sonu olaylari... animasyon.. score.. panel acip kapatmak
            // oyunu kazandi mi kaybetti mi kontntrolu gerekirse yapilabilir.
            // player durdurulur. tagi finish olan obje level prefablarinin icinde yolun sonundad�r.
            // ornek olarak asagidaki kodda score 10 dan buyukse kazan degilse kaybet dedik ancak
            // baz� oyunlarda farkli parametlere g�re kontrol etmek veya oyun sonunda karakterin yola devam etmesi gibi
            // durumlarda developer buray� kendisi duzenlemelidir.
            GameManager.instance.isContinue = false;
            Debug.Log(GameManager.instance.levelScore);
            if (GameManager.instance.levelScore > 10) UiController.instance.OpenWinPanel();
            else UiController.instance.OpenLosePanel();
        }
    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlan�r. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon i�inde yapilir.
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
    /// taptostart butonuna t�klan�nca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlan�r, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
	{
        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
	}
}
