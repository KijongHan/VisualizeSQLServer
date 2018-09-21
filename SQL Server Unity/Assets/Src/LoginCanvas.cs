using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginCanvas : MonoBehaviour
{
	public InputField ServerInputField { get; set; }
	public InputField DatabaseInputField { get; set; }
	public InputField UserIDInputField { get; set; }
	public InputField PasswordInputField { get; set; }

	public Button LoginButton { get; set; }

	private void Awake()
	{
		LoginButton = GameObject.Find("LoginButton").GetComponent<Button>();
		ServerInputField = GameObject.Find("ServerInputField").GetComponent<InputField>();
		DatabaseInputField = GameObject.Find("DatabaseInputField").GetComponent<InputField>();
		UserIDInputField = GameObject.Find("UserIDInputField").GetComponent<InputField>();
		PasswordInputField = GameObject.Find("PasswordInputField").GetComponent<InputField>();
	}

	private void OnDestroy()
	{
		LoginButton.onClick.RemoveAllListeners();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
