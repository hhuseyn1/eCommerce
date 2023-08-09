namespace Source.Helpers;

public class UploadFileHelper
{
    public async static Task<string> UploadFile(IFormFile file)
    {
        var imagePath = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string path = @$"C:\Users\LEGION\source\repos\eCommerce\Source\wwwroot\{imagePath}";

        FileStream fs = new(path, FileMode.CreateNew, FileAccess.ReadWrite);

        await file.CopyToAsync(fs);
    
        return imagePath;
    }
}
