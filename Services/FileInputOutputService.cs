﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toDoList.Models;

namespace toDoList.Services
{
	class FileInputOutputService
	{
		private readonly string PATH;

		public FileInputOutputService(string path)
		{
			PATH = path;
		}
		public BindingList<TodoModel> LoadData()
		{
			var fileExists = File.Exists(PATH);
			if (!fileExists)
			{
				File.CreateText(PATH).Dispose();
				return new BindingList<TodoModel>();
			}
			using (var reader = File.OpenText(PATH))
			{
				var fileText = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
			}
		}
		public void SaveData(object _todoData)
		{
			using (StreamWriter writer = File.CreateText(PATH))
			{
				string output = JsonConvert.SerializeObject(_todoData);
				writer.Write(output);
			}
		}
	}
}
