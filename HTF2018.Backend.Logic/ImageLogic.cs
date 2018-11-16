using HTF2018.Backend.DataAccess;
using HTF2018.Backend.DataAccess.Entities;
using HTF2018.Backend.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HTF2018.Backend.Logic
{
    public class ImageLogic : IImageLogic
    {
        private readonly TheArtifactDbContext _dbContext;

        public ImageLogic(TheArtifactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Image> StoreImage(Byte[] binaryImage)
        {
            var checksum = CalculateChecksum(binaryImage);
            var existingImage = await _dbContext.Images.SingleOrDefaultAsync(x => x.Checksum == checksum);
            if (existingImage != null)
            {
                return existingImage;
            }

            var image = new Image
            {
                Id = Guid.NewGuid(),
                Data = binaryImage,
                Checksum = checksum
            };
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();
            return image;
        }

        public async Task<Image> LoadImage(Guid imageId)
        {
            return await _dbContext.Images.SingleOrDefaultAsync(x => x.Id == imageId);
        }

        private String CalculateChecksum(Byte[] bytes)
        {
            MD5 md5 = MD5.Create();
            var hashedBytes = md5.ComputeHash(bytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}