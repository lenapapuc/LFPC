using lexer;

var program = File.ReadAllText(@"C:\Users\User\RiderProjects\Solution1\Lab3\BMI.txt");
Lexer lexer = new Lexer(program);
lexer.Scan();
lexer.PrintResult();