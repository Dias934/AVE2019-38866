using System;

public class App{

	public static void Main(String []args){
		Foo(12);
		Foo(19);
	}
	
	private static void Foo(int nr){
		Console.Write(nr);
		Console.Write(", ");
		if(nr == 1)
			Console.WriteLine();
		else if(nr%2==0)
			Foo(nr/2);
		else 
			Foo(nr*3+1);
	}

}