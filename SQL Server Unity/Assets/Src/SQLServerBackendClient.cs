using Newtonsoft.Json;
using SQLServer.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Unity.Jobs;
using UnityEngine;

public class SQLServerBackendClient : MonoBehaviour
{
	public static SQLServerBackendClient Client { get; private set; }
	public HttpClient HttpClient { get; private set; }

	private List<DataSpace> dataSpaces;
	private List<Table> tables;

	public string Database { get; set; }
	public string Server { get; set; }
	public string UserID { get; set; }
	public string Password { get; set; }
	public string ConnectionString
	{
		get
		{
			if(string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(UserID))
			{
				return $"Server={Server};Database={Database};Trusted_Connection=true;";
			}
			else
			{
				return $"Server={Server};Database={Database};User Id={UserID};Password={Password}";
			}
		}
	}

	private void Awake()
	{
		if(Client == null)
		{
			DontDestroyOnLoad(this);
			Client = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	private void Start()
	{
		HttpClient = new HttpClient();
		HttpClient.BaseAddress = new Uri("http://localhost:55683");
	}
	
	// Update is called once per frame
	private void Update()
	{
		
	}

	public List<Table> RetrieveTables()
	{
		if(tables == null)
		{
			var response = HttpClient.GetAsync("api/metadata/tables").Result;
			var content = response.Content.ReadAsStringAsync().Result;
			tables = JsonConvert.DeserializeObject<List<Table>>(content);
		}

		return tables;
	}
}

public struct RetrieveTablesJob : IJob
{
	private string url;
	public string Content { get; private set; }

	public RetrieveTablesJob(string url)
	{
		this.url = url;
		Content = null;
	}

	public void Execute()
	{
		var response = SQLServerBackendClient.Client.HttpClient.GetAsync(url).Result;
		Content = response.Content.ReadAsStringAsync().Result;
	}
}
