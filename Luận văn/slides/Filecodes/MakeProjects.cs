public static void MakeProjects(string topDir) {
  string[] references = { "Microsoft.Pex.Framework", 
                          "Microsoft.Pex.Framework.Settings",
                          "System.Text.RegularExpressions" };
  foreach (string taskDir in Directory.GetDirectories(topDir)) {
    foreach (var studentDir in Directory.GetDirectories(taskDir)) {
      if (studentDir.EndsWith("secret_project"))
        continue;
      foreach (var file in Directory.GetFiles(studentDir)) {
        if (file.EndsWith(".cs")) {
          string fileName = file.Substring(file.LastIndexOf("\\") + 1);
          string projectName = "project" + fileName.Substring(0, 
          fileName.Length - 3);
          string projectDir = studentDir + "\\" + projectName;
          Directory.CreateDirectory(projectDir);
          CreateProjectFile(projectDir + "\\" + projectName + 
          ".csproj", fileName);
          string propertyDir = projectDir + "\\Properties";
          Directory.CreateDirectory(propertyDir);
          CreateAssemblyFile(propertyDir + "\\AssemblyInfo.cs", 
          projectName);
          string newFile = projectDir + "\\" + fileName;
          File.Copy(file, newFile, true);
          AddNameSpace(newFile, "Submission");
          AddUsingStatements(newFile, references);
          //string[] classes={"Program"};
          string[] methods = { "Puzzle" };
          AddPexAttribute(newFile, null, methods);
        }
      }
    }
  }
}