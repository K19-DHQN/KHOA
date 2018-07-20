public static void ExtractPexTests(string topDir) {
    foreach (string taskDir in Directory.GetDirectories(topDir)) {
        MethodInfo method = null;
        foreach (var studentDir in Directory.GetDirectories(taskDir)) {
            if (studentDir.EndsWith("secret_project")) {
                string assemblyFile = FileModifier.GetAssemblyFileOfProject(studentDir);
                method = RunTest.GetMethodDefinition(assemblyFile, "Program", "Puzzle");
                List<Test> tests = ExtractTestsForProject(studentDir, method);
                if (tests != null) {
                    string fileName = studentDir + @"\PexTests.xml";
                    Serializer.SerializeTests(tests, fileName);
                }
                break;
            }
        }
        foreach (var studentDir in Directory.GetDirectories(taskDir)) {
            if (studentDir.EndsWith("secret_project")) {
                continue;
            }
            foreach (var projectDir in Directory.GetDirectories(studentDir)) {
                List<Test> tests = ExtractTestsForProject(projectDir, method);
                if (tests != null) {
                    string fileName = projectDir + @"\PexTests.xml";
                    Serializer.SerializeTests(tests, fileName);
                }
            }
        }
    }
}