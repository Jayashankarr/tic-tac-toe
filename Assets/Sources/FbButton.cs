using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FbButton : MonoBehaviour
{
    public void OnFbButtonClick ()
    {
        GameManager.Instance.EnableFacebookManagerGameobject();
    }
}
