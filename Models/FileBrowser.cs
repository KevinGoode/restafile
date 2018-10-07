
using System;
using System.Collections.Generic;
using restafile.Models;
using System.Web;
namespace restafile.Models{
    public class FileBrowser{
        public List<RestFile> GetDirectoryTree(RestFile parent, System.IO.DirectoryInfo root)
        {
            if(parent ==null) parent = new RestFile();
            
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
            }

            catch (System.IO.DirectoryNotFoundException e)
            {  
            }
            
            if (files != null)
            {
                List<RestFile> rootFiles = new List<RestFile>();
                foreach (System.IO.FileInfo fi in files)
                {
                    RestFile restFile = new RestFile();
                    restFile.lastModified=fi.LastWriteTimeUtc;
                    restFile.name=fi.Name;
                    restFile.id = HttpUtility.UrlEncode(fi.FullName);
                    restFile.path=fi.FullName;
                    restFile.sizeBytes=0;
                    restFile.isDirectory=false;
                    rootFiles.Add(restFile);
                }
                parent.addChildren(rootFiles);
                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    RestFile restDir = new RestFile();
                    restDir.lastModified=dirInfo.LastWriteTimeUtc;
                    restDir.name=dirInfo.Name;
                    restDir.id = HttpUtility.UrlEncode(dirInfo.FullName);
                    restDir.path=dirInfo.FullName;
                    restDir.sizeBytes=0;
                    restDir.isDirectory=true;
                    this.GetDirectoryTree(restDir, dirInfo);
                    parent.addChild(restDir);
                }
            }    
            return parent.children ;       
        }
    }
}