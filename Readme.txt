Hello!

To debug or run this application correctly, the solution must be changed to execute "multiple startup projects".
This solution has two executable projects(RCC.WebApi and RCC.WorkService). Both projects must be runnig for the application receive requests correctly.

The API is configured to listen ports 44388(https) and 52750(http)

Requests are done using this structure:
GET - http://localhost:52750/api/like/id
POST - http://localhost:52750/api/like  
	Body[JSON]: {"articleid":1,"liked":true}

Others settings can be done on appsettings.json file.

Let me know whether you have any problem.

Thanks.