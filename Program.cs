using System;

namespace CSGen
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args == null){
				Console.WriteLine("!args = null");
				return; 
			}
			if(1 >= args.Length){
				Console.WriteLine("!args = " + args);
				return;
			}
			string option = args[0];
			string value = null;
			if(2 <= args.Length){
				value = args[1];
			}
			Run(option, value);
		}

		static void Run(string option, string value)
		{
			switch(option)
			{
			case "c":
				CreateClass(value);
				break;
			case "p":
				CreateProject(value);
				break;
			case "h":
				break;
			default:
				Console.WriteLine("!option = " + option);
				break;
			}
		}

		static void CreateClass(string arg)
		{
			if(arg == null){
				Console.WriteLine("!arg = null");
				return;
			}
			if(arg == string.Empty){
				Console.WriteLine("!arg = string.Empty");
				return;
			}
			bool ret = CSClass.gen(arg);
			if(!ret){
				Console.WriteLine("!Faild");
			}
		}

		static void CreateProject(string arg)
		{
			if(arg == null){
				Console.WriteLine("!arg = null");
				return;
			}
			if(arg == string.Empty){
				Console.WriteLine("!arg = string.Empty");
				return;
			}
			string filePath = arg;
			bool ret = CSProject.gen(filePath);
			if(!ret){
				Console.WriteLine("!Faild");
			}
		}
	}
}
