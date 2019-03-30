FakeGoogleDrive - some kind of Google Drive clone.

<pre>
Technical Task:
  -Upload/Download files
  -Create folders
  -Navigate through folders
  -Delete files/folder
  -Rename files/folders
  -Share files/folders
  -Be able to see actual information about free/taken space
  -Recieve and be invocator of realtime notifications about file/folder deletition, shatring
  -Recieve realtime updates about storage 
  
Technologies : 
  -.NET ASP Core 2 Web Api,
  -EF Core 2,
  -SignalR Core 2,
  -Angular 7,
  -Angular Material,
  -Bootstrap 4,
  -NgRx,
  -MsSql.
  
Architecture: 
  Back-end of app consist of two main parts - main infrastructional service, SignalRNotificationService -service responsible for realtime communication with front-end.
  On main infrastructional service was applied onion architecture, on SignalRNotificationService was used SignalR Core library.
Security:
  For user athentification was used JWT tokens, was specified CORS policy with specific whitelist, password are hashing and salting.
  </pre>
