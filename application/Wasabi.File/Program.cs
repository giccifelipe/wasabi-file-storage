using Microsoft.Extensions.Configuration;
using System.IO;
using Wasabi.File.Helpers;
using Wasabi.File.Models;
using Wasabi.File.Models.Enum;
using Wasabi.File.Tasks;

// Read the appsettings json file into a configuration
IConfigurationBuilder configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile($"appsettings.json");

IConfigurationRoot config = configuration.Build();

AppSettingsMdl appSettings = new AppSettingsMdl(config);
appSettings.BuildSettings(SettingsTypes.Wasabi);

if (appSettings.Wasabi != null)
{
    FileStorageTask.SetClient(appSettings.Wasabi);

    MenuOptions opt;
    do
    {
        string bucketName = ""; // test-dev

        opt = MenuHelper.GetMenuOption();

        FileStorageTask fileTask;

        switch (opt)
        {
            case MenuOptions.CreateBucket:
                bucketName = WasabiHelper.GetBucketName(true);
                fileTask = new(bucketName);
                await fileTask.CreateFolder();

                break;
            case MenuOptions.UploadFile:
                bucketName = WasabiHelper.GetBucketName(false);
                fileTask = new(bucketName);

                var _flPath = WasabiHelper.GetFileFullPathToUpload();

                FileInfo fileInfo = new FileInfo(_flPath);
                await fileTask.UploadFile(fileInfo.FullName, fileInfo.Name);

                break;
            case MenuOptions.DownloadFile:
                bucketName = WasabiHelper.GetBucketName(false);
                fileTask = new(bucketName);

                Console.Write("Object key (file name): ");
                string? _flNameToDownload;

                do
                {
                    _flNameToDownload = Console.ReadLine();
                } while (string.IsNullOrEmpty(_flNameToDownload));

                await fileTask.DownloadFile(_flNameToDownload);

                break;
            case MenuOptions.DownloadFiles:
                bucketName = WasabiHelper.GetBucketName(false);
                fileTask = new(bucketName);

                await fileTask.DownloadFiles();

                break;
            case MenuOptions.DeleteFile:
                bucketName = WasabiHelper.GetBucketName(false);
                fileTask = new(bucketName);

                Console.Write("Object key (file name): ");
                string? _flNameToDelete;

                do
                {
                    _flNameToDelete = Console.ReadLine();
                } while (string.IsNullOrEmpty(_flNameToDelete));

                await fileTask.DeleteFileAsync(_flNameToDelete);

                break;
            case MenuOptions.Exit:
                // Just exit
                break;
            default:
                ConsoleHelper.WriteError("Wrong menu option!");
                break;
        }

        ConsoleHelper.WriteSuccess("Process completed.");

    } while (opt != MenuOptions.Exit);

}
else
{
    ConsoleHelper.WriteWarning("No settings found to conect to wasabi.");
}

ConsoleHelper.WriteInfo("Press any key to close.", false);
Console.ReadLine();