using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
//#if FACEBOOK
using Facebook.Unity;
//#endif

/// <summary>
/// Facebook manager
/// </summary>
	public class FacebookManager : MonoBehaviour
	{
		private bool loginEnable;

		private string userID;

		private string lastResponse { get; set; } = string.Empty;

		private string status { get; set; } = "Ready";

		private bool loginOnce;

		private string userName = null;

		private Sprite profilePic = null;

		public string UserName
		{
			get {return userName;}
		}

		public Sprite ProfilePic
		{
			get {return profilePic;}
		}

		private void Start() 
        {
            
        }

		void Awake()
		{
			
		}

		void OnEnable()
		{
			CallFBInit ();
		}


		void OnDisable()
		{

		}

		public void CallFBInit()
		{
			Debug.Log("init facebook");
			if (!FB.IsInitialized)
			{
				FB.Init(OnInitComplete, OnHideUnity);
			}
			else
			{
				FB.ActivateApp();
			}
		}

		private void OnInitComplete()
		{
			Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
			if (FB.IsLoggedIn)
			{
				loggedSuccefull();
			}
			else
			{
				CallFBLogin ();
			}


		}

		private void OnHideUnity(bool isGameShown)
		{
			Debug.Log("Is game showing? " + isGameShown);
		}

		public void CallFBLogin()
		{
			if (!loginOnce)
			{
				loginOnce = true;

				Debug.Log("login");

				FB.LogInWithReadPermissions(new List<string> { "public_profile", "email", "user_friends" }, HandleResult);
			}
		}

		public void CallFBLogout()
		{
			FB.LogOut();

			SceneManager.LoadScene("game");
		}

		private void HandleResult(IResult result)
		{
			loginOnce = false;

			if (result == null)
			{
				lastResponse = "Null Response\n";

				Debug.Log(lastResponse);

				return;
			}

			// Some platforms return the empty string instead of null.
			if (!string.IsNullOrEmpty(result.Error))
			{
				status = "Error - Check log for details";

				lastResponse = "Error Response:\n" + result.Error;

				Debug.Log(result.Error);
			}
			else if (result.Cancelled)
			{
				status = "Cancelled - Check log for details";

				lastResponse = "Cancelled Response:\n" + result.RawResult;

				Debug.Log(result.RawResult);
			}
			else if (!string.IsNullOrEmpty(result.RawResult))
			{
				status = "Success - Check log for details";

				lastResponse = "Success Response:\n" + result.RawResult;

				loggedSuccefull();//1.3
			}
			else
			{
				lastResponse = "Empty Response\n";

				Debug.Log(lastResponse);
			}
		}

		private void loggedSuccefull()
		{
			PlayerPrefs.SetInt("Facebook_Logged", 1);

			PlayerPrefs.Save();

			userID = AccessToken.CurrentAccessToken.UserId;

			getPicture(AccessToken.CurrentAccessToken.UserId);
		}

		private void getUserName()
		{
			FB.API("/me?fields=first_name", HttpMethod.GET, gettingNameCallback);
		}

		private void gettingNameCallback(IGraphResult result)
		{
			if (string.IsNullOrEmpty(result.Error))
			{
				IDictionary dict = result.ResultDictionary as IDictionary;

				userName = dict["first_name"].ToString();
			}

		}

		private IEnumerator loadPicture(string url)
		{
			WWW www = new WWW(url);

			yield return www;

			Texture2D texture = www.texture;

			profilePic = Sprite.Create(texture, new Rect(0, 0, 128, 128), new Vector2(0, 0), 1f);
		}


		private void getPicture(string id)
		{
			FB.API("/" + id + "/picture?g&width=128&height=128&redirect=false", HttpMethod.GET, profilePhotoCallback);
		}

		private void profilePhotoCallback(IGraphResult result)
		{
			if (string.IsNullOrEmpty(result.Error))
			{
				Dictionary<String, object> dic = result.ResultDictionary["data"] as Dictionary<string, object>;

				string url = dic.Where(i => i.Key == "url").First().Value as string;

				StartCoroutine(loadPicture(url));
			}
		}

		public void APICallBack(IGraphResult result)
		{
			Debug.Log(result);
		}
	}