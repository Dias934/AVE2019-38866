using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jsonzai.Test.Model;

namespace Jsonzai.Test
{
    [TestClass]
    public class JsonParserTest
    {
        [TestMethod]
        public void TestParsingStudent()
        {
            string src = "{Name: \"Ze Manel\", Nr: 6512, Group: 11, GithubId: \"omaior\"}";
            Student std = (Student) JsonParser.Parse(src, typeof(Student));
            Assert.AreEqual("Ze Manel", std.Name);
            Assert.AreEqual(6512, std.Nr);
            Assert.AreEqual(11, std.Group);
            Assert.AreEqual("omaior", std.GithubId);
        }
        [TestMethod]
        public void TestSiblings()
        {
            string src = "{Name: \"Ze Manel\", Sibling: { Name: \"Maria Papoila\", Sibling: { Name: \"Kata Badala\"}}}";
            Person p = (Person)JsonParser.Parse(src, typeof(Person));
            Assert.AreEqual("Ze Manel", p.Name);
            Assert.AreEqual("Maria Papoila", p.Sibling.Name);
            Assert.AreEqual("Kata Badala", p.Sibling.Sibling.Name);
        }

        [TestMethod]
        public void TestParsingPersonWithBirth()
        {
            string src = "{Name: \"Ze Manel\", Birth: {Year: 1999, Month: 12, Day: 31}}";
            Person p = (Person)JsonParser.Parse(src, typeof(Person));
            Assert.AreEqual("Ze Manel", p.Name);
            Assert.AreEqual(1999, p.Birth.Year);
            Assert.AreEqual(12, p.Birth.Month);
            Assert.AreEqual(31, p.Birth.Day);
        }

        [TestMethod]
        public void TestParsingPersonArray()
        {
            string src = "[{Name: \"Ze Manel\"}, {Name: \"Candida Raimunda\"}, {Name: \"Kata Mandala\"}]";
            Person [] ps = (Person[]) JsonParser.Parse(src, typeof(Person));
            Assert.AreEqual(3, ps.Length);
            Assert.AreEqual("Ze Manel", ps[0].Name);
            Assert.AreEqual("Candida Raimunda", ps[1].Name);
            Assert.AreEqual("Kata Mandala", ps[2].Name);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestBadJsonObjectWithUnclosedBrackets()
        {
            string src = "{Name: \"Ze Manel\", Sibling: { Name: \"Maria Papoila\", Sibling: { Name: \"Kata Badala\"}";
            Person p = (Person)JsonParser.Parse(src, typeof(Person));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestBadJsonObjectWithWrongCloseToken()
        {
            string src = "{Name: \"Ze Manel\", Sibling: { Name: \"Maria Papoila\"]]";
            Person p = (Person)JsonParser.Parse(src, typeof(Person));
        }

		[TestMethod]
		public void TestParsingClassroom()
		{
			string src = "{Name: \"TL41_N\", Students: [{Name: \"Ze Manel\"}, {Name: \"Candida Raimunda\"}, {Name: \"Kata Mandala\"}]}";
			Classroom cs = (Classroom)JsonParser.Parse(src, typeof(Classroom));
			Assert.AreEqual(3, cs.Students.Length);
			Assert.AreEqual("TL41_N", cs.Name);
			Assert.AreEqual("Ze Manel", cs.Students[0].Name);
			Assert.AreEqual("Candida Raimunda", cs.Students[1].Name);
			Assert.AreEqual("Kata Mandala", cs.Students[2].Name);
		}

		[TestMethod]
		public void TestParsingAccount()
		{
			string src = "{Person: {Name: \"Ze Manel\", Birth: {Year: 1999, Month: 12, Day: 31}}, Number: 152, Balance: 500, Transactions: ["+
				"{Value:25, Account: {Person: {Name: \"Candida Raimunda\"}}},{Value:30, Account: {Person: {Name: \"Kata Mandala\"}}}]}";
			Account ac = (Account)JsonParser.Parse(src, typeof(Account));
			Assert.AreEqual(2, ac.Transactions.Length);
			Assert.AreEqual("Ze Manel",ac.Person.Name);
			Assert.AreEqual(1999, ac.Person.Birth.Year);
			Assert.AreEqual(12, ac.Person.Birth.Month);
			Assert.AreEqual(31, ac.Person.Birth.Day);
			Assert.AreEqual(152, ac.Number);
			Assert.AreEqual(500, ac.Balance);
			Assert.AreEqual(25, ac.Transactions[0].Value);
			Assert.AreEqual(30, ac.Transactions[1].Value);
		}

		[TestMethod]
		public void TestParsingJsonPropertyAtt()
		{
			string src = "{Name: \"Livro para tótós\", publish_date:{Year: 2015, Month: 2, Day: 23}, author: {"+
				"Name: \"Ze Manel\", Birth: {Year: 1999, Month: 12, Day: 31}}, edition: 4}";
			Book book = (Book)JsonParser.Parse(src, typeof(Book));
			Assert.AreEqual("Livro para tótós", book.Name);
			Assert.AreEqual(4, book.Edition);
			Assert.AreEqual(23, book.PublishDate.Day);
			Assert.AreEqual("Ze Manel", book.Author.Name);
		}

		[TestMethod]
		public void TestParsingJsonConvertAtt()
		{
			string src = "{DueDate: \"18/10/2019 17:26\"}";
			Clock c = (Clock)JsonParser.Parse(src, typeof(Clock));
			Assert.AreEqual(17, c.DueDate.Hour);
			Assert.AreEqual(26, c.DueDate.Minute);
			Assert.AreEqual(18, c.DueDate.Day);
			Assert.AreEqual(10, c.DueDate.Month);
		}
	}
}
