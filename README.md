# Minecraft server starter 🎮
Was a project created by me just for hobby, the focus of this project is automatize the process of minecraft servers creation, skipping all the manual configuration required.
This project has features like:
- Support multi world server using bungeecord
- Automatically configure server IP using VPNs
- Sincronize the launch of bungeecoord and vanilla server
- If everything is already configured, it's skip to the launch

## Code
This project was coded using C# with no external frameworks
The concept of multithreading was applied to run both servers in parallel
System.Proccess library was used to invoke server bootstrap scripts
