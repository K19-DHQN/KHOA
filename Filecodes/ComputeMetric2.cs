public static void ComputeMetric2(string topDir) {
    foreach (var taskDir in Directory.GetDirectories(topDir)){
        List<Test> tests = Serializer.DeserializeTests(taskDir + @"\secret_project\PexTests.xml");
        MethodInfo secretMethod = Utility.GetMethodDefinition(
            Utility.GetAssemblyForProject(taskDir + @"\secret_project"), "Program", "Puzzle");
        foreach (var test in tests){
            try{
                test.TestOuput = secretMethod.Invoke(null, test.TestInputs.ToArray());
            }
            catch (Exception e){
                test.TestOuput = e;
            }
        }
        foreach (var studentDir in Directory.GetDirectories(taskDir)){
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("projectNo\t#match\t#all\tmetric2");
            if (studentDir.EndsWith("secret_project"))
                continue;
            foreach (var projectDir in Directory.GetDirectories(studentDir)){
                double match = 0;
                double metric = 0;
                if (!projectDir.Contains("meta_project")){
                    string projectNo = projectDir.Substring(projectDir.LastIndexOf("project") + 7);
                    MethodInfo method = Utility.GetMethodDefinition(
                                Utility.GetAssemblyForProject(projectDir), "Program", "Puzzle");
                    foreach (var test in tests){
                        object result;
                        try {
                            result = method.Invoke(null, test.TestInputs.ToArray());
                        }
                        catch (Exception e){
                            result = e;
                        }
                        if (result is Exception){
                            if (test.TestOuput is Exception){
                                string type1 = ((Exception)result).InnerException.GetType().ToString();
                                string type2 = ((Exception)test.TestOuput).InnerException.GetType().ToString();
                                if (type1 == type2){
                                    match++;
                                }
                            }
                        }
                        else {
                            if (test.TestOuput is Exception)
                                continue;
                            if (result == null){
                                if (test.TestOuput == null)
                                    match++;
                            }
                            else if (result is Int32){
                                if ((int)result == (int)test.TestOuput)
                                    match++;
                            }
                            else if (result is Double){
                                if ((double)result == (double)test.TestOuput)
                                    match++;
                            }
                            else if (result is String){
                                if ((string)result == (string)test.TestOuput)
                                    match++;
                            }
                            else if (result is Byte){
                                if ((byte)result == (byte)test.TestOuput)
                                    match++;
                            }
                            else if (result is Char){
                                if ((char)result == (char)test.TestOuput)
                                    match++;
                            }
                            else if (result is Double){
                                if ((double)result == (double)test.TestOuput)
                                    match++;
                            }
                            else if (result is Boolean){
                                if ((bool)result == (bool)test.TestOuput)
                                    match++;
                            }
                            else if (result is Int32[]){
                                int[] array1 = (int[])result;
                                int[] array2 = (int[])test.TestOuput;
                                if (array1.Length == array2.Length){
                                    bool equal = true;
                                    for (int i = 0; i < array1.Length; i++){
                                        if (array1[i] != array2[i]){
                                            equal = false;
                                            break;
                                        }
                                    }
                                    if (equal)
                                        match++;
                                }
                            }
                            else{
                                throw new Exception("Not handled return type at " + projectDir);
                            }
                        }
                    }
                    metric = match / tests.Count;
                    sb.AppendLine(projectNo + "\t" + match + "\t" + tests.Count + "\t" + metric);
                }
            }
            File.WriteAllText(studentDir + @"\Metric2.txt", sb.ToString());
        }
    }
}