
using System;


namespace Dummies
{
    public class Person
    {
        public string Name { get; set; }
        public Date Birth { get; set; }

		
	public DateTime DueDate { get; set; }

        public Uri Website { get; set; }

        public Guid PersonGuid { get; set; }

         public Person Sibling { get; set; }

        public Person()
        {
        }

        public Person(string name)
        {
            this.Name = name;
        }
    }
}
