using System.Text.RegularExpressions;

namespace Wasabi.File.Helpers
{
    internal class WasabiHelper
    {
        public static bool IsValid(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName)) return false;

            if (bucketName.Length < 3) return false;

            string regexMask = "^[a-z0-9.-]*$";
            Regex regex = new Regex(regexMask);

            return regex.IsMatch(bucketName);
        }

        public static string GetBucketName(bool newBucket)
        {
            string? bucketName;

            bool isValidName;
            do
            {
                if (newBucket)
                {
                    ConsoleHelper.WriteInfo("What is the bucket name to be created:");
                    ConsoleHelper.WriteInfo("Requirements:");
                    ConsoleHelper.WriteInfo("Min 3 chars");
                    ConsoleHelper.WriteInfo("Number or Letters (lower case)");
                    ConsoleHelper.WriteInfo("Optionals:");
                    ConsoleHelper.WriteInfo(".");
                    ConsoleHelper.WriteInfo("-", false);
                }
                else
                {
                    ConsoleHelper.WriteInfo("Bucket name: ", false);
                }

                bucketName = Console.ReadLine();
                bucketName = bucketName?.TrimStart().TrimEnd();

                // check if is valid name
                isValidName = IsValid(bucketName);

                if (!isValidName)
                    ConsoleHelper.WriteError("Invalid name to create a bucket!");

            } while (!isValidName);

            return bucketName;
        }

        public static string GetFileFullPathToUpload()
        {
            bool fileExists = false;
            string? fullFilePath;
            do
            {
                ConsoleHelper.WriteInfo("Add the full file path to upload to wasabi: ", false);
                fullFilePath = Console.ReadLine();

                fullFilePath = fullFilePath?.TrimStart().TrimEnd();

                if (!string.IsNullOrEmpty(fullFilePath))
                    fileExists = System.IO.File.Exists(fullFilePath);

                if (!fileExists)
                    ConsoleHelper.WriteError("Invalid file path!");

            } while (!fileExists);

            return fullFilePath;
        }
    }
}
