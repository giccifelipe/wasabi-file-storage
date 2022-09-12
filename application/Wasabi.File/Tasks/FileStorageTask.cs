using Amazon.S3;
using Amazon.S3.Model;
using Wasabi.File.Helpers;
using Wasabi.File.Models.Settings;

namespace Wasabi.File.Tasks
{
    internal class FileStorageTask
    {
        private static IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public FileStorageTask(string bucketName)
        {
            _bucketName = bucketName;
        }

        public async Task<bool> ValidateBucket()
        {
            return await DoesBucketExistsAsync(_bucketName);
        }

        public static void SetClient(WasabiMdl wasabiSettings)
        {
            // 1. this is necessary for the endpoint
            AmazonS3Config s3Config = new() { ServiceURL = wasabiSettings.URL };

            // create s3 connection with credential files and config.
            _s3Client = new AmazonS3Client(wasabiSettings.Credentials.AccessKey, wasabiSettings.Credentials.SecretKey, s3Config);
        }

        public async Task CreateFolder() => await CreateBucketAsync(_bucketName);

        public async Task UploadFile(string filePath, string fileName) => await UploadObjectFromFileAsync(_bucketName, fileName, filePath);

        public async Task<Stream?> DownloadFile(string fileName) => await DownloadObjectFromBucketAsync(_bucketName, fileName);

        public async Task<IEnumerable<Stream>> DownloadFiles() => await DownloadObjectsFromBucketAsync(_bucketName);

        public async Task DeleteFileAsync(string fileName) => await DeleteObjectAsync(_bucketName, fileName);

        private static async Task<bool> DoesBucketExistsAsync(string bucketName)
        {
            bool result;
            try
            {
                result = await _s3Client.DoesS3BucketExistAsync(bucketName);
            }
            catch (AmazonS3Exception e)
            {
                result = false;
                ConsoleHelper.WriteError(e.Message);
            }
            catch (Exception ex)
            {
                result = false;
                ConsoleHelper.WriteError(ex.Message);
            }

            return result;
        }

        private static async Task CreateBucketAsync(string bucketName)
        {
            try
            {
                PutBucketResponse putResponse = await _s3Client.PutBucketAsync(bucketName);

                var statusCode = putResponse.HttpStatusCode;

                ConsoleHelper.WriteSuccess($"Bucket {bucketName} created!");
            }
            catch (AmazonS3Exception e)
            {
                ConsoleHelper.WriteError(e.Message);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError(ex.Message);
            }
        }

        private static async Task UploadObjectFromFileAsync(string bucketName, string objectName, string filePath)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                    FilePath = filePath
                };
                putRequest.Metadata.Add("x-amz-meta-title", "someTitle");
                var putResponse = await _s3Client.PutObjectAsync(putRequest);

                var statusCode = putResponse.HttpStatusCode;

                ConsoleHelper.WriteSuccess($"Object {objectName} uploaded!");
            }
            catch (AmazonS3Exception e)
            {
                ConsoleHelper.WriteError(e.Message);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError(ex.Message);
            }
        }

        private static async Task<Stream?> DownloadObjectFromBucketAsync(string bucketName, string objectName)
        {
            Stream? objectStream;
            try
            {
                objectStream = await _s3Client.GetObjectStreamAsync(bucketName, objectName, null);

                ConsoleHelper.WriteSuccess($"Object {objectName} downloaded!");
            }
            catch (AmazonS3Exception e)
            {
                objectStream = null;
                ConsoleHelper.WriteError(e.Message);
            }
            catch (Exception ex)
            {
                objectStream = null;
                ConsoleHelper.WriteError(ex.Message);
            }

            return objectStream;
        }

        private static async Task<IEnumerable<Stream>> DownloadObjectsFromBucketAsync(string bucketName)
        {
            List<Stream> objectsStream = new();
            try
            {
                IList<string> objectKeys = await _s3Client.GetAllObjectKeysAsync(bucketName, string.Empty, null);

                foreach (var objectKey in objectKeys)
                {
                    var objectStream = await DownloadObjectFromBucketAsync(bucketName, objectKey);
                    if (objectStream != null)
                        objectsStream.Add(objectStream);
                }

                ConsoleHelper.WriteSuccess($"{objectsStream.Count()} objects downloaded!");
            }
            catch (AmazonS3Exception e)
            {
                objectsStream = new();
                ConsoleHelper.WriteError(e.Message);
            }
            catch (Exception ex)
            {
                objectsStream = new();
                ConsoleHelper.WriteError(ex.Message);
            }

            return objectsStream;
        }

        private static async Task DeleteObjectAsync(string bucketName, string objectName)
        {
            try
            {
                await _s3Client.DeleteAsync(bucketName, objectName, null);

                ConsoleHelper.WriteSuccess($"Object {objectName} deleted!");
            }
            catch (AmazonS3Exception e)
            {
                ConsoleHelper.WriteError(e.Message);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError(ex.Message);
            }
        }
    }
}
