using System;

public class Student {
    int nr;
    string name;
    int group;
    string githubId;

    public Student(int nr, string name, int group, string githubId)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.githubId = githubId;
    }
    
    public int Nr { get {return nr; } }
    public string Name { get {return name; } }
    public int Group { get {return group; } }
    public string GithubId { get {return githubId; } }

}

class Point{
    public readonly int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public double Module{
        get{
            return Math.Sqrt(x*x + y*y);
        }
    }
    
}

class Account {
    public static readonly int CODE = 4342;
    public long balance;
    public Account(long b) { balance = b; }
}

public class App {
    public static void Main(){
		
        Point p = new Point(7, 9);
        Student s = new Student(154134, "Ze Manel", 5243, "ze");
        
        Student[] classroom = {
            new Student(154134, "Ze Manel", 5243, "ze"),
            new Student(765864, "Maria El", 4677, "ma"),
            new Student(456757, "Antonias", 3153, "an"),
        };
        
        Account a = new Account(1300);
        Console.WriteLine(p);
        Console.WriteLine(s);
        Console.WriteLine(a);
		Logger l=new Logger();
        l.Log(p);
        l.Log(s);
        l.Log(a);
		
		Logger l1=new Logger();
		l1.ReadFields();
        l1.Log(p);
        l1.Log(s);
        l1.Log(a);
		
		Logger l2=new Logger();
		l2.ReadMethods();
        l2.Log(p);
        l2.Log(s);
        l2.Log(a);
		
		Logger l3=new Logger();
		l3.ReadProperties();
        l3.Log(p);
        l3.Log(s);
        l3.Log(a);
		
		Logger l4=new Logger();
		l4.ReadMethods();
		l4.ReadFields();
		l4.ReadProperties();
        l4.Log(p);
        l4.Log(s);
        l4.Log(classroom);
    }
}