using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codec.classes {
    internal class Matrix {
        
        private int[,] MatrixSquare;  

        public Matrix(int[,] matrix) {
            this.MatrixSquare = matrix;
        }

        private int[,] MatrixInverse() {

            int[,] passwordMatrix = this.MatrixSquare;
            int[,] passwordMatrixInverse = new int[2, 2];

            //inverting positions
            passwordMatrixInverse[0, 0] = passwordMatrix[1, 1];
            passwordMatrixInverse[1, 1] = passwordMatrix[0, 0];

            //changing signal
            passwordMatrixInverse[0, 1] = -1 * passwordMatrix[0, 1];
            passwordMatrixInverse[1, 0] = -1 * passwordMatrix[1, 0];

            return passwordMatrixInverse;
        }

        public int DetMatrix() {

            int[,] matrix = this.MatrixSquare;

            int det = matrix[0,0] * matrix[1,1] - matrix[0,1] * matrix[1,0];

            return det;
                
        }

        public int[,] MatrixDecoder() {

            int[,] matrixInverse = MatrixInverse();

            int det = DetMatrix();

            int i = 1;

            while((det * i) % 127 != 1) {
                i++;
            }

            for(int j= 0; j < 2; j++) {
                for(int k= 0; k < 2; k++) {

                    int item = matrixInverse[j, k] * i;

                    if (item < 0) {
                        item = item + 127;
                    } else {
                        item = item % 127;
                    }

                    matrixInverse[j, k] = item;
                }
            }

            return matrixInverse;

        }

        public int[,] GetMatrix() {
            return this.MatrixSquare;
        }

        
    }
}
