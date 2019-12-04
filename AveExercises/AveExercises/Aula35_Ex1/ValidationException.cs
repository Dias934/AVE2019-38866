using System;
using System.Runtime.Serialization;

namespace AveExercises.Aula35_Ex1 {
	[Serializable]
	public class ValidationException : Exception {
		public ValidationException() {
		}

		public ValidationException(string message) : base(message) {
		}

		public ValidationException(string message, Exception innerException) : base(message, innerException) {
		}

		protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}