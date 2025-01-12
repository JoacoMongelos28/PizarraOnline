# PizarraOnline

Whiteboard is a tool that allows users to share ideas, take notes, and work together interactively. Designed to promote creativity and organization in real-time, it is ideal for work teams, students, professionals, or those simply looking for entertainment.<br><br>

Features:<br>
• Change history: Saves and displays modifications made by collaborators.<br>
• Drawing tools: Includes colors, erasers, and various brush thickness options.<br>
• Real-time drawing: Users can view changes on the whiteboard as they happen.<br>
• Chat functionality: Adds an integrated chat to facilitate collaboration among users.<br>
• Collaborative editing: Allows multiple users to interact with the same whiteboard simultaneously.<br><br>

Technologies:<br>
• Backend: ASP.NET Core | SignalR | C# | Entity Framework Core<br>
• Frontend: JavaScript | HTML5 | CSS3 | Bootstrap<br><br>

> [!NOTE]
>.NET SDK 6.0<br>
>Visual Studio (Opcional)<br>
>SQL Server (Recomendado)

<br>

> [!IMPORTANT]
> Steps for the operation of the project:<br>
>1.	Clone Repository: git clone https://github.com/JoacoMongelos28/PizarraOnline.git<br>
>2.	Abrir la solucion "Pizarra_SignalR.sln"<br>
>3.	Crear Base de Datos en SQL Server con el nombre "Pizarra"<br>
>4.	Ejecutar en SQL Server la query del archivo "Pizarra_Database.sql" que se encuentra en el proyecto "Pizarra_SignalR_Data"<br>
>5.	Ejecutar el siguiente Scaffold en la consola de Administrador de Paquetes en el proyecto "Pizarra_SignalR_Data". (RECORDA PONER EL NOMBRE DE TU SERVIDOR EN EL SCAFFOLD)<br><br>
Scaffold-DbContext "Server=YOUR_SERVER_NAME;Database=Pizarra;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entidades/ -Force <br><br>
>6.	Ejecutar la aplicacion y disfrutar!
