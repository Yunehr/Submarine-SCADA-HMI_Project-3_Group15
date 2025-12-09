# MyProjectTemplate.API

## Getting Started

Follow these steps to run the Submarine SCADA HMI solution after cloning from GitHub.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (as required by the project)
- [Node.js (v18+)](https://nodejs.org/) and [npm](https://www.npmjs.com/) (for the client)
- [Git](https://git-scm.com/)



## Running the Backend API

1. Open the solution in Visual Studio 2022.
2. Set `MyProjectTemplate.API` as the startup project.
3. Build the solution (`Ctrl+Shift+B`).
4. Run the API (`F5` or `Ctrl+F5`).  
   The API should start and display a URL (e.g., `https://localhost:5001`).

## Running the Client Application
1. Open a terminal and navigate to the client directory:
`cd MyProjectTemplate.Client/myprojecttemplate.client.client`

2. Install dependencies:
`npm install`
3. Start the client:
`npm run dev`
4. The client should open in your browser (default: `http://localhost:3000`).

If it does not automatically open, verify it is running and enter `o` into the terminal

---
### Accessing the Application

- Ensure both the API and client are running.
- Use the client’s web interface to interact with the system.

## Troubleshooting

- If you encounter issues, check that all prerequisites are installed and that ports are not blocked by other applications.
- Review the Output window in Visual Studio for backend errors.
- For client errors, check the terminal output where `npm run dev` was run.


## Credits
- Created a custom component with the aid of GaugeComponent From; [react-gauge-compoent By: antoniolago](https://github.com/antoniolago/react-gauge-component?tab=readme-ov-file)

