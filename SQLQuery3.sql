-- Create the LibraryDB database
CREATE DATABASE LibraryDB;

-- Switch to the LibraryDB database
USE LibraryDB;

-- Create the Books table
CREATE TABLE Books (
    BookId int PRIMARY KEY IDENTITY(1,1),
    Title nvarchar(255),
    Author nvarchar(255),
    Genre nvarchar(255),
    Quantity int
);

-- Insert 5 rows into the Books table
INSERT INTO Books (Title, Author, Genre, Quantity)
VALUES
    ('The Great Gatsby', 'F. Scott Fitzgerald', 'Classic', 10),
    ('To Kill a Mockingbird', 'Harper Lee', 'Fiction', 15),
    ('1984', 'George Orwell', 'Dystopian', 20),
    ('The Hobbit', 'J.R.R. Tolkien', 'Fantasy', 25),
    ('The Catcher in the Rye', 'J.D. Salinger', 'Coming of Age', 30);

SELECT * FROM Books