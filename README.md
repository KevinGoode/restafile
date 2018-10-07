# restafile
Restafile is an extremly lightweight rest server that supports full CRUD operations on a tree of files
Restafile is written using .netcore using the Visual Studio Code development environment
//To create a project like this;
//https://www.youtube.com/watch?v=QpjkiQ5qYtw
//This project created by:  dotnet new api -n restafile 
//To build: restafile>dotnet build
//To run: restafile>dotnet run path=/home/goode_k/Dev/restafile/testfiles
To use:
1.) GET ALL:  IN Browser: http://localhost:5000/api/files
2.) GET SINGLE FILE: In Browser http://localhost:5000/api/files/{id}
eg http://localhost:5000/api/files/%2fhome%2fgoode_k%2fDev%2frestafile%2ftestfiles%2fdir2%2fdir2file2
(note the file id is the URL encoded path of file)
3.) DELETE A FILE:  Using CURL curl http://localhost:5000/api/files/{id} -X DELETE  --vebose
curl http://localhost:5000/api/files/%2fhome%2fgoode_k%2fDev%2frestafile%2ftestfiles%2fdir2%2fdir2filex -X DELETE  --vebose
4.) EDIT A FILE: Using CURL curl http://localhost:5000/api/files/{id} -X PUT --data "new content" --verbose -H "Content-Type: text/plain"
5.) CREATE A NEW FILE: Using CURL curl http://localhost:5000/api/files/{new-id} -X POST --data "new file content" --verbose -H "Content-Type: text/plain"
