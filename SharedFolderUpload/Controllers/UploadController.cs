using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharedFolderUpload.Controllers
{

    [ApiController]
    public class UploadController : ControllerBase
    {


        private readonly IHostingEnvironment _env;
        public UploadController(IHostingEnvironment env)
        {
            _env = env;
        }


        // 1-https://www.codeproject.com/Questions/1102764/File-from-one-computer-to-another-computer-network
        //System.IO.File.Copy("sourcePath", "\\machinename\share folder path");

        ////but if you want to paste it on specific drive then you can use following code
        //System.IO.File.Copy("sourcePath", "\\machinename\DriveLetter$\folder name");
        ////e.g. System.IO.File.Copy("D:\\test.txt", "\\machinename\E$\test\test.txt");





        //2-https://www.codeproject.com/Questions/1102764/File-from-one-computer-to-another-computer-network
        //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        //WindowsIdentity wid = new WindowsIdentity(username, password);
        //WindowsImpersonationContext context = wid.Impersonate();

        //File.Move(pathToSourceFile, "\\\\Server\\Folder");

        //context.Undo();




        //3-https://www.codeproject.com/Questions/5257378/Copy-files-between-two-servers-in-different-domain
        //        using (var client = new System.Net.WebClient())
        //{
        //    client.Credentials = new System.Net.NetworkCredential("username", "password");

        //    var localPath = @"\\xxx.xxx.xxx.xxx\folder\MineFile.txt";
        //    var remotePath = "https://test.com:xxxx/folder/MineFile.txt";

        //    client.DownloadFile(remotePath, localPath);
        //}


        //Downloading File From External Server Using Credential In ASP.NET MVC
        //4-https://www.c-sharpcorner.com/article/downloading-file-from-external-server-using-credential-in-asp-net-mvc/



        [HttpPost]
        [Route("api/Upload/UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] List<IFormFile> files)
        {
            //either you can pass the list of files in the method or you can initialize them inside the method like the commented line below
            //var files = HttpContext.Request.Form.Files;
            FileDetailModel fileDetail = new FileDetailModel();

            foreach (var file in files)
            {
                var fileType = Path.GetExtension(file.FileName);
                //var ext = file.;
                if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
                {
                    var filePath = _env.ContentRootPath;
                    var docName = Path.GetFileName(file.FileName);
                    if (file != null && file.Length > 0)
                    {
                        fileDetail.Id = Guid.NewGuid();
                        fileDetail.DocumentName = docName;
                        fileDetail.DocType = fileType;
                        fileDetail.DocUrl = Path.Combine(filePath, "Files", fileDetail.Id.ToString() + fileDetail.DocType);
                        using (var stream = new FileStream(fileDetail.DocUrl, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }

                        var latestFileDetail = fileDetail;

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }

            return Ok();
        }





    }
}
