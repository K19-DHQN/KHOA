public static void ComputeMetric1(string topDir)
{
    foreach (var taskDir in Directory.GetDirectories(topDir))
    {
        foreach (var studentDir in Directory.GetDirectories(taskDir))
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("projectNo\t#pass\t#notpass\t#all\tmetric1");
            if (studentDir.EndsWith("secret_project"))
                continue;
            foreach (var projectDir in Directory.GetDirectories(studentDir))
            {
                double pass = 0;
                double notPass = 0;
                double metric = 0;
                if(projectDir.Contains("meta_project"))
                {
                    string projectNo = projectDir.Substring(projectDir.LastIndexOf("meta_project")+12);
                    List<Test> tests = Serializer.DeserializeTests(projectDir + @"\PexTests.xml");
                    MethodInfo method = Utility.GetMethodDefinition(
                            Utility.GetAssemblyForProject(projectDir), "MetaProgram", "Check");
                    foreach (var test in tests)
                    {
                        try
                        {                            
                            object result = method.Invoke(null, test.TestInputs.ToArray());
                            pass++;
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException.Message.Contains("Submission failed"))
                            {
                                notPass++;
                            }
                        }
                    }
                    metric = pass / (notPass + pass);
                    sb.AppendLine(projectNo + "\t" +pass +"\t"+notPass+"\t"+(pass+notPass)+"\t"+metric);
                }
            }
            File.WriteAllText(studentDir + @"\Metric1.txt", sb.ToString());
        }
    }
}