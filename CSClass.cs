using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace CSGen
{
	/// <summary>
	/// 
	/// </summary>
	public class CSClass
	{
		static CSClass instance = new CSClass();
		
		// Constructor
		private CSClass()
		{
		}

		public static bool gen(string arg)
		{
			string[] splitedText = arg.Split('/');
			string namespaceName = splitedText[0];
			string className = splitedText[splitedText.Length - 1];
			string rootPath = @"./output";
			string path = rootPath + "/" + arg + ".cs";
			return instance.generateClassFile(path, namespaceName, className);
		}

		public bool generateClassFile(string path, string namespaceName, string className)
		{
			namespaceName = toTitleCase(namespaceName);
			className = toTitleCase(className);
			string classText = createClassText(namespaceName, className);
			return writeText(path, classText);
		}

		private string toTitleCase(string text)
		{
			StringBuilder builder = new StringBuilder();
			for(int i = 0 ; i < text.Length ; i++){
				if(i == 0){
					builder.Append(text[i].ToString().ToUpper());
				}else{
					builder.Append(text[i].ToString());
				}
			}
			return builder.ToString();
		}

		private string createClassText(string namespaceName, string className)
		{
			var text = $@"using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RLTPS.{namespaceName}
{{

	/// <summary>
	/// 
	/// </summary>
	public class {className}
	{{
		
		// Constructor
		public {className}()
		{{
		}}

		
		
	}}
}}
";
			return text;
		}

		private bool writeText(String path, string text)
		{
			DirectoryInfo dir = Directory.GetParent(path);
			if(!Directory.Exists(dir.FullName)){
				Directory.CreateDirectory(dir.FullName);
			}
			File.WriteAllText(path, text);
			return true;
		}
		
	}
}