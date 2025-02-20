# PizarraOnline

Whiteboard is a tool that allows users to share ideas, take notes, and work together interactively. Designed to promote creativity and organization in real-time, it is ideal for work teams, students, professionals, or those simply looking for entertainment.<br><br>

Features:<br>
• Change history: Saves and displays modifications made by collaborators.<br>
• Drawing tools: Includes colors, erasers, and various brush thickness options.<br>
• Real-time drawing: Users can view changes on the whiteboard as they happen.<br>
• Chat functionality: Adds an integrated chat to facilitate collaboration among users.<br>
• Collaborative editing: Allows multiple users to interact with the same whiteboard simultaneously.<br><br>

Technologies:<br>
• Backend: C#<br>
• Library: SignalR<br>
• Database: SQL Server<br>
• ORM: Entity Framework<br>
• Frontend: JavaScript | HTML5 | CSS3<br>
• Frameworks: ASP.NET MVC | Bootstrap<br><br>

> [!NOTE]
> Prerequisites:<br>
>.NET SDK 6.0<br>
>Visual Studio (Optional)<br>
>SQL Server (Recommended)

<br>

> [!IMPORTANT]
> Steps for the operation of the project:<br>
>1.	Clone Repository: git clone https://github.com/JoacoMongelos28/PizarraOnline.git<br>
>2.	Open the solution "Pizarra_SignalR.sln"<br>
>3.	Create a database in SQL Server named "Pizarra"<br>
>4.	Run the SQL query from the file "Pizarra_Database.sql" located in the "Pizarra_SignalR_Data" project in SQL Server<br>
>5.	Run the following Scaffold in the Package Manager Console in the "Pizarra_SignalR_Data" project. (REMEMBER TO ENTER YOUR SERVER NAME IN THE SCAFFOLD)<br><br>
Scaffold-DbContext "Server=YOUR_SERVER_NAME;Database=Pizarra;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entidades/ -Force <br><br>
>6.	Run the application and enjoy!

<br>

> [!TIP]
> If any error occurs with the application, clear the browser cache data and run the application again.
