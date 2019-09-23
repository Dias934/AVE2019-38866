using System;

public class App{

	public static void Main(String []args){
		Console.WriteLine(Foo("aa"));
	}
	
	private static bool Foo(String message){
		if(message.Length==1) return true;
		if(message[0]!=message[message.Length-1]) return false;
		if(message.Length==2) return true;
		return Foo(message.Substring(1,message.Length-2));
	}
}