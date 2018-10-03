using System;

namespace Datastructures
{
    public class Matrix
    {
        int N { get; set; }
        int M { get; set; }

        public int[,] backboneMatrix { get; set; }

        public int[][] rowset { get; set; }

        public int[][] colset { get; set; }

        private int N_idx;
        private int M_idx;

        #region <constructors>
        public Matrix()
        {
            new Matrix(1, 1);
        }

        public Matrix(int m, int n)
        {
            N = n;
            M = m;
            backboneMatrix = new int[M, N];

            for (int i = 0; i < M; ++i)
            {
                for (int j = i; j < N; ++j)
                {
                    backboneMatrix[i, j] = 0;
                }
            }

            N_idx = 0;
            M_idx = 0;

            getRowset();
            getColset();
        }

        public Matrix(string filePath)
        {
            getRowset();
        }

        public Matrix(bool isRandom, int m = 0, int n = 0)
        {
            if (n == 0 || m == 0)
            {
                M = N = 10;
            }
            else
            {
                N = n;
                M = m;
            }
            backboneMatrix = new int[M, N];

            if (isRandom)
            {
                Random rnd = new Random();
                for (int i = 0; i < M; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        backboneMatrix[i, j] = rnd.Next(1, 100);
                    }
                }
            }
            else
            {
                int x = 0;
                for (int i = 0; i < M; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        backboneMatrix[i, j] = ++x;
                    }
                }
            }

            getRowset();
            getColset();
        }

        public static Matrix constructUpperTriangle(int m, int n)
        {
            Matrix init = new Matrix(m, n);

            for (int i = 0; i < m; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    init.backboneMatrix[i, j] = 1;
                }
            }

            return init;

            //implement getRowset
        }

        public static Matrix constructStandardMatrix(int m, int n)
        {
            Matrix init = new Matrix(m, n);

            for (int i = 0; i < m; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    if (i == j) init.backboneMatrix[i, j] = 1;
                }
            }

            return init;
            //implement getRowset
        }

        private void getRowset()
        {
            int[] tmp;
            rowset = new int[M][];
            for (int i = 0; i < M; ++i)
            {
                tmp = new int[N];
                for (int j = 0; j < N; ++j)
                {
                    tmp[j] = backboneMatrix[i, j];
                }
                rowset[i] = tmp;
            }
        }

        private void getColset()
        {
            int[] tmp;
            colset = new int[N][];
            for (int i = 0; i < N; ++i)
            {
                tmp = new int[M];
                for (int j = 0; j < M; ++j)
                {
                    tmp[j] = backboneMatrix[j, i];
                }
                colset[i] = tmp;
            }
        }

        #endregion </constructors>
        /****************************************************************************/
        #region <public_methods>

        #region <print_matrix>
        public void printMatrix(string header = "")
        {
            Console.WriteLine(header + "\n");
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    Console.Write("{0,3}", backboneMatrix[i, j]);
                }

                Console.Write("\n");
            }

            Console.WriteLine("-----------------------------------------\n");
        }

        public void printMatrixAsCharacter()
        {
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    Console.Write("{0,3}", (char)backboneMatrix[i, j]);
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("-----------------------------------------\n");
        }
        #endregion </print_matrix>

        #region <mirroring>
        public void mirrorToMainDiagonal()
        {
            int tmp;
            if (M == N)
            {
                for (int i = 0; i < M; ++i)
                {
                    for (int j = i; j < N; ++j)
                    {
                        tmp = backboneMatrix[i, j];
                        backboneMatrix[i, j] = backboneMatrix[j, i];
                        backboneMatrix[j, i] = tmp;
                    }
                }
            }
        }

        public void mirrorToMarginalDiagonal()
        {
            int tmp;
            if (M == N)
            {
                for (int i = M - 1; i >= 0; --i)
                {
                    for (int j = N - i - 1; j < N; ++j)
                    {
                        tmp = backboneMatrix[i, j];
                        backboneMatrix[i, j] = backboneMatrix[(M - 1 - j), (M - 1 - i)];
                        backboneMatrix[(M - 1 - j), (M - 1 - i)] = tmp;
                    }
                }
            }
        }

        public void mirrorTotCenter()
        {
            if (M == N && N % 2 == 0)
            {
                int tmp;
                for (int i = 0; i < M / 2; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        tmp = backboneMatrix[i, j];
                        backboneMatrix[i, j] = backboneMatrix[(M - 1 - i), (M - 1 - j)];
                        backboneMatrix[(M - 1 - i), (M - 1 - j)] = tmp;
                    }
                }
            }
        }
        #endregion </mirroring>

        //<add_elems>
        public bool addElemeLinearly(int toAdd)
        {
            bool ret;
            if (M > M_idx)
            {
                backboneMatrix[M_idx, N_idx] = toAdd;

                N_idx++;
                if (N_idx == N)
                {
                    N_idx = 0;
                    M_idx++;
                }

                ret = true;
            }
            else
            {
                ret = false;
            }

            return ret;
        }
        //</add_elems>

        #endregion </public_methods>

        #region <operator_overloads>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.M == b.M && a.N == b.N)
            {
                Matrix newMatrix = new Matrix(a.M, b.N);

                for (int i = 0; i < a.M; ++i)
                {
                    for (int j = 0; j < a.N; ++j)
                    {
                        newMatrix.backboneMatrix[i, j] = a.backboneMatrix[i, j] + b.backboneMatrix[i, j];
                    }
                }

                return newMatrix;
            }
            else
            {
                throw new InvalidOperationException("Two matrix need to have same dimensions");
            }
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.N == b.M)
            {
                Matrix newMatrix = new Matrix(a.M, b.N);
                for (int i = 0; i < a.M; i++)
                {
                    for (int j = 0; j < b.N; j++)
                    {
                        newMatrix.backboneMatrix[i, j] = Vector.VectorsSkalarMultiply(a.rowset[i], b.colset[j]);
                    }
                }
                return newMatrix;
            }
            else
            {
                throw new InvalidOperationException("Two matrix need to have correct dimensions");
            }
        }

        public static Matrix operator *(Matrix a, int b)
        {
            for (int i = 0; i < a.M; ++i)
            {
                for (int j = 0; j < a.N; ++j)
                {
                    a.backboneMatrix[i, j] = a.backboneMatrix[i, j] * b;
                }
            }

            return a;
        }

        public static Matrix operator *(Matrix a, int[] x)
        {
            //int[] d = { 1 };
            Matrix ret = new Matrix(x.Length, 1);
            if (a.N == x.Length)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    ret[i, 0] = Datastructures.Vector.VectorsSkalarMultiply(a.rowset[i], x);
                }
            }
            return ret;
        }

        public int this[int m, int n]
        {
            get
            {
                return backboneMatrix[m - 1, n - 1];
            }
            set
            {
                backboneMatrix[m, n] = value;
            }
        }
        #endregion </operator_overloads>
    }

    class Vector : Matrix
    {
        public static int VectorsSkalarMultiply(int[] a, int[] b)
        {
            int tot = 0;
            if (a.Length == b.Length)
            {
                int[] ret = new int[a.Length];

                for (int i = 0; i < a.Length; i++)
                {
                    ret[i] = a[i] * b[i];
                    tot += ret[i];
                }

                return tot;
            }

            throw new InvalidOperationException("Two vectors need to have same length");

        }
    }
}

