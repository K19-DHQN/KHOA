public static void RunPexOnSecretProjects(string topDir)
{
    foreach (string taskDir in Directory.GetDirectories(topDir))
    {
        string secretDir = null;
        foreach (var dir in Directory.GetDirectories(taskDir))
        {
            if (dir.EndsWith("secret_project"))
            {
                secretDir = dir;
                break;
            }
        }
        if (secretDir == null)
            throw new Exception("secret project not found");
        string reportDir = secretDir + @"\bin\Debug\reports";
        if (Directory.Exists(reportDir))
            DeleteDirectory(reportDir);
        string assemblyFile = secretDir + @"\bin\Debug\secret_project.dll";
        if (!File.Exists(assemblyFile))
        {
            throw new Exception("assembly file not found");
        }
        string[] methods = { "Puzzle" };
        string typeUnderTest = null;
        Assembly assembly = Assembly.LoadFile(assemblyFile);
        foreach (var type in assembly.GetTypes())
        {
            foreach (var method in type.GetMethods())
            {
                if (method.Name == "Puzzle")
                {
                    typeUnderTest = type.Name;
                    break;
                }
            }
            if (typeUnderTest != null)
                break;
        }
        CommandExecutor.ExecuteCommand(
            CommandGenerator.GenerateRunPexCommand(assemblyFile, "Solution", typeUnderTest, methods));
    }
}