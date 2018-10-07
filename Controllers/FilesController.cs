using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restafile.Models;
using System.Web;
using System.IO;
namespace restafile.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        // GET api/files
        [HttpGet]
        public IEnumerable<RestFile> GetAll()
        {
            FileBrowser browser = new FileBrowser();
            System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(ServerConfig.getConfig().filePath);
            return browser.GetDirectoryTree(null, root);
        }

        // GET api/files/{urlencodedpath}
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string response = "";
            string path = HttpUtility.UrlDecode(id);
            if(pathOk(path, true))
            {
               Console.Write("INFO: Reading File: " + path + "\n");
               response =  System.IO.File.ReadAllText(path);
            }
            return response;
        }

        // POST api/files/{urlencodedpath}
        [HttpPost("{id}")]
        public void Post(string id)
        {
            //NB This handler : public void Post(string id, [FromBody]string value) ONLY WORKS FOR JSON
            // SEE https://weblog.west-wind.com/posts/2017/Sep/14/Accepting-Raw-Request-Body-Content-in-ASPNET-Core-API-Controllers
            
            //EG curl http://localhost:5000/api/files/%2fhome%2fgoode_k%2fDev%2frestafile%2ftestfiles%2fdir2%2fdir2filex -X POST --data "created a new file" --verbose -H "Content-Type: text/plain"

            StreamReader reader = new StreamReader(Request.Body);
            string value=reader.ReadToEnd();
            string path = HttpUtility.UrlDecode(id);
            if(pathOk(path, false))
            {
                 Console.Write("INFO: Creating  File: " + path +  " with data " + value + "\n");
                 System.IO.File.WriteAllText(path, value);
            }
        }

        // PUT api/files/{urlencodedpath}
        [HttpPut("{id}")]
        public void Put(string id)
        {
            //EG curl http://localhost:5000/api/files/%2fhome%2fgoode_k%2fDev%2frestafile%2ftestfiles%2fdir2%2fdir2file1 -X PUT --data "updated a file" --verbose -H "Content-Type: text/plain"
            StreamReader reader = new StreamReader(Request.Body);
            string value=reader.ReadToEnd();
            string path = HttpUtility.UrlDecode(id);
            if(pathOk(path, true))
            {
               Console.Write("INFO: Updating File: " + path +  " with data " + value + "\n");
               System.IO.File.WriteAllText(path, value);
            }
        }

        // DELETE api/files{urlencodedpath}
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            //EG curl http://localhost:5000/api/files/%2fhome%2fgoode_k%2fDev%2frestafile%2ftestfiles%2fdir2%2fdir2file1 -X DELETE --verbose -H "Content-Type: text/plain"
            string path = HttpUtility.UrlDecode(id);
            if(pathOk(path, true))
            {
               Console.Write("INFO: Deleting File: " + path + "\n");
               System.IO.File.Delete(path);
            }
            
        }
        private bool pathOk(string path, bool exists){
            bool ok = false;
            if (!path.StartsWith(ServerConfig.getConfig().filePath))
            {
                Console.Write("ERROR: File " + path + " is not in root dir\n");
                this.HttpContext.Response.StatusCode=400;
            } 
            else if(System.IO.File.Exists(path)!=exists)
            {
                if(exists)
                {
                   Console.Write("ERROR: File " + path + " does not exist\n");
                   this.HttpContext.Response.StatusCode=404;
                }
                else
                {
                   Console.Write("ERROR: File " + path + " already exists. Issue a PUT if you want to edit this resource\n");
                   this.HttpContext.Response.StatusCode=400;
                }
            }
            else
            {
                ok = true;
            }
            return ok;
        }
    }
}
