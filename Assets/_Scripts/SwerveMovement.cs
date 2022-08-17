using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    [SerializeField] private float swerveSpeed = .5f;
    [SerializeField] private float maxSwerveAmount = 2;
    [SerializeField] private float maxHorizontalDistance = 2;

    // Tiklama ile hizli yer degisiminin onune gecer.
    // TODO: katsayilari kontrol et.
    [SerializeField] private bool checkDistanceChange = true;
    [SerializeField] private float maxHorizontalChange = .5f;

    private float deltaPos;
    private float lastMousePosX;
    private float lastPositonChange;
    public bool isHuman;
    private float lastMousePosY, firstMousePosY;
    [SerializeField] private float swipeDistance = 20;
    public bool isSwipe=true;

    #region Singleton
    public static SwerveMovement instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion
    private void Update()
    {

        if (isSwipe) { 
            if (Input.GetMouseButtonDown(0))
            {
                // oyunu baþlatýyoruz.. karakter ileri hareket etmeye baþlýyor..
                if (!GameManager.instance.isContinue && UiController.instance.tapToStartPanel.activeInHierarchy) {
                    PlayerController.instance.PostStartingEvents();
                    UiController.instance.tapToStartPanel.SetActive(false);
                }
            
                lastMousePosX = Input.mousePosition.x;
                lastMousePosY = Input.mousePosition.y;
                firstMousePosY = Input.mousePosition.y;
             }
            else if (Input.GetMouseButton(0))
            {
                deltaPos = Input.mousePosition.x - lastMousePosX;
                lastMousePosX = Input.mousePosition.x;
                 lastMousePosY = Input.mousePosition.y;
                if (lastMousePosY - firstMousePosY > swipeDistance && !isHuman) // yukari
                {
                    firstMousePosY = lastMousePosY;
                    PlayerController.instance.Human();
                    Debug.Log("insan yap");
                }
                else if (firstMousePosY - lastMousePosY > swipeDistance && isHuman) // asagi
                {
                    firstMousePosY = lastMousePosY;
                    PlayerController.instance.Ghost();
                    Debug.Log("hayalet yap");
                }

             }
            else if (Input.GetMouseButtonUp(0))
            {
                deltaPos = 0;
                firstMousePosY = lastMousePosY;
            }

            var swerve = Time.deltaTime * swerveSpeed * deltaPos;
            swerve = Mathf.Clamp(swerve, -maxSwerveAmount, maxSwerveAmount);

            var x = transform.position.x + swerve;
            if (x < maxHorizontalDistance && x > -maxHorizontalDistance)
                if (checkDistanceChange)
                {
                    if (Mathf.Abs(x - lastPositonChange) < maxHorizontalChange) transform.Translate(swerve, 0, 0);
                }
                else
                    transform.Translate(swerve, 0, 0);

            lastPositonChange = x;
        }
    }

}
