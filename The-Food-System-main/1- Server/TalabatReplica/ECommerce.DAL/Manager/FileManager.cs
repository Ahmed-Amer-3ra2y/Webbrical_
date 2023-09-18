using Microsoft.AspNetCore.Http;

namespace ECommerce.DAL.Manager
{
    public static class FileManager
    {
        public static async Task<byte[ ]> UploadFileAsync( IFormFile file )
        {
            using var dataStreem = new MemoryStream( );
            await file.CopyToAsync( dataStreem );
            return dataStreem.ToArray( );

        }
    }
}
