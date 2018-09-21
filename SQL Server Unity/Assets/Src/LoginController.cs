using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
	public static LoginController Controller;

	private LoginCanvas loginCanvas;
	public LoginCanvas LoginCanvas
	{
		get
		{
			if(loginCanvas == null)
			{
				loginCanvas = GameObject.Find("LoginCanvas").GetComponent<LoginCanvas>();
			}
			return loginCanvas;
		}
	}

	private void Awake()
	{
		Controller = this;
	}

	// Use this for initialization
	private void Start ()
	{
		LoginCanvas.LoginButton.onClick.AddListener(() => 
		{
			SQLServerBackendClient.Client.Server = LoginCanvas.ServerInputField.text;
			SQLServerBackendClient.Client.Database = LoginCanvas.DatabaseInputField.text;
			var tables = SQLServerBackendClient.Client.RetrieveTables();

			SceneManager.LoadSceneAsync("MainScene");
		});
	}
	
	// Update is called once per frame
	private void Update ()
	{
		
	}
}