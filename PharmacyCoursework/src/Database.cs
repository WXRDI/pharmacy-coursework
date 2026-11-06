using Microsoft.Data.Sqlite;

namespace PharmacyCoursework.src;

public static class Database
{
    public static SqliteConnection Open() //access to db
    {
        var conn = new SqliteConnection("Data Source=pharmacy.db");
        conn.Open();
        using var pragma = conn.CreateCommand();
        pragma.CommandText = "PRAGMA foreign_keys = ON;";
        pragma.ExecuteNonQuery();
        return conn;
    }

    public static void Init() //create db
    {
        using var conn = Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"

        CREATE TABLE IF NOT EXISTS employees (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            full_name TEXT NOT NULL,
            login TEXT NOT NULL UNIQUE,
            password_hash TEXT NOT NULL,
            role TEXT NOT NULL CHECK (role IN ('manager','pharmacist'))
        );

        CREATE TABLE IF NOT EXISTS suppliers (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            supplier_name TEXT NOT NULL,
            supplier_address TEXT,
            supplier_phone TEXT
        );

        CREATE TABLE IF NOT EXISTS medicine_type (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            type TEXT NOT NULL UNIQUE
        );

        CREATE TABLE IF NOT EXISTS stock (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            medicine_name TEXT NOT NULL,
            type_id INTEGER NOT NULL,
            supplier_id INTEGER NOT NULL,
            manufacturer TEXT,
            needs_prescription INTEGER NOT NULL DEFAULT 0,
            batch_id INTEGER,
            date_of_arrival TEXT NOT NULL,
            expiration_date TEXT NOT NULL,
            dosage TEXT,
            quantity INTEGER NOT NULL DEFAULT 0 CHECK (quantity >= 0),
            supplier_price REAL NOT NULL CHECK (supplier_price >= 0),
            client_price REAL NOT NULL CHECK (client_price >= 0),
            FOREIGN KEY (type_id) REFERENCES medicine_type(id),
            FOREIGN KEY (supplier_id) REFERENCES suppliers(id)
        );

        CREATE TABLE IF NOT EXISTS sales_data (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            medicine_id INTEGER NOT NULL,
            employee_id INTEGER NOT NULL,
            date TEXT NOT NULL,
            amount INTEGER NOT NULL CHECK (amount > 0),
            medicine_price REAL NOT NULL CHECK (medicine_price >= 0),
            client_name TEXT,
            FOREIGN KEY (medicine_id) REFERENCES stock(id),
            FOREIGN KEY (employee_id) REFERENCES employees(id)
        );";

        cmd.ExecuteNonQuery();
    }
}