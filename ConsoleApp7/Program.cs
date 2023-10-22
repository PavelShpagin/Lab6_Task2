using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.IO.Pipes;
using System.Globalization;
using System.Xml.Linq;

class PublishingHouse
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Adress { get; set; }
}

class Book
{
	[JsonIgnore]
	public int PublishingHouseId { get; set; }
	[JsonPropertyName("Name")]
	public string Title { get; set; }
	public PublishingHouse PublishingHouse { get; set; }
}

class Program
{
	public static void Main()
	{
		Console.OutputEncoding = System.Text.Encoding.UTF8;
		string path = @"C:/MyFiles/test_x/my_json.json";

		List<Book> books = new List<Book>
		{
			new Book
			{
				PublishingHouseId = 2,
				Title = "Підручник. Алгебра 8",
				PublishingHouse = new PublishingHouse
				{
					Id = 2,
					Name = "ГІМНАЗІЯ",
					Adress = "Адреса 2"
				}
			},
			new Book
			{
				PublishingHouseId = 1,
				Title = "Щоденник нейрохірурга",
				PublishingHouse = new PublishingHouse
				{
					Id = 1,
					Name = "Видавництво старого лева",
					Adress = "Адреса 1"
				}
			},
			new Book
			{
				PublishingHouseId = 2,
				Title = "Посібник. Алгебра 9",
				PublishingHouse = new PublishingHouse
				{
					Id = 2,
					Name = "ГІМНАЗІЯ",
					Adress = "Адреса 2"
				}
			}
		};

		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};
		string jsonString = JsonSerializer.Serialize(books, options);
		File.WriteAllText(path, jsonString);

		jsonString = File.ReadAllText(path);
		List<Book> deserializedBooks = JsonSerializer.Deserialize<List<Book>>(jsonString, options);

		Console.WriteLine(jsonString);

		foreach (var book in deserializedBooks)
		{
			Console.WriteLine($"PublishingHouseId: {book.PublishingHouseId}");
			Console.WriteLine($"Title: {book.Title}");
			Console.WriteLine($"Publishing House: {book.PublishingHouse.Name}");
			Console.WriteLine($"Adress: {book.PublishingHouse.Adress}");
			Console.WriteLine();
		}
	}
}