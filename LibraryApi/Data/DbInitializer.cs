using Dapper;
using Microsoft.Data.Sqlite;

namespace LibraryApi.Data
{
    public static class DbInitializer
    {
        public static void InitializeDatabase(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var tableCommands = new[]
            {
            // Members
            @"CREATE TABLE IF NOT EXISTS Members (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName TEXT NOT NULL,
                Email TEXT NOT NULL,
                JoinedAt TEXT NOT NULL);
            ",
            
            // Books
            @"CREATE TABLE IF NOT EXISTS Books (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT NOT NULL,
            Author TEXT NOT NULL,
            IsAvailable INTEGER NOT NULL);
            ",

            // Loans
            @"CREATE TABLE IF NOT EXISTS Loans (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            BookId INTEGER NOT NULL,
            MemberId INTEGER NOT NULL,
            LoanDate TEXT NOT NULL,
            ReturnDate TEXT,
            FOREIGN KEY (BookId) REFERENCES Books(Id),
            FOREIGN KEY (MemberId) REFERENCES Members(Id));
            "
        };

            foreach (var cmd in tableCommands)
            {
                connection.Execute(cmd);
            }

            var memberCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Members;");
            if (memberCount == 0)
            {
                connection.Execute(@"
                INSERT INTO Members (FullName, Email, JoinedAt) 
                VALUES 
                ('Yetta Pitts','orci.sem@protonmail.edu','2024-10-05 19:57:05'),
                ('Pandora Lewis','aliquet.magna.a@hotmail.net', '2025-08-10 22:34:59'),
                ('Brett Jacobs','feugiat.sed@aol.couk','2025-03-01 21:49:48'),
                ('Noelle Tate','quis.diam@outlook.net','2025-11-08 22:40:32'),
                ('Emily Solomon', 'risus.donec.egestas@outlook.net','2026-04-08 06:37:54');");
            }

            var bookCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Books;");
            if (bookCount == 0)
            {
                connection.Execute(@"
                INSERT INTO Books (Title, Author, IsAvailable) 
                VALUES 
                ('Don Quixote', 'Miguel de Cervantes', 1),
                ('Alice s Adventures in Wonderland', 'Lewis Carroll', 1),
                ('Harry Potter and the Chamber of Secrets', 'J.K. Rowling', 1);");
            }
        }
    }
}