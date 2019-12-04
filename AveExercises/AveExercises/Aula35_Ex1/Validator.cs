using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AveExercises.Aula35_Ex1 {
	public class Validator<T> : IValidation {

		private Node __head;

		public Validator(){
			__head = new Node();
		}

		public Validator<T> AddValidation<W>(string src, Func<W,bool> func) {
			PropertyInfo property = typeof(T).GetProperty(src);
			if (property is null)
				throw new ArgumentException(src + " is not a property of " + typeof(T).Name);
			if (!property.PropertyType.Equals(typeof(W)))
				throw new TypeMismatchException("Type of property "+src+"is ot the same as the type in Func");
			IValidation validation = new AuxValidation<W>(func);
			return AddValidation(src,validation);
		}

		public Validator<T> AddValidation(string src, IValidation validation) {
			PropertyInfo property = typeof(T).GetProperty(src);
			if (property is null)
				throw new ArgumentException(src+" is not a property of "+ typeof(T).Name);
			Node aux = __head;
			while (aux.next != null)
				aux = aux.next;
			aux.property = property;
			aux.validation = validation;
			aux.next = new Node();
			return this;
		}

		public bool Validate(object obj) {
			if (obj is null)
				throw new ArgumentNullException();
			Node aux = __head;
			while (aux.next != null) {
				if (!aux.validation.Validate(aux.property.GetValue(obj)))
					throw new ValidationException("The property "+ aux.property.Name+"is not valid. ->"+ aux.validation.GetType().Name);
				aux = aux.next;
			}
			return true;
		}
	}

	internal class AuxValidation<W> : IValidation {

		private Func<W, bool> __func;

		public AuxValidation(Func<W,bool> func) {
			__func = func;
		}

		public bool Validate(object obj) {
			return __func.Invoke((W)obj);
		}
	}

	internal class Node {
		public PropertyInfo property { get; set; }
		public IValidation validation { get; set; }

		public Node next;

	}
}
