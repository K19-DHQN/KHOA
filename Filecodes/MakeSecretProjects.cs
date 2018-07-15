public static void MakeSecretProjects(string topDir)
        {
            
            string[] references = { "Microsoft.Pex.Framework", "Microsoft.Pex.Framework.Settings",
                                  "System.Text.RegularExpressions"};
            foreach (string taskDir in Directory.GetDirectories(topDir))
            {
                string[] files = Directory.GetFiles(taskDir);
                string secretFile = null;
                
                foreach (var file in files)
                {
                    if (file.EndsWith("solution.cs"))
                    {
                        secretFile = file;
                        break;
                    }
                }
                if (secretFile == null)
                {
                    throw new Exception("secret implementation not found");
                }

                string fileName = secretFile.Substring(secretFile.LastIndexOf("\\") + 1);
                //string projectName = "secret_project\\" + fileName.Substring(0, fileName.Length - 3);
                string projectName = "secret_project";
                string projectDir = taskDir + "\\" + projectName;
                Directory.CreateDirectory(projectDir);
                CreateProjectFile(projectDir + "\\" + projectName + ".csproj", fileName);
                string propertyDir = projectDir + "\\Properties";
                Directory.CreateDirectory(propertyDir);
                CreateAssemblyFile(propertyDir + "\\AssemblyInfo.cs", projectName);
                string newFile = projectDir + "\\" + fileName;
                File.Copy(secretFile, newFile, true);
                AddNameSpace(newFile, "Solution");
                AddUsingStatements(newFile, references);
                string[] classes = { "Program" };
                string[] methods = { "Puzzle" };
                AddPexAttribute(newFile, classes, methods);
            }
        }