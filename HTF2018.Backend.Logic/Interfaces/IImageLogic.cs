using System;
using System.Threading.Tasks;
using HTF2018.Backend.DataAccess.Entities;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IImageLogic
    {
        Task<Image> StoreImage(Byte[] binaryImage);

        Task<Image> LoadImage(Guid imageId);
    }
}