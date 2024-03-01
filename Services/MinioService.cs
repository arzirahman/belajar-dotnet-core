using Minio;
using Minio.DataModel.Args;
using System.Text.RegularExpressions;

namespace food_order_dotnet.Services
{
    public partial class MinioService(IMinioClient minioClient, IConfiguration configuration)
    {
        private readonly IMinioClient _minioClient = minioClient ?? throw new ArgumentNullException(nameof(minioClient));
        private readonly string? _bucketName = configuration["Minio:BucketName"];

        [GeneratedRegex("[^a-zA-Z0-9]")]
        private static partial Regex MyRegex();
        
        private static string SanitizeForFilename(string input)
        {
            return MyRegex().Replace(input, "_");
        }

        public string GenerateFileName(string baseFileName, string categoryName, string levelName, string extension)
        {
            ArgumentNullException.ThrowIfNull(categoryName);

            ArgumentNullException.ThrowIfNull(levelName);

            string timeStamp = DateTime.Now.Ticks.ToString();
            string fileName = $"{baseFileName}_{SanitizeForFilename(categoryName)}_{SanitizeForFilename(levelName)}_{timeStamp}.{extension}";

            return fileName;
        }

        public async Task<string> GetPresignedUrlAsync(string objectName, int expiryTime)
        {
            try
            {
                return await _minioClient.PresignedGetObjectAsync(
                    new PresignedGetObjectArgs()
                        .WithBucket(_bucketName)
                        .WithObject(objectName)
                        .WithExpiry(expiryTime)
                );
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get presigned URL from MinIO", e);
            }
        }
    }
}
