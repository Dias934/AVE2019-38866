using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonzai.Test.Model
{
	class Account
	{
		public Account() { }

		public Transaction [] Transactions;
		public int Balance { get; set; }
		public int Number { get; set; }
		public Person Person { get; set; }
	}
}
