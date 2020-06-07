using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CadastroApp.API.Data;
using CadastroApp.API.Helpers;
using CadastroApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cadastro.Controllers {
    [Route ("api/[controller]")]
    [Authorize ()]
    [ApiController]
    public class FileUploadController : Controller {
        private readonly ClienteRepository _repository;

        public FileUploadController (ClienteRepository repository) {
            this._repository = repository ??
                throw new ArgumentNullException (nameof (repository));
        }

        [HttpPost, DisableRequestSizeLimit]
        [Consumes ("multipart/form-data")]
        public async Task<string> Post () {
            try {
                foreach (var file in Request.Form.Files) {
                    string folderName = "Upload";
                    string webRootPath = "C:/Prod/";
                    string newPath = Path.Combine (webRootPath, folderName);
                    if (!Directory.Exists (newPath)) {
                        Directory.CreateDirectory (newPath);
                    }
                    if (file.Length > 0) {
                        string fileName = file.FileName.Trim ('"');
                        string fullPath = Path.Combine (newPath, fileName);
                        using (var stream = new FileStream (fullPath, FileMode.Create)) {
                            file.CopyTo (stream);
                        }
                    }

                }
                return "Upload Successful";
            } catch (System.Exception ex) {
                return "Upload Failed: " + ex.Message;
            }

        }
    }
}