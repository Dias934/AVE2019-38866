using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai.Test.Model
{
	class Classroom
	{
		public Classroom() { }
		public Classroom(string name,params Student []students)
		{
			this.Name = name;
			this.Students= students;
		}
		public string Name { get; set; }
		public Student[] Students;

	}
}
