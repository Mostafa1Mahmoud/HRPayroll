using System;
using System.Collections.Generic;
namespace HRPayroll.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);
        Task DeleteAsync(string fileUrl, CancellationToken cancellationToken = default);
        string GetPresignedUrl(string fileUrl, int expiryMinutes = 60);
    }
}
