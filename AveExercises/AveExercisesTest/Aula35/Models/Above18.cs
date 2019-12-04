using AveExercises.Aula35_Ex1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AveExercisesTest.Aula35.Models {
	public class Above18 : IValidation {
		public bool Validate(object obj) {
			return ((int)obj) > 18;
		}
	}
}
