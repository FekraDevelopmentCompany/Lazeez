using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;

namespace Lazeez.Service.Service
{
    public class SourceImageService : BaseService<lkp_SourceImage, SourceImage>
    {
        public SourceImageService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<lkp_SourceImage>();
        }

        #region BaseService Operations

        public override void Delete(int id)
        {
            // Delete image from the original folder
            var sourceImage = GetById(id);
            var path = Upload.GetFullPath(sourceImage.SourceID, (Enums.SourceType)sourceImage.SourceTypeID, sourceImage.ImagePath);
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                // Delete image
                fileInfo.Delete();

                // Check if this last image and directory became empty
                var directoryInfo = fileInfo.Directory;
                var isEmpty = !directoryInfo.EnumerateFiles().Any();
                if (isEmpty)
                    directoryInfo.Delete(); // Delete empty directory
            }

            // Delete from table SourceImage
            base.Delete(id);
        }

        public override void Delete(Expression<Func<SourceImage, bool>> whereCondition)
        {
            // Delete images from the original folder
            var sourceImages = GetAll(whereCondition);
            if (sourceImages.Count > 0)
            {
                var directoryPath = Upload.GetFullPath(sourceImages[0].SourceID, (Enums.SourceType)sourceImages[0].SourceTypeID);

                // Delete original folder with all images
                Directory.Delete(directoryPath, true);
            }

            // Delete from table SourceImage
            base.Delete(whereCondition);
        }

        #endregion

        #region Business Operations

        public List<SourceImageModel> GetImagePreview(int sourceId, Enums.SourceType sourceType)
        {
            var directoryAbsolutePath = Upload.GetAbsolutePath(sourceId, sourceType);
            if (directoryAbsolutePath == null) return null;

            var directoryFullPath = Upload.GetFullPath(sourceId, sourceType);
            var sourceImages = GetAll(s => s.SourceID == sourceId && s.SourceTypeID == (short)sourceType);

            return (from s in sourceImages
                    let fileInfo = new FileInfo(directoryFullPath + "/" + s.ImagePath)
                    select new SourceImageModel
                    {
                        ID = s.ID,
                        Url = directoryAbsolutePath + "/" + s.ImagePath,
                        Caption = fileInfo.Name,
                        Size = fileInfo.Length
                    }).ToList();
        }

        public List<string> GetImagesURL(int sourceId, Enums.SourceType sourceType)
        {
            var directoryPath = Upload.GetStaticPath(sourceId, sourceType);
            var sourceImages = GetAll(s => s.SourceID == sourceId && s.SourceTypeID == (short)sourceType);

            return (from s in sourceImages
                    select directoryPath + "/" + s.ImagePath)
                    .ToList();
        }

        public string GetDefaultImageURL(int sourceId, Enums.SourceType sourceType)
        {
            try
            {
                var directoryPath = Upload.GetStaticPath(sourceId, sourceType);
                var sourceImages = GetAll(s => s.SourceID == sourceId && s.SourceTypeID == (short)sourceType);

                return (from s in sourceImages
                        select directoryPath + "/" + s.ImagePath)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                //TODO: later on we have to log exception
                return string.Empty;
            }
        }

        #endregion
    }
}