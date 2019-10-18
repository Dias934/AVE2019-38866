namespace Jsonzai.Test.Model
{
	class Book
	{
		public string Name { get; set; }
		[JsonProperty("publish_date")] public Date PublishDate { get; set; }

		[JsonProperty("author")] public Person Author { get; set; }

		[JsonProperty("edition")] public int Edition { get; set; }

		public Book() { }
	}
}
