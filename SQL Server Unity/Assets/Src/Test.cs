using Newtonsoft.Json;
using SQLServer.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var client = new HttpClient();
		client.BaseAddress = new Uri("http://localhost:55683");
		var response = client.GetAsync("api/metadata/tables").Result;
		var content = response.Content.ReadAsStringAsync().Result;
		print("Test " + content);

		var result = JsonConvert.DeserializeObject<List<Table>>(content);
		foreach(var table in result)
		{
			print(table.TableName);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
