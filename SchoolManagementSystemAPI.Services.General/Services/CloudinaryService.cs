using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SchoolManagementSystemAPI.Services.General.Services;

public class CloudinaryService
{
    // private readonly string 
    private readonly Cloudinary cloudinary;
    
    public CloudinaryService(IConfiguration configuration)
    {
        var account = new Account(
            configuration.GetValue<string>("Cloudinary:Cloud_Name"),
            configuration.GetValue<string>("Cloudinary:API_Key"),
            configuration.GetValue<string>("Cloudinary:API_Secret")
        );
        
        cloudinary = new Cloudinary(account);
    }

    public string UploadFile(IFormFile file)
    {
        
        var uploadResult  = cloudinary.Upload(new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            PublicId = $"{DateTime.Now.Millisecond.ToString()}",
            Folder = "school_management_system_media"
        });
        
        
        if (uploadResult.Error != null)
        {
            throw new Exception(uploadResult.Error.Message);
        }
        
        return uploadResult.Url.AbsoluteUri;
    }
}