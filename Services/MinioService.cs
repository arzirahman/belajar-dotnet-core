using Minio;
using Minio.DataModel.Args;

namespace food_order_dotnet.Services
{
    public partial class MinioService(IMinioClient minioClient, IConfiguration configuration)
    {
        private readonly IMinioClient _minioClient = minioClient ?? throw new ArgumentNullException(nameof(minioClient));
        private readonly string? _bucketName = configuration["Minio:BucketName"];

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
