using codec.classes;
int[,] passwordMatrix = { { 2, 1 }, { -1, 4 } };

Console.WriteLine("Insira o caminho para o arquivo de entrada:");

string filePath = Console.ReadLine();

if(filePath == "") {
    Console.WriteLine("Arquivo inválido");
	Console.ReadLine();
	return;
}

Console.WriteLine("Digite uma opção:");
Console.WriteLine("0 - Criptografar Arquivo.");
Console.WriteLine("1 - Decriptografar Arquivo.");

string option = Console.ReadLine();

codec.classes.Matrix matrixPass = new codec.classes.Matrix(passwordMatrix);


string outputFile = "";


switch (option) {
	case "0":
        codec.classes.File originalFile = new codec.classes.File(@filePath);

        Console.WriteLine("Insira o caminho para o arquivo criptografado de saída:");
		outputFile = Console.ReadLine();

        originalFile.Encode(@outputFile, matrixPass);

        break;

	case "1":
        codec.classes.File encodedFile = new codec.classes.File(@filePath);
        outputFile = Console.ReadLine();

        encodedFile.Decode(@outputFile, matrixPass);

        break;

}







