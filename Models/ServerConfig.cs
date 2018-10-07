using System;

namespace restafile.Models{
    public class ServerConfig{
    
    public string filePath {get;set;}
    public static ServerConfig getConfig(){
        if (instance==null){
            instance = new ServerConfig();
        }
        return instance;
    }
    private ServerConfig()
    {
        
    }
    private static ServerConfig instance = null;

    }
}