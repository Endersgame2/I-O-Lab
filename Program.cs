using System;

string TelegramTranslator(string input)
{
    return input.Replace(".", "STOP");
}

Directory.SetCurrentDirectory("C:\\Users\\Susha\\Downloads\\Parser");

string currentWorkingDirectory = Directory.GetCurrentDirectory();

string[] paths = Directory.GetFiles(currentWorkingDirectory);

string filePath = "";

foreach (string path in paths)
{
    if (path.EndsWith("theMachineStops.txt"))
    {
        filePath = path;
    }
}

if (filePath == null)
{
    Console.WriteLine("Could not find the required file");
}
else
{
    string fileText = null;

    try
    {
        using (StreamReader fileReader = new StreamReader(filePath))
        {
            fileText = await fileReader.ReadToEndAsync();

            fileReader.Close();
        }

        try
        {
            string updatedFileText = TelegramTranslator(fileText);

            using (StreamWriter fileWriter = File.CreateText("TelegramCopy.txt"))
            {
                await fileWriter.WriteAsync(updatedFileText);

                fileWriter.Close();
            }

            Console.WriteLine("Done");

        } catch (Exception exception) when (
            exception is UnauthorizedAccessException
            || exception is ArgumentException
            || exception is ArgumentNullException
            || exception is PathTooLongException
            || exception is DirectoryNotFoundException
            || exception is NotSupportedException
            )
        {
            Console.WriteLine($"Error creating new file: {exception.Message}");
        } catch (Exception exception) when (
            exception is ObjectDisposedException
            || exception is InvalidOperationException
        ) {
            Console.WriteLine($"Error writing file: {exception.Message}");
        }
    }
    catch (Exception exception) when (
        exception is ArgumentException
        || exception is ArgumentNullException
        || exception is FileNotFoundException
        || exception is DirectoryNotFoundException
        || exception is IOException
    ) {
        Console.WriteLine($"Error accessing file: {exception.Message}");
    }
    catch (Exception exception) when (
        exception is ArgumentOutOfRangeException
        || exception is ObjectDisposedException
        || exception is InvalidOperationException
    ) {
        Console.WriteLine($"Error reading file: {exception.Message}");
    }
}
