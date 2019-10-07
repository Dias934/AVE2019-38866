using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class Logger {
	
	private List<Readers> lr; //list of readers
	
	public Logger(){lr=new List<Readers>();}
	
	public void ReadFields(){
		if(!lr.Exists(x => x.GetType()==typeof(ReaderFields)))
			lr.Add(new ReaderFields());
	}
	
	public void ReadMethods(){
		if(!lr.Exists(x => x.GetType()==typeof(ReaderMethods)))
			lr.Add(new ReaderMethods());
	}
	
	public void ReadProperties(){
		if(!lr.Exists(x => x.GetType()==typeof(ReaderProperties)))
			lr.Add(new ReaderProperties());
	}
	
	public void ReadPropertiesAnnotated(){
		if(!lr.Exists(x => x.GetType()==typeof(ReaderPropertiesAnnotated)))
			lr.Add(new ReaderPropertiesAnnotated());
	}
	
	
	
	public void Log(object o){
	Type t = o.GetType();
       if(t.IsArray) LogArray((IEnumerable) o);
       else {
			List <IGetter> lig=new List<IGetter>();
			foreach(Readers r in lr){
				lig.AddRange(r.Read(t));
			}
            LogObject(o, lig);
        }
	}
    
    public void LogArray(IEnumerable o) {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
        List <IGetter> lig=new List<IGetter>();
		foreach(Readers r in lr){
			lig.AddRange(r.Read(elemType));
		}
        Console.WriteLine("Array of " + elemType.Name + "[");
        foreach(object item in o) LogObject(item, lig); // * 
        Console.WriteLine("]");
    }
    
    public void LogObject(object o, IEnumerable<IGetter> gs) {
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(IGetter g in gs) {
            Console.Write(g.GetName() + ": ");
            Console.Write(g.GetValue(o) + ", ");
        }
        Console.WriteLine("}");
    }
}

public interface Readers{
	IEnumerable<IGetter> Read(Type t);
}

public class ReaderFields:Readers{
	public IEnumerable<IGetter> Read(Type t){
		List<IGetter> l = new List<IGetter>();
        foreach(FieldInfo m in t.GetFields()) {
            l.Add(new GetterField(m));
        }
        return l;
	}
}

public class ReaderMethods:Readers{
	public IEnumerable<IGetter> Read(Type t){
		List<IGetter> l = new List<IGetter>();
        foreach(MethodInfo m in t.GetMethods()) {
            if(m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                l.Add(new GetterMethod(m));
            }
        }
        return l;
	}
}

public class ReaderProperties:Readers{
	public IEnumerable<IGetter> Read(Type t){
		List<IGetter> l = new List<IGetter>();
		foreach(PropertyInfo p in t.GetProperties())
			l.Add(new GetterProperty(p));
		return l;
	}
}

public class ReaderPropertiesAnnotated:Readers{
	public IEnumerable<IGetter> Read(Type t){
		List<IGetter> l = new List<IGetter>();
		foreach(PropertyInfo p in t.GetProperties()){
			object [] attrs = p.GetCustomAttributes(typeof(LogAttribute), true);
            if(attrs.Length != 0) {
                LogAttribute a = (LogAttribute) attrs[0];
                l.Add(new GetterPropertyAndFormat(p, a));
            }
		}
		return l;
	}
}

public interface IGetter {
    string GetName();
    object GetValue(object target);
}

public class GetterField : IGetter{
    FieldInfo f; 
    public GetterField(FieldInfo f) { this.f = f;}
    public string GetName() { return f.Name; }
    public object GetValue(object target) {
        return f.GetValue(target);
    }
}

public class GetterMethod : IGetter{
    MethodInfo m;
    public GetterMethod(MethodInfo m) {this.m = m;}
    public string GetName() { return m.Name; }
    public object GetValue(object target) { 
        return m.Invoke(target, new object[0]);
    }
}

public class GetterProperty : IGetter{
	PropertyInfo p;
	public GetterProperty(PropertyInfo p){this.p=p;}
	public string GetName(){return p.Name;}
	public object GetValue(object target){
		return p.GetGetMethod().Invoke(target, null);
	}

}

public class GetterPropertyAndFormat : IGetter{
    PropertyInfo p;
    LogAttribute log;
    public GetterPropertyAndFormat(PropertyInfo p, LogAttribute log) {
        this.p = p;
        this.log = log;
    }
    public string GetName() { return p.Name; }
    public object GetValue(object target) { 
        object val = p.GetValue(target);
        return log.Print(val);
    }
}

public class LogAttribute : Attribute {
    public virtual string Print(object val) {
        return val.ToString();
    }
}