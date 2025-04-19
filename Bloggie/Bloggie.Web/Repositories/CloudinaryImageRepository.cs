
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.Name
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
