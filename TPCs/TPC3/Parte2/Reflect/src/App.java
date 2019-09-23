import java.lang.reflect.Field;
import java.lang.reflect.Method;

class A{}
class B extends A{}
class C extends B{
	public int x, y;
	public void Foo() {}
}

public class App {

	public static void main(String[] args) {
		PrintBaseTypes("Ola");
		PrintBaseTypes(19);
		PrintBaseTypes(new C());
		//PrintMembers(new C());
		PrintMethods(new C());
		PrintFields(new C());
	}
	
	public static void PrintMembers(Object obj) {
		System.out.print("Members: ");
		for (Method m : obj.getClass().getMethods()) {
			System.out.print(m.getName()+" ");
		}
		System.out.println();
	}
	
	public static void PrintMethods(Object obj) {
		System.out.print("Methods: ");
		for (Method m : obj.getClass().getMethods()) {
			System.out.print(m.getName()+" ");
		}
		System.out.println();
	}
	
	public static void PrintFields(Object obj) {
		System.out.print("Fields: ");
		for (Field f : obj.getClass().getFields()) {
			System.out.print(f.getName()+" ");
		}
		System.out.println();
	}
	
	public static void PrintBaseTypes(Object obj) {
		Class c=obj.getClass();
		do {
			System.out.print(c.getSimpleName()+" ");
			PrintInterfaces(c);
			c=c.getSuperclass();
		}while(c!=null);
		System.out.println();
	}
	
	public static void PrintInterfaces(Class c) {
	
	}
}
