using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace codec.classes{
    internal class File{
        private string Path;

        public File(string path) {
            this.Path = path;
        }

        private string ReadFile() {
            string textFile = "";

            if (!System.IO.File.Exists(this.Path)) {
                Console.WriteLine("This file do not exist");
                return "";
            }
            
            textFile = System.IO.File.ReadAllText(this.Path);

            return textFile;
        }

        private void WriteFile(string path, string fileContent) {

            System.IO.File.WriteAllText(path, fileContent);
        }

        private byte[] GetASCII() {
            string textFile = ReadFile();

            int count = textFile.Count();

            if(count % 2 != 0) {
                textFile = textFile + " ";
            }


            // Convert the string into a byte[].
            byte[] asciiBytes = Encoding.ASCII.GetBytes(textFile);

            return asciiBytes;
        }

        private string GetText(byte[] asciiBytes) {
                        
            string textFile = Encoding.ASCII.GetString(asciiBytes);

            return textFile;
        }


        public void Encode(string outputPath, Matrix matrixPass) {
            byte[] asciiBytes = GetASCII();

            int[,] passwordMatrix = matrixPass.GetMatrix();

            byte[] bytesCodec = TextCodec(asciiBytes, passwordMatrix);
            
            string textEncoded = GetText(bytesCodec);

            WriteFile(outputPath, textEncoded);
        }

        public void Decode(string outputPath, Matrix matrixPass) {

            byte[] bytes = GetASCII();

            int[,] passwordMatrixInv = matrixPass.MatrixDecoder();

            byte[] bytesCodec = TextCodec(bytes, passwordMatrixInv);

            string textDecoded = GetText(bytesCodec);

            WriteFile(outputPath, textDecoded);

        }

        private byte[] TextCodec(byte[] asciiBytes, int[,] passwordMatrix) {

            int itemsCounted = 0;
            byte[] bytesCodec = new byte[asciiBytes.Count()];

            while (asciiBytes.Count() != itemsCounted) {

                for (int i = 0; i < 2; i++) {

                    int auxResult = passwordMatrix[i, 0] * asciiBytes[itemsCounted] +
                                      passwordMatrix[i, 1] * asciiBytes[itemsCounted + 1];

                    int result = auxResult % 127;

                    if(result < 0) {
                        result = -1 * result;
                    }

                    bytesCodec[itemsCounted + i] = Convert.ToByte(result);

                }

                itemsCounted += 2;

            }

            return bytesCodec;
        }

    }
}
