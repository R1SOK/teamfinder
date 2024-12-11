using System;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CW2_example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            // Task_1:
            var error = new int[0];
            var array = new int[] { 1, 5, -5, 4, 4, -1, 3, -2, 5, 5, 3, -4, -1, 2, 3 };
            var matrix = new int[,] { { 1, 2, 3, 4, 5 }, { 5, 0, 3, 2, 3 }, { 5, 5, 3, 4, 2 }, { 1, 2, 2, 1, 0 }, { 1, 2, 4, 3, 2 } };
            var matrix2 = new int[,] { { 1, 2, 3, 4 }, { 5, 0, 3, 2 }, { 5, 5, 3, 4 }, { 1, 2, 2, 1 } };
            // input:   1 5 -5 4 4 -1 3 -2 5 5 3 -4 -1 2 3
            program.Print(array);
            program.Task_1(array);
            // output:  1 5 -5 4 4 -1 3 18 5 5 3 18 -1 2 3
            program.Print(array);

            // Task_2:
            // input:   1 5 -5 4 4 -1 3 18 5 5 3 18 -1 2 3
            program.Print(array);
            program.Task_2(array);
            // output:  1 5 -5 4 3 -1 4 18 5 5 3 18 -1 2 3
            program.Print(array);

            // Task_3:
            // input: 
            /*1 2 3 4 5
              5 0 3 2 3
              5 5 3 4 2
              1 2 2 1 0
              1 2 4 3 2*/
            program.Print(matrix);
            program.Task_3(matrix);
            // output:
            /*1 2 3 4 5
              5 0 3 2 3
              5 5 20 4 2
              1 2 2 1 0
              1 2 4 3 2*/
            program.Print(matrix);

            // Task_4:
            // input:   1 5 -5 4 3 -1 4 18 5 5 3 18 -1 2 3
            program.Print(array);
            program.Task_4(ref array);
            // output:  1 5 -5 4 3 -1 4 18 5 5 3 18 -1 13 2 3
            program.Print(array);

            // Task_5:
            // input: 
            /*1 2 3 4 5
              5 0 3 2 3
              5 5 20 4 2
              1 2 2 1 0
              1 2 4 3 2*/
            program.Print(matrix);
            program.Task_5(ref matrix);
            // output:
            /*1 2 2 1 0
            1 2 4 3 2
            5 0 3 2 3
            1 2 3 4 5
            5 5 20 4 2*/
            program.Print(matrix);

        }
        void Print(int[] array)
        {
            Console.WriteLine();
            foreach (int i in array) Console.Write(i + " ");
            Console.WriteLine();
        }
        void Print(int[,] matrix)
        {
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        // В одномерном массиве найти сумму индексов максимальных элементов. Заменить все четные отрицательные элементы найденной суммой. 
        void Task_1(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
                if (array[i] > max)
                    max = array[i];
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] == max)
                    sum += i;
            for (int i = 0; i < array.Length; i++)
                if (array[i] < 0 && array[i] % 2 == 0)
                    array[i] = sum;
        }
        // В одномерном массиве отсортировать все положительные элементы с четными индексами,
        // расположенные между максимальным и минимальным элементом, по возрастанию.
        // Остальные элементы оставить на своих местах. 
        void Task_2(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int imax = 0, imin = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[imax])
                    imax = i;
                if (array[i] < array[imin])
                    imin = i;
            }
            for (int k = Math.Min(imin, imax); k < Math.Max(imin, imax); k++)
            {
                if (array[k] > 0 && k % 2 == 0)
                {
                    int i = k, j = k - 2;
                    while (j >= 0)
                    {
                        if (array[j] > 0 && array[i] < array[j])
                        {
                            var temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                            i = j;
                        }
                        j -= 2;
                    }
                }
            }
        }
        //  Дана квадратная матрица A. Найти сумму элементов правого верхнего и левого нижнего квадратов.
        //  Квадраты образуются пересечением срединной строки и срединного столбца матрицы.
        //  Элементы этих строк и столбцов не считать частью квадратов.
        //  Заменить центральный элемент матрицы найденной суммой. 
        void Task_3(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(0) != matrix.GetLength(1)) return;
            int sum = 0, n = matrix.GetLength(0);
            int rest = n % 2;
            for (int i = n / 2 + 1; i < n; i++)
            {
                for (int j = 0; j < n / 2 - 1 + rest; j++)
                {
                    sum += matrix[i, j];
                    sum += matrix[j, i];
                }
            }
            matrix[n / 2, n / 2] = sum;
            if (rest == 0)
            {
                matrix[n / 2 - 1, n / 2] = sum;
                matrix[n / 2 - 1, n / 2 - 1] = sum;
                matrix[n / 2, n / 2 - 1] = sum;
            }
        }
        // В одномерном массиве вставить сумму минимального и максимального элементов после последнего отрицательного элемента.
        // Вставку элемента осуществлять в методе InsertItem(array, item, index).
        // Метод должен возвращать новый массив. 
        void Task_4(ref int[] array)
        {
            if (array == null || array.Length == 0) return;
            int max = array[0], min = array[0], k = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max) max = array[i];
                if (array[i] < min) min = array[i];
                if (array[i] < 0) k = i;
            }
            if (k > -1)
                array = InsertItem(array, min + max, k);
        }
        int[] InsertItem(int[] array, int item, int index)
        {
            if (index < 0 || index >= array.Length) return array;
            int[] newArray = new int[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                if (i <= index) newArray[i] = array[i];
                else newArray[i + 1] = array[i];
            }
            newArray[index + 1] = item;
            return newArray;
        }
        //  Отсортировать строки матрицы А по возрастанию сумм положительных элементов.
        //  Суммирование положительных элементов в строках оформить методом SumRow(matrix, row).
        //  Метод должен возвращать целое число.
        //  Сортировку строк оформить методом SortByPatternAscending(matrix, pattern). 
        void Task_5(ref int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0) return;
            int[] sums = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                sums[i] = SumRow(matrix, i);
            SortByPatternDescending(matrix, sums);
        }
        int SumRow(int[,] matrix, int row)
        {
            if (row < 0 || row >= matrix.GetLength(0)) return 0;
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
                sum += matrix[row, i];
            return sum;
        }
        void SortByPatternDescending(int[,] matrix, int[] pattern)
        {
            for (int i = 1; i < pattern.Length; i++)
            {
                int key = pattern[i], j = i - 1;
                int[] row = new int[matrix.GetLength(1)];
                for (int k = 0; k < matrix.GetLength(1); k++)
                    row[k] = matrix[i, k];
                while (j >= 0 && pattern[j] > key)
                {
                    pattern[j + 1] = pattern[j];
                    for (int k = 0; k < matrix.GetLength(1); k++)
                        matrix[j + 1, k] = matrix[j, k];
                    j--;
                }
                pattern[j + 1] = key;
                for (int k = 0; k < matrix.GetLength(1); k++)
                    matrix[j + 1, k] = row[k];
            }
        }
        // В одномерном массиве поменять местами первый отрицательный элемент и минимальный элемент,
        // расположенный после последнего отрицательного элемента. 
        void Task_1(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int firstNeg = -1, lastNeg = -1, imin = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] < 0)
                {
                    firstNeg = i; break;
                }
            for (int i = array.Length - 1; i >= 0; i--)
                if (array[i] < 0)
                {
                    lastNeg = i; break;
                }
            imin = lastNeg + 1;
            for (int i = lastNeg + 1; i < array.Length; i++)
                if (array[i] < array[imin])
                    imin = i;
            if (firstNeg > -1 && imin < array.Length)
            {
                var temp = array[imin];
                array[imin] = array[firstNeg];
                array[firstNeg] = temp;
            }
        }
        //В одномерном массиве отсортировать все четные элементы по убыванию, оставив нечетные на своих местах. 
        void Task_2(int[] array)
        {
            if (array == null || array.Length == 0) return;
            for (int k = 0; k < array.Length; k++)
            {
                if (array[k] % 2 == 0)
                {
                    int i = k, j = k - 1;
                    while (j >= 0)
                    {
                        if (array[j] % 2 == 0 && array[j] < array[i])
                        {
                            var temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                            i = j;
                        }
                        j--;
                    }
                }
            }
        }
        // Дана матрица А. Сдвинуть четные элементы каждого столбца вниз после нечетных с сохранением их порядка.  
        void Task_3(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(0) != matrix.GetLength(1)) return;
            int n = matrix.GetLength(0), m = matrix.GetLength(1);
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n - 1;)
                {
                    if (i >= 0 && matrix[i, j] % 2 == 0 && matrix[i + 1, j] % 2 == 1)
                    {
                        var temp = matrix[i, j];
                        matrix[i, j] = matrix[i + 1, j];
                        matrix[i + 1, j] = temp;
                        i--;
                    }
                    else i++;
                }
            }
        }
        // В одномерном массиве удалить все отрицательные элементы, расположенные сразу после максимального элемента.
        // Удаление элемента осуществлять в методе DeleteItem(array, index). 
        void Task_4(ref int[] array)
        {
            if (array == null || array.Length == 0) return;
            int max = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max) max = array[i];
            }
            for (int i = array.Length - 2; i > 0; i--)
            {
                if (array[i] == max && array[i + 1] < 0)
                    array = DeleteItem(array, i + 1);
            }
        }
        int[] DeleteItem(int[] array, int index)
        {
            if (index < 0 || index >= array.Length || array[index + 1] >= 0) return array;
            int[] newArray = new int[array.Length - 1];
            for (int i = 0; i < newArray.Length; i++)
            {
                if (i < index) newArray[i] = array[i];
                else newArray[i] = array[i + 1];
            }
            return newArray;
        }
        // В матрице А отсортировать по возрастанию элементы первого по счету столбца.
        // Вставить в матрицу вектор Е, состоящий из элементов главной диагонали,
        // в качестве новой строки с сохранением упорядоченности элементов первого столбца.
        // Сортировку первого столбца в матрице оформить методом SortColumnAscending(matrix, column).
        // Вставку вектора оформить методом InsertRow(matrix, array). 
        void Task_5(ref int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0) return;
            SortColumnAscending(matrix, 0);
            int[] array = new int[matrix.GetLength(0)];
            for (int i = 0; i < array.Length; i++)
                array[i] = matrix[i, i];
            matrix = InsertRow(matrix, array);
        }
        void SortColumnAscending(int[,] matrix, int col)
        {
            int i = 1, j = 2;
            while (i < matrix.GetLength(0))
            {
                if (i == 0 || matrix[i, col] > matrix[i - 1, col])
                {
                    i = j;
                    j++;
                }
                else
                {
                    var temp = matrix[i, col];
                    matrix[i, col] = matrix[i - 1, col];
                    matrix[i - 1, col] = temp;
                    i--;
                }
            }
        }
        int[,] InsertRow(int[,] matrix, int[] array)
        {
            if (matrix.GetLength(1) != array.Length) return matrix;
            int[,] newMatrix = new int[matrix.GetLength(0) + 1, matrix.GetLength(1)];
            int rowIndex = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, 0] <= array[0])
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        newMatrix[i, j] = matrix[i, j];
                    rowIndex++;
                }
                else
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        newMatrix[i + 1, j] = matrix[i, j];
                }
            }
            for (int j = 0; j < matrix.GetLength(1); j++)
                newMatrix[rowIndex, j] = array[j];
            return newMatrix;
        }

        // В-1

        // В одномерном массиве заменить отрицательные элементы суммой положительных элементов массива, расположенных до максимального.
        void Task_1(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int sum = 0, imax = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] > array[imax])
                {
                    imax = i;
                }
            for (int i = 0; i < imax; i++)
                if (array[i] > 0)
                {
                    sum += array[i];
                }
            for (int i = 0; i < imax; i++)
                if (array[i] < 0)
                {
                    array[i] = sum;
                }

        }

        // В одномерном массиве отсортировать все отрицательные элементы по убыванию, оставив остальные элементы на своих местах.
        void Task_2(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Сначала извлекаем отрицательные элементы
            int[] negativeElements = new int[array.Length];
            int count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    negativeElements[count] = array[i];
                    count++;
                }
            }

            // Сортируем отрицательные элементы по убыванию (пузырьковая сортировка)
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (negativeElements[j] < negativeElements[j + 1])
                    {
                        // Меняем местами
                        int temp = negativeElements[j];
                        negativeElements[j] = negativeElements[j + 1];
                        negativeElements[j + 1] = temp;
                    }
                }
            }

            // Вставляем отсортированные отрицательные элементы обратно в массив
            int negativeIndex = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    array[i] = negativeElements[negativeIndex];
                    negativeIndex++;
                }
            }
        }
        //Дана матрица А. В каждой строке поменять местами максимальный и первый элементы.
        void Task_3(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                int maxIndex = 0;
                int maxValue = array[i, 0];

                // Находим максимальный элемент и его индекс
                for (int j = 1; j < cols; j++)
                {
                    if (array[i, j] > maxValue)
                    {
                        maxValue = array[i, j];
                        maxIndex = j;
                    }
                }

                // Меняем местами максимальный элемент и первый элемент
                if (maxIndex != 0) // Проверяем, что максимальный элемент не первый
                {
                    int temp = matrix[i, 0];
                    matrix[i, 0] = matrix[i, maxIndex];
                    matrix[i, maxIndex] = temp;
                }
            }
        }
        // В одномерном массиве удалить максимальный элемент. Удаление элемента осуществлять в методе Deleteltem(array, index).
        void Task_4(int[] array)
        {
            // Находим индекс максимального элемента
            int maxIndex = FindMaxIndex(array);

            // Удаляем максимальный элемент
            array = DeleteItem(array, maxIndex);
        }
        static int FindMaxIndex(int[] array)
        {
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
        // Метод для удаления элемента по индексу
        static int[] DeleteItem(int[] array, int index)
        {
            if (array.Length == 0 || index < 0 || index >= array.Length)
            {
                return array; // Возвращаем исходный массив, если он пуст или индекс вне диапазона
            }

            int[] newArray = new int[array.Length - 1];
            for (int i = 0, j = 0; i < array.Length; i++)
            {
                if (i != index)
                {
                    newArray[j] = array[i];
                    j++;
                }
            }
            return newArray;
        }

        //Дана матрица А. Удалить строку,
        //содержащую максимальный элемент в 1-м по счету столбце, и строку, содержащую максимальный элемент в последнем столбце матрицы.
        //Поиск максимального элемента в заданном столбце матрицы оформить методом FindMaxRowIndex(matrix, column).
        //Удаление строки оформить методом DeleteRow(matrix, row).
        void Task_5(int[] array)
        {
            // Находим индексы строк с максимальными элементами в первом и последнем столбцах
            int maxRowIndexFirstColumn = FindMaxRowIndex(matrix, 0);
            int maxRowIndexLastColumn = FindMaxRowIndex(matrix, matrix.GetLength(1) - 1);

            // Удаляем строки, если они не совпадают
            if (maxRowIndexFirstColumn != maxRowIndexLastColumn)
            {
                matrix = DeleteRow(matrix, maxRowIndexLastColumn); // Сначала удаляем последнюю строку
                matrix = DeleteRow(matrix, maxRowIndexFirstColumn);
            }
            else
            {
                matrix = DeleteRow(matrix, maxRowIndexFirstColumn); // Удаляем только одну строку
            }
        }
        static int FindMaxRowIndex(int[,] matrix, int column)
        {
            int maxRowIndex = 0;
            int maxValue = matrix[0, column];

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, column] > maxValue)
                {
                    maxValue = matrix[i, column];
                    maxRowIndex = i;
                }
            }
            return maxRowIndex;
        }

        // Метод для удаления строки по индексу
        static int[,] DeleteRow(int[,] matrix, int row)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (rows <= 1 || row < 0 || row >= rows)
            {
                return matrix; // Возвращаем исходную матрицу, если она пустая или индекс вне диапазона
            }

            int[,] newMatrix = new int[rows - 1, cols];
            for (int i = 0, j = 0; i < rows; i++)
            {
                if (i != row)
                {
                    for (int k = 0; k < cols; k++)
                    {
                        newMatrix[j, k] = matrix[i, k];
                    }
                    j++;
                }
            }
            return newMatrix;
        }






        // Вар2

        // В одномерном массиве поменять местами последний отрицательный элемент и минимальный элемент, расположенный после него.
        void Task_1(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int lastNegativeIndex = -1;
            int minIndex = -1;
            int minValue = int.MaxValue;

            // Находим индекс последнего отрицательного элемента
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (array[i] < 0)
                {
                    lastNegativeIndex = i;
                    break;
                }
            }

            // Если найден последний отрицательный элемент
            if (lastNegativeIndex != -1)
            {
                // Находим минимальный элемент после последнего отрицательного
                for (int i = lastNegativeIndex + 1; i < array.Length; i++)
                {
                    if (array[i] < minValue)
                    {
                        minValue = array[i];
                        minIndex = i;
                    }
                }

                // Если найден минимальный элемент, меняем их местами
                if (minIndex != -1)
                {
                    int temp = array[lastNegativeIndex];
                    array[lastNegativeIndex] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }
        // В одномерном массиве отсортировать все положительные элементы по убыванию, оставив остальные элементы на своих местах.
        void Task_2(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Шаг 1: Извлекаем положительные элементы
            int positiveCount = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                {
                    positiveCount++;
                }
            }

            int[] positiveElements = new int[positiveCount];
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                {
                    positiveElements[index] = array[i];
                    index++;
                }
            }

            // Шаг 2: Сортируем положительные элементы по убыванию (сортировка выбором)
            for (int i = 0; i < positiveElements.Length - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < positiveElements.Length; j++)
                {
                    if (positiveElements[j] > positiveElements[maxIndex])
                    {
                        maxIndex = j;
                    }
                }
                // Меняем местами текущий элемент с максимальным
                if (maxIndex != i)
                {
                    int temp = positiveElements[i];
                    positiveElements[i] = positiveElements[maxIndex];
                    positiveElements[maxIndex] = temp;
                }
            }

            // Шаг 3: Вставляем отсортированные положительные элементы обратно в массив
            index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                {
                    array[i] = positiveElements[index];
                    index++;
                }
            }

        }
        //  Дана матрица А. Поменять местами 1-ю строку со 2-й, 3-ю с 4-й и т. д. Если строк нечетное количество, последнюю оставить нетронутой.
        void Task_3(int[] array)
        {
            if (array == null || array.Length == 0) return;
            int rows = matrix.GetLength(0);
            for (int i = 0; i < rows - 1; i += 2)
            {
                if (rows % 2 == 0)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        // Меняем местами элементы в строках i и i+1
                        int temp = matrix[i, j];
                        matrix[i, j] = matrix[i + 1, j];
                        matrix[i + 1, j] = temp;
                    }
                }
                else
                {
                    for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                    {
                        // Меняем местами элементы в строках i и i+1
                        int temp = matrix[i, j];
                        matrix[i, j] = matrix[i + 1, j];
                        matrix[i + 1, j] = temp;
                    }
                }
            }
            PrintMatrix(array);
        }
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        //  В одномерный массив вставить после последнего отрицательного элемента его порядковый номер в массиве. Вставку элемента осуществлять в методе Insertitem(array, item, index).
        void Task_4(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим индекс последнего отрицательного элемента
            int lastNegativeIndex = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    lastNegativeIndex = i;
                }
            }

            // Если найден отрицательный элемент, вставляем его порядковый номер
            if (lastNegativeIndex != -1)
            {
                int itemToInsert = lastNegativeIndex + 1; // Порядковый номер (индекс + 1)
                array = InsertItem(array, itemToInsert, lastNegativeIndex + 1); // Вставляем после последнего отрицательного
            }
        }
        // Метод для вставки элемента в массив
        static int[] InsertItem(int[] array, int item, int index)
        {
            // Создаем новый массив с увеличенной длиной
            int[] newArray = new int[array.Length + 1];

            // Копируем элементы до индекса
            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }

            // Вставляем новый элемент
            newArray[index] = item;

            // Копируем оставшиеся элементы
            for (int i = index; i < array.Length; i++)
            {
                newArray[i + 1] = array[i];
            }

            return newArray;
        }


        //  Дана матрица А. Найти максимальный элемент
        //  Если он стоит в строке с четным индексом, отсортировать по убыванию элементы в столбцах, расположенных правее столбца с максимальным элементом.
        //  Иначе - отсортировать по убыванию элементы в столбцах, расположенных левее столбца с максимальным элементом.
        //  Поиск максимального элемента в матрице оформить методом FindMax(matrix, out row, out column).
        //  Сортировку элементов в строке оформить методом SortColumnDescending(matrix, column).
        void Task_5(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим максимальный элемент
            FindMax(matrix, out int maxRow, out int maxColumn);
            Console.WriteLine($"\nМаксимальный элемент: {matrix[maxRow, maxColumn]} на позиции ({maxRow}, {maxColumn})");

            // Сортируем столбцы в зависимости от четности строки
            if (maxRow % 2 == 0) // Четный индекс строки
            {
                for (int col = maxColumn + 1; col < matrix.GetLength(1); col++)
                {
                    SortColumnDescending(matrix, col);
                }
            }
            else // Нечетный индекс строки
            {
                for (int col = 0; col < maxColumn; col++)
                {
                    SortColumnDescending(matrix, col);
                }
            }
        }
        // Метод для поиска максимального элемента в матрице
        static void FindMax(int[,] matrix, out int row, out int column)
        {
            int max = int.MinValue;
            row = -1;
            column = -1;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
        }

        // Метод для сортировки столбца по убыванию
        static void SortColumnDescending(int[,] matrix, int column)
        {
            int rows = matrix.GetLength(0);
            // Сортировка столбца с использованием сортировки выбором
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = i + 1; j < rows; j++)
                {
                    if (matrix[i, column] < matrix[j, column])
                    {
                        // Меняем местами элементы
                        int temp = matrix[i, column];
                        matrix[i, column] = matrix[j, column];
                        matrix[j, column] = temp;
                    }
                }
            }
        }










        // Вар 3

        //  В одномерном массиве заменить все отрицательные элементы, кроме первого отрицательного, суммой положительных элементов массива.
        void Task_1(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим сумму положительных элементов и индекс первого отрицательного
            int firstNegativeIndex = -1;
            int positiveSum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    if (firstNegativeIndex == -1)
                    {
                        firstNegativeIndex = i; // Запоминаем индекс первого отрицательного
                    }
                }
                else
                {
                    positiveSum += array[i]; // Суммируем положительные элементы
                }
            }

            // Заменяем все отрицательные элементы, кроме первого
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0 && i != firstNegativeIndex)
                {
                    array[i] = positiveSum;
                }
            }
        }
        // В одномерном массиве отсортировать по убыванию элементы первой половины массива (не считая центральный элемент), элементы второй половины (считая центральный элемент) - по возрастанию.
        void Task_2(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Определяем длину массива и индекс центрального элемента
            int length = array.Length;
            int midIndex = length / 2;

            // Сортируем первую половину (не включая центральный элемент) по убыванию
            for (int i = 0; i < midIndex - 1; i++)
            {
                for (int j = i + 1; j < midIndex; j++)
                {
                    if (array[i] < array[j])
                    {
                        // Меняем местами элементы
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            // Сортируем вторую половину (включая центральный элемент) по возрастанию
            for (int i = midIndex; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (array[i] > array[j])
                    {
                        // Меняем местами элементы
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
        //  Дана матрица А. Найти минимальный элемент матрицы. Строку, содержащую этот элемент, поменять местами с последней строкой.
        void Task_3(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Инициализация переменных для поиска минимального элемента
            int minValue = int.MaxValue;
            int minRowIndex = -1;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            // Поиск минимального элемента и его индекса
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < minValue)
                    {
                        minValue = matrix[i, j];
                        minRowIndex = i; // Запоминаем индекс строки с минимальным элементом
                    }
                }
            }
            // Меняем местами строку с минимальным элементом и последнюю строку
            if (minRowIndex != -1 && minRowIndex != rows - 1)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Меняем элементы местами
                    int temp = matrix[minRowIndex, j];
                    matrix[minRowIndex, j] = matrix[rows - 1, j];
                    matrix[rows - 1, j] = temp;
                }
            }
        }
        // Удалить из одномерного массива первый отрицательный элемент. Удаление элемента осуществлять в методе Deleteltem(array, index).
        void Task_4(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим индекс первого отрицательного элемента
            int indexToDelete = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    indexToDelete = i;
                    break; // Выходим из цикла после нахождения первого отрицательного элемента
                }
            }

            // Если найден отрицательный элемент, удаляем его
            if (indexToDelete != -1)
            {
                array = DeleteItem(array, indexToDelete);
            }
        }
        // Метод для удаления элемента по индексу
        static int[] DeleteItem(int[] array, int index)
        {
            // Создаем новый массив на один элемент меньше
            int[] newArray = new int[array.Length - 1];

            // Копируем элементы, пропуская удаляемый
            for (int i = 0, j = 0; i < array.Length; i++)
            {
                if (i != index)
                {
                    newArray[j] = array[i];
                    j++;
                }
            }

            return newArray;
        }

        //Дана матрица А. Найти максимальный элемент. Если он стоит в столбце с нечетным индексом, отсортировать по возрастанию элементы в строках,
        //расположенных ниже строки с максимальным элементом. Иначе - отсортировать по возрастанию элементы в строках, расположенных выше строки с максимальным элементом.
        //Поиск максимального элемента в матрице оформить методом FindMax(matrix, out row, out column).
        //Сортировку элементов в столбце Оформить методом SortRowAscending(matrix, row).
        void Task_5(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Поиск максимального элемента
            FindMax(matrix, out int maxRow, out int maxColumn);
            int maxElement = matrix[maxRow, maxColumn];

            Console.WriteLine($"\nМаксимальный элемент: {maxElement} (строка: {maxRow}, столбец: {maxColumn})");

            // Проверка индекса столбца
            if (maxColumn % 2 != 0) // Нечетный индекс
            {
                // Сортируем строки ниже строки с максимальным элементом
                for (int i = maxRow + 1; i < matrix.GetLength(0); i++)
                {
                    SortRowAscending(matrix, i);
                }
            }
            else // Четный индекс
            {
                // Сортируем строки выше строки с максимальным элементом
                for (int i = 0; i < maxRow; i++)
                {
                    SortRowAscending(matrix, i);
                }
            }
        }
        // Метод для поиска максимального элемента в матрице
        static void FindMax(int[,] matrix, out int row, out int column)
        {
            int maxValue = int.MinValue;
            row = -1;
            column = -1;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                        row = i;
                        column = j;
                    }
                }
            }
        }
        // Метод для сортировки строки по возрастанию
        static void SortRowAscending(int[,] matrix, int row)
        {
            int cols = matrix.GetLength(1);

            // Сортировка пузырьком
            for (int i = 0; i < cols - 1; i++)
            {
                for (int j = 0; j < cols - 1 - i; j++)
                {
                    if (matrix[row, j] > matrix[row, j + 1])
                    {
                        // Меняем местами элементы
                        int temp = matrix[row, j];
                        matrix[row, j] = matrix[row, j + 1];
                        matrix[row, j + 1] = temp;
                    }
                }
            }
        }




        //Вар 6

        // Найти сумму элементов одномерного массива, расположенных после максимального элемента. Заменить центральный элемент массива (два элемента, если массив четный) найденной суммой.
        void Task_1(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим индекс максимального элемента
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }
            // Вычисляем сумму элементов после максимального
            int sumAfterMax = 0;
            for (int i = maxIndex + 1; i < array.Length; i++)
            {
                sumAfterMax += array[i];
            }
            // Заменяем центральный элемент(ы) на найденную сумму
            int length = array.Length;
            if (length % 2 == 0) // Четный массив
            {
                int mid1 = length / 2 - 1;
                int mid2 = length / 2;
                array[mid1] = sumAfterMax;
                array[mid2] = sumAfterMax;
            }
            else // Нечетный массив
            {
                int mid = length / 2;
                array[mid] = sumAfterMax;
            }
        }

        // В одномерном массиве отсортировать по убыванию элементы, расположенные после минимального.
        void Task_2(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим индекс минимального элемента
            int minIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[minIndex])
                {
                    minIndex = i;
                }
            }
            Console.WriteLine($"Минимальный элемент: {array[minIndex]} (индекс: {minIndex})");

            // Извлекаем элементы после минимального
            int lengthAfterMin = array.Length - minIndex - 1;
            int[] elementsAfterMin = new int[lengthAfterMin];
            for (int i = 0; i < lengthAfterMin; i++)
            {
                elementsAfterMin[i] = array[minIndex + 1 + i];
            }

            // Сортируем элементы по убыванию с помощью сортировки пузырьком
            for (int i = 0; i < lengthAfterMin - 1; i++)
            {
                for (int j = 0; j < lengthAfterMin - 1 - i; j++)
                {
                    if (elementsAfterMin[j] < elementsAfterMin[j + 1])
                    {
                        // Меняем местами
                        int temp = elementsAfterMin[j];
                        elementsAfterMin[j] = elementsAfterMin[j + 1];
                        elementsAfterMin[j + 1] = temp;
                    }
                }
            }

            // Вставляем отсортированные элементы обратно в исходный массив
            for (int i = 0; i < lengthAfterMin; i++)
            {
                array[minIndex + 1 + i] = elementsAfterMin[i];
            }
        }

        // Дана матрица А. В каждой строке заменить максимальный элемент суммой элементов четных столбцов данной строки.
        void Task_3(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Обработка каждой строки
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Находим максимальный элемент и его индекс
                int maxIndex = 0;
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > matrix[i, maxIndex])
                    {
                        maxIndex = j;
                    }
                }

                // Вычисляем сумму элементов четных столбцов
                int sumEvenColumns = 0;
                for (int j = 0; j < matrix.GetLength(1); j += 2) // Четные столбцы
                {
                    sumEvenColumns += matrix[i, j];
                }

                // Заменяем максимальный элемент на сумму
                matrix[i, maxIndex] = sumEvenColumns;
            }
        }

        // В одномерном массиве продублировать максимальный элемент, вставив его сразу после максимального. Вставку элемента осуществлять в методе InsertItem(array, item, index).
        void Task_4(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Находим максимальный элемент и его индекс
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            // Дублируем максимальный элемент
            int maxElement = array[maxIndex];
            array = InsertItem(array, maxElement, maxIndex + 1);
        }
        // Метод для вставки элемента в массив
        static int[] InsertItem(int[] array, int item, int index)
        {
            // Создаем новый массив с увеличенной длиной
            int[] newArray = new int[array.Length + 1];

            // Копируем элементы до указанного индекса
            for (int i = 0; i < index; i++)
            {
                newArray[i] = array[i];
            }

            // Вставляем новый элемент
            newArray[index] = item;

            // Копируем оставшиеся элементы
            for (int i = index; i < array.Length; i++)
            {
                newArray[i + 1] = array[i];
            }

            return newArray;
        }

        // В каждой строке матрицы А найти минимальный элемент. Отсортировать по возрастанию элементы каждой строки, стоящие после минимального.
        // Поиск минимального элемента в строках оформить методом FindMinIndexinRow(matrix, row). Сортировку строки оформить методом SortRowAscending(matrix, row, start, end).
        void Task_5(int[] array)
        {
            if (array == null || array.Length == 0) return;
            // Обработка каждой строки
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Находим индекс минимального элемента в строке
                int minIndex = FindMinIndexinRow(matrix, i);

                // Сортируем элементы после минимального
                if (minIndex < matrix.GetLength(1) - 1) // Проверяем, есть ли элементы после минимального
                {
                    SortRowAscending(matrix, i, minIndex + 1, matrix.GetLength(1) - 1);
                }
            }
        }
        // Метод для поиска индекса минимального элемента в строке
        static int FindMinIndexinRow(int[,] matrix, int row)
        {
            int minIndex = 0;
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                if (matrix[row, j] < matrix[row, minIndex])
                {
                    minIndex = j;
                }
            }
            return minIndex;
        }

        // Метод для сортировки строки по возрастанию в заданном диапазоне
        static void SortRowAscending(int[,] matrix, int row, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                for (int j = start; j < end - (i - start); j++)
                {
                    if (matrix[row, j] > matrix[row, j + 1])
                    {
                        // Меняем местами
                        int temp = matrix[row, j];
                        matrix[row, j] = matrix[row, j + 1];
                        matrix[row, j + 1] = temp;
                    }
                }
            }
        }

    }
