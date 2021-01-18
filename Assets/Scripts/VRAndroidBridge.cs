using UnityEngine;
using UnityEngine.UI;

public class VRAndroidBridge : MonoBehaviour
{
    [SerializeField] private Button startListeningBtn = null;
    [SerializeField] private bool continuousListeningTgle = true;
    [SerializeField] private Text resultsTxt = null;

    public static string ANDROIDBRIDGE_GO_NAME = "AndroidBridge";

    public interface IAndroidBridge
    {
        void OnResult( string recognizedResult );
    }

    public static T AndroidCall<T>( string method, params object[] args )
    {
        using ( AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
        {
            using ( AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>( "currentActivity" ) )
            {
                if ( args != null )
                    return jo.Call<T>( method, args );
                else
                    return jo.Call<T>( method );
            }
        }
    }

    public static void AndroidCall( string method, params object[] args )
    {
        using ( AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
        {
            using ( AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>( "currentActivity" ) )
            {
                if ( args != null )
                    jo.Call( method, args );
                else
                    jo.Call( method );
            }
        }
    }

    public static void AndroidStaticCall( string method, params object[] args )
    {
        using ( AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
        {
            using ( AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>( "currentActivity" ) )
            {
                if ( args != null )
                    jo.CallStatic( method, args );
                else
                    jo.CallStatic( method );
            }
        }
    }

    public static void AndroidRunnableCall( string method, params object[] args )
    {
        using ( AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
        {
            using ( AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>( "currentActivity" ) )
            {
                if ( args != null )
                {
                    jo.Call( "runOnUiThread", new AndroidJavaRunnable( () =>
                    {
                        AndroidCall( method, args );
                    } ) );
                }
                else
                {
                    jo.Call( "runOnUiThread", new AndroidJavaRunnable( () =>
                    {
                        AndroidCall( method );
                    } ) );
                }
            }
        }
    }


    void Start()
    {
        this.gameObject.name = ANDROIDBRIDGE_GO_NAME;
        
        SetContinuousListening();
        
        startListeningBtn.onClick.AddListener( StartListening );
    }

    void DoActivateTrigger()
    {
        StartListening();
    }

    private void StartListening()
    {
        AndroidRunnableCall( "StartListening" );
    }

    private void SetContinuousListening()
    {
        AndroidCall( "SetContinuousListening", continuousListeningTgle );
    }

    public void OnResult( string recognizedResult )
    {
        char[] delimiterChars = { '~' };
        string[] result = recognizedResult.Split( delimiterChars );

        resultsTxt.text = "";
        for ( int i = 0; i < result.Length; i++ )
        {
            resultsTxt.text += result[i] + '\n';
        }

        SendMessageUpwards( "ParsePossibleCommand", result );
    }

}
