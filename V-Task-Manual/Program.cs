using System;
using System.IO;
using System.Timers;

class Program
{
    private static System.Timers.Timer timer;
    private static string sourceDirectory;
    private static string replicaDirectory;
    private static string logFilePath;

    static void Main(string[] args)
    {
        // Prompts for user inputs
        Console.WriteLine("Hello and welcome," +
            "\n\nThis program will create a copy of the folder you specify," +
            "\ncopy all the files from the source folder to the replica folder, and it will do this periodically," +
            "\nin an interval of time of your choice, defined in minutes." +
            "\n\nPlease enter the full path of the source directory:");
        sourceDirectory = Console.ReadLine();

        Console.WriteLine("Please enter the path of the replica directory:");
        replicaDirectory = Console.ReadLine();

        Console.WriteLine("Please enter the path of the log file:");
        logFilePath = Console.ReadLine();

        Console.WriteLine("Please enter the interval in minutes:");
        double interval = Double.Parse(Console.ReadLine()) * 60000; // Converts minutes to milliseconds

        // Creates a timer with the specified interval
        timer = new System.Timers.Timer(interval);

        // Binds the Elapsed event to the OnTimedEvent event-handling method
        timer.Elapsed += OnTimedEvent;

        Console.WriteLine("Press Enter to start the task...");
        Console.ReadLine();

        // Runs the task once before starting the timer
        OnTimedEvent(null, null);

        // Inicia o timer
        timer.Start();

        Console.WriteLine("Press the 'k' key to exit the program...");
        while (Console.Read() != 'k') ;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        try
        {
            // Creates the replica directory if it does not exist
            Directory.CreateDirectory(replicaDirectory);

            // Copies all files and subfolders from the source folder to the replica folder
            foreach (var dirPath in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(Path.Combine(replicaDirectory, dirPath.Substring(sourceDirectory.Length)));
                Log($"Created directory: {dirPath}");
            }

            foreach (var newPath in Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, Path.Combine(replicaDirectory, newPath.Substring(sourceDirectory.Length)), true);
                Log($"Copied file: {newPath}");
            }

            // Removes files in the replica folder that do not exist in the source folder
            foreach (var newPath in Directory.GetFiles(replicaDirectory, "*.*", SearchOption.AllDirectories))
            {
                if (!File.Exists(Path.Combine(sourceDirectory, newPath.Substring(replicaDirectory.Length))))
                {
                    File.Delete(newPath);
                    Log($"Removed file: {newPath}");
                }
            }

            Log("Operation completed successfully!");
        }
        catch (Exception ex)
        {
            Log($"An error occurred during execution: {ex.Message}");
        }
    }

    private static void Log(string message)
    {
        try
        {
            // Write the message to the console
            Console.WriteLine(message);

            // Attaches the message to the log file
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while trying to write to the log file: {ex.Message}");
        }
    }

}

//Sérgio Arantes
