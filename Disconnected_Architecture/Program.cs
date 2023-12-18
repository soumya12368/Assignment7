using System;
using System.Data;
using System.Data.SqlClient;

namespace Assignment_07_Disconnected_Architecture
{
    public class Program
    {
    static string conStr = "server=SOUMYA;database=LibraryDB;trusted_connection=true;";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Library Book Inventory Management");
            Console.WriteLine("1. Display Book Inventory");
            Console.WriteLine("2. Add New Book");
            Console.WriteLine("3. Update Book Quantity");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        DisplayBookInventory();
                        break;
                    case 2:
                        AddNewBook();
                        break;
                    case 3:
                        UpdateBookQuantity();
                        break;
                    case 4:
                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void DisplayBookInventory()
    {
        DataSet dataSet = RetrieveData();
        if (dataSet.Tables["Books"].Rows.Count > 0)
        {
            Console.WriteLine("Book Inventory:");
            Console.WriteLine("BookId\tTitle\tAuthor\tGenre\tQuantity");
            foreach (DataRow row in dataSet.Tables["Books"].Rows)
            {
                Console.WriteLine($"{row["BookId"]}\t{row["Title"]}\t{row["Author"]}\t{row["Genre"]}\t{row["Quantity"]}");
            }
        }
        else
        {
            Console.WriteLine("No books found in the inventory.");
        }
    }

    static void AddNewBook()
    {
        Console.Write("Enter Book Title: ");
        string title = Console.ReadLine();
        Console.Write("Enter Author: ");
        string author = Console.ReadLine();
        Console.Write("Enter Genre: ");
        string genre = Console.ReadLine();
        Console.Write("Enter Quantity: ");
        int quantity;
        int.TryParse(Console.ReadLine(), out quantity);

        using (SqlConnection connection = new SqlConnection(conStr))
        {
            connection.Open();
            string query = "INSERT INTO Books (Title, Author, Genre, Quantity) VALUES (@Title, @Author, @Genre, @Quantity)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                command.Parameters.AddWithValue("@Genre", genre);
                command.Parameters.AddWithValue("@Quantity", quantity);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("New book added to the inventory.");
                else
                    Console.WriteLine("Failed to add the new book.");
            }
        }
    }

    static void UpdateBookQuantity()
    {
        Console.Write("Enter Book Title to update quantity: ");
        string title = Console.ReadLine();
        Console.Write("Enter New Quantity: ");
        int newQuantity;
        int.TryParse(Console.ReadLine(), out newQuantity);

        using (SqlConnection connection = new SqlConnection(conStr))
        {
            connection.Open();
            string query = "UPDATE Books SET Quantity = @Quantity WHERE Title = @Title";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Quantity", newQuantity);
                command.Parameters.AddWithValue("@Title", title);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Book quantity updated successfully.");
                else
                    Console.WriteLine("Book not found in the inventory or failed to update quantity.");
            }
        }
    }

    static DataSet RetrieveData()
    {
        DataSet dataSet = new DataSet();
        using (SqlConnection connection = new SqlConnection(conStr))
        {
            connection.Open();
            string query = "SELECT * FROM Books";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(dataSet, "Books");
        }
        return dataSet;
    }
}
}