public static void MakeMetaProjects(string topDir) {
    string[] references = { "Microsoft.Pex.Framework", "Microsoft.Pex.Framework.Settings",
                            "System.Text.RegularExpressions"};
    foreach (string taskDir in Directory.GetDirectories(topDir)) {
        string secretProjectDir = taskDir + "\\secret_project";
        if (!Directory.Exists(secretProjectDir))
            throw new Exception("Make sure creating the secret project before the meta project");
        string[] files = Directory.GetFiles(taskDir);
        string secretFile = null;
        foreach (var file in files) {
            if (file.EndsWith("solution.cs")) {
                secretFile = file;
                break;
            }
        }
        if (secretFile == null) {
            throw new Exception("secret implementation not found");
        }
        string secretFileName = secretFile.Substring(secretFile.LastIndexOf("\\") + 1);
        string[] compiledFile = new string[3];
        compiledFile[1] = secretFileName;
        compiledFile[2] = "MetaProgram.cs";
        string secretAssembly = secretProjectDir + @"\bin\Debug\secret_project.dll";
        foreach (var studentDir in Directory.GetDirectories(taskDir)) {
            if(studentDir.EndsWith("secret_project"))
                continue;
            foreach (var file in Directory.GetFiles(studentDir)) {
                if (file.EndsWith(".cs")) {
                    string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                    string projectName = "meta_project" + fileName.Substring(0, fileName.Length - 3);
                    string projectDir = studentDir + "\\" + projectName;
                    Directory.CreateDirectory(projectDir);
                    string propertyDir = projectDir + "\\Properties";
                    Directory.CreateDirectory(propertyDir);
                    CreateAssemblyFile(propertyDir + "\\AssemblyInfo.cs", projectName);
                    string newFile = projectDir + "\\" + fileName;
                    File.Copy(file, newFile, true);
                    string newSecretFile = projectDir + "\\" + secretFileName;
                    File.Copy(secretFile, newSecretFile, true);
                    AddNameSpace(newFile, "Submission");
                    AddNameSpace(newSecretFile, "Solution");
                    AddUsingStatements(newFile, references);
                    AddUsingStatements(newSecretFile, references);
                    AddPexAttribute(newFile);
                    AddPexAttribute(newSecretFile);
                    string metaProgramFile = projectDir + "\\MetaProgram.cs";
                    CreateMetaProgram(metaProgramFile, secretFile, secretAssembly, file);
                    AddUsingStatements(metaProgramFile, references);
                    compiledFile[0] = fileName;
                    CreateProjectFile(projectDir + "\\" + projectName + ".csproj", compiledFile);
                }
            }
        }
    }
}