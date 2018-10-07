using System;
using System.Collections.Generic;
namespace restafile.Models{
    public class RestFile{
        public RestFile(){
            children = new List<RestFile>();
        }
        public string name{get;set;}
        public string id {get;set;}
        public string path{get;set;}
        public long sizeBytes{get;set;}
        public DateTime created{get;set;}
        public DateTime lastModified{get;set;}
        public bool isDirectory{get;set;}
        public  List<RestFile> children {get;set;}
        public void addChild(RestFile file)
        {
            children.Add(file);
        }
         public void addChildren(List<RestFile> files)
        {
            foreach(RestFile file in files)
            {
               children.Add(file);
            }
            
        }


    }
}