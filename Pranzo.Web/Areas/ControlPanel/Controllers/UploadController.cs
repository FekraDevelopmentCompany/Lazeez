﻿using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Service.Service;
using Lazeez.Web.Controllers;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class UploadController : BaseController
    {
        // GET: ControlPanel/Upload
        public PartialViewResult _Index()
        {
            return PartialView();
        }

        #region Upload Images

        public JsonResult GetImagePreview(int sourceId, Enums.SourceType sourceType)
        {
            if (sourceId <= 0) return Json(string.Empty, JsonRequestBehavior.AllowGet);

            var sourceImageService = unitOfWork.Service<SourceImageService>();
            var sourceObject = sourceImageService.GetImagePreview(sourceId, sourceType);

            return (sourceObject != null && sourceObject.Count > 0) ? Json(sourceObject, JsonRequestBehavior.AllowGet) : Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(int sourceId, Enums.SourceType sourceType)
        {
            var sourceImageService = unitOfWork.Service<SourceImageService>();

            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];
                if (file == null || file.ContentLength <= 0) continue;

                // Save in table SourceImage
                var sourceImage = new SourceImage
                {
                    SourceID = sourceId,
                    SourceTypeID = (short)sourceType,
                    ImagePath = file.FileName
                };

                sourceImageService.Insert(sourceImage);
                unitOfWork.Save();

                // Save image in the original folder
                var path = Upload.GetFullPath(sourceId, sourceType, file.FileName);
                file.SaveAs(path);
            }

            return Json(true);
        }

        [HttpPost]
        public JsonResult Delete(int key)
        {
            var sourceImageService = unitOfWork.Service<SourceImageService>();
            sourceImageService.Delete(key);
            unitOfWork.Save();
            return Json(true);
        }

        #endregion
    }
}