using DemoApp.Business.Models;
using DemoApp.Business.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;

namespace DemoApp.Api.Controllers
{
    public class ContentController : ApiController
    {
        private IContentService ContentService { get; set; }
        private IFileService FileService { get; set; }

        private const string FilesDirectory = "Files";

        public ContentController(IContentService service, IFileService fileService)
        {
            ContentService = service;
            FileService = fileService;
        }

        public IEnumerable<Content> Get()
        {
            return ContentService.GetAll();
        }

        public Content Get(int id)
        {
            return ContentService.Get(id);
        }

        public Content Post(Content model)
        {
            return ContentService.Add(model);
        }

        public Content Post(int id)
        {
            try
            {
                var value = ContentService.Get(id);
                if (value == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                var filename = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(HttpContext.Current.Request.Files[0].FileName));
                FileService.Save(FilesDirectory, filename, HttpContext.Current.Request.Files[0].InputStream);
                value.Photo = string.Format(@"{0}/{1}", FilesDirectory, filename);
                return ContentService.Save(value);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public Content Put(int id, Content model)
        {
            var value = ContentService.Get(id);
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return ContentService.Save(model);
        }

        public void Delete(int id)
        {
            var model = ContentService.Get(id);
            if (model == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ContentService.Delete(model.Id);
        }
    }
}
