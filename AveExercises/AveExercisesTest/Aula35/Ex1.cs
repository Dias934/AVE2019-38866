using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AveExercisesTest.Aula35.Models;
using AveExercises.Aula35_Ex1;

namespace AveExercisesTest.Aula35 {
	[TestClass]
	public class Ex1 {
		[TestMethod]
		public void One() {
			IValidation validation = new Above18();
			Assert.IsTrue(validation.Validate(20));
			Assert.IsFalse(validation.Validate(15));
		}

		[TestMethod]
		public void TwoWithoutException() {
			Student s = new Student(); 
			s.Age = 20; 
			s.Name = "Anacleto";
			Validator<Student> validator = new Validator<Student>()
							.AddValidation("Age", new Above18())
							.AddValidation("Name", new NotNull());
			Assert.IsTrue(validator.Validate(s));
		}

		[TestMethod]
		[ExpectedException(typeof(ValidationException))]
		public void TwoWithException() {
			Student s = new Student();
			s.Age = 10;
			s.Name = "Anacleto";
			Validator<Student> validator = new Validator<Student>()
							.AddValidation("Age", new Above18())
							.AddValidation("Name", new NotNull());
			Assert.IsTrue(validator.Validate(s));
		}

		[TestMethod]
		public void ThreeWithoutException() {
			Student s = new Student();
			s.Age = 20;
			s.Name = "Anacleto";
			Validator<Student> validator = new Validator<Student>()
							.AddValidation<string>("Name", UtilMethods.Max50Chars);
			Assert.IsTrue(validator.Validate(s));
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThreeWithException() {
			Student s = new Student();
			s.Age = 20;
			s.Name = "Anacleto";
			Validator<Student> validator = new Validator<Student>()
							.AddValidation<string>("Age", UtilMethods.Max50Chars);
			Assert.IsTrue(validator.Validate(s));
		}

		[TestMethod]
		public void Four() {
			Student s = new Student();
			s.Age = 20;
			s.Name = "Anacleto";
			Validator<Student> validator = new Validator<Student>()
							.AddValidation("Age", new Above18())
							.AddValidation("Name", new NotNull())
							.AddValidation<string>("Name", UtilMethods.Max50Chars);
			Assert.IsTrue(validator.Validate(s));
		}
	}
}
