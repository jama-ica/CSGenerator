using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace CSGen
{
	/// <summary>
	/// 
	/// </summary>
	public class CSProject
	{
		static CSProject instance = new CSProject();
		
		// Constructor
		public CSProject()
		{
		}

		public static bool gen(string arg)
		{
			return instance.generateProject(arg);
		}

		public bool generateProject(string arg)
		{
			List<string> list = readCSClasses(arg);
			foreach(var item in list)
			{
				bool ret = CSClass.gen(item);
				if(!ret){
					Console.WriteLine("!ret : item = " + item);
					return false;
				}
			}
			return true;
		}

		private List<string> readCSClasses(string filePath)
		{
			if(!File.Exists(filePath)){
				Console.WriteLine("!filePath = " + filePath);
				return null;
			}
			List<string> list = new List<string>();
			var encoding = System.Text.Encoding.GetEncoding("UTF-8");
			List<string> previous = null;
			using (var reader = new System.IO.StreamReader(filePath, encoding))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					List<string> parameters = convertToParams(previous, line);
					List<string> dirParameters = getDirParams(parameters);
					previous = dirParameters;
					string fileName = getFileName(parameters);
					if(fileName == null){
						continue;
					}
					Console.WriteLine("fileName = " + fileName);
					string text = convertToText(dirParameters, fileName);
					list.Add(text);
				}
			}
			return list;
		}

		private List<string> convertToParams(List<string> previous, string text)
		{
			if(text == null){
				return null;
			}
			if(text == string.Empty){
				return null;
			}
			int tabCount = 0;
			for(int i = 0 ; i < text.Length ; i++){
				if(text[i] == '\t'){
					tabCount++;
				}else{
					break;
				}
			}
			List<string> list = new List<string>();
			for(int i = 0 ; i < tabCount ; i++){
				Console.WriteLine("previous.Count = " + previous.Count + ", text = " + text + ",tabCount = " + tabCount);
				list.Add(previous[i]);
			}
			list.Add(text.Substring(tabCount));
			return list;
		}

		private string getFileName(List<string> parameters)
		{
			if(parameters == null){
				Console.WriteLine("!params == null");
				return null;
			}
			if(parameters.Count == 0){
				Console.WriteLine("!parameters.Count == 0");
				return null;
			}
			string lastItem = parameters[parameters.Count - 1];
			if(!lastItem.StartsWith("-")){
				return null;
			}
			return lastItem.Substring(1);
		}

		private List<string> getDirParams(List<string> parameters)
		{
			List<string> dirParameters = new List<string>();
			for(int i = 0 ; i < parameters.Count ; i++)
			{
				string item = parameters[i];
				if(i < parameters.Count - 1)
				{
					dirParameters.Add(item);
				}
				else
				{
					if(item.StartsWith("-"))
					{
						;
					}
					else
					{
						dirParameters.Add(item);
					}
				}
			}
			return dirParameters;
		}

		private string convertToText(List<string> dirParameters, string fileName)
		{
			if(fileName == null){
				return null;
			}
			StringBuilder builder = new StringBuilder();
			foreach(var dir in dirParameters)
			{
				builder.Append(dir);
				builder.Append("/");
			}
			builder.Append(fileName);
			return builder.ToString();
		}

	}
}
