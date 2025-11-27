using UnityEngine;

public class MobileInputManager : MonoBehaviour
{
    public static MobileInputManager Instance { get; private set; }
    
    [SerializeField] private CanvasGroup mobileInputContainer;
    public bool TestInIspector = false;
    private bool activeContainer;
    private bool Mobile = false;
    private int FirstCheckMobile = 0;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ActiveMobileContainer()
    {
        activeContainer = !activeContainer;
        if (IsMobileDevice())
        {
            mobileInputContainer.alpha = activeContainer ? 1 : 0;
            mobileInputContainer.blocksRaycasts = activeContainer ? true : false;
        }
    }


    private void Start()
    {
        // Вызывает JavaScript для проверки сенсорности устройства
        Application.ExternalEval("checkForTouchDevice()");
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();

#endif
    public bool IsMobileDevice()
    {
        if (FirstCheckMobile == 1)
            return Mobile;
        FirstCheckMobile = 1;


        var isMobile = false;

#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif


        if (isMobile)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Mobile = true;
            return true;
        }
        else
        {
            if (TestInIspector)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            Mobile = TestInIspector;
            return TestInIspector;
        }
    }
}