## FORUM-WEBSITE

This project is a website created with Js/Html/Css that uses a C# API. It implements a forum for posting stuff and also an API to track users and provide login/register tools. 
To debug this project I used a static IP address (1.1.1.1) for my API when using dotnet run, to ensure that my website hosted in "localhost" would be able to make requests to it. Make sure to change your IP back to a DHCP generated IP because 1.1.1.1 is already reserved as a static IP and will prevent you from accessing other websites.

---

TODO: 

1. Finish communication between interface and API
2. Develop password encryption system for better security
3. Migrate http to https