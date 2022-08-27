// Напишите программу, которая найдёт точку пересечения двух прямых, заданных уравнениями y = k1 * x + b1, y = k2 * x + b2; значения b1, k1, b2 и k2 задаются пользователем.

// Метод создает и заполняет двумерный массив координатами X и Y двух прямых вида y = kx + b
// первая строка принимает значение координаты X (общая для двух прямых)
// вторая строка принимает значение координаты Y первой прямой
// третья строка принимает значение координаты Y второй прямой
double[,] FillArray(int size, int k1, int b1, int k2, int b2, double x)
{
    double[,] array = new double[3, size + 1];
    for (int i = 0; i <= size; i++)
    {
        array[0, i] = x + i * 0.01;
        array[1, i] = k1 * (x + i * 0.01) + b1;
        array[2, i] = k2 * (x + i * 0.01) + b2;
    }
    return array;
}

// Метод возвращет значение функции вида y = kx + b, принимая на входе значение координаты x и коэффициентов k и b
double YOfLine(double x, int k, int b)
{
    double y = k * x + b;
    return y;
}

Console.Clear();
Console.Write("Введите коэффициент k для первой прямой: ");
int k1 = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите коэффициент b для первой прямой: ");
int b1 = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите коэффициент k для второй прямой: ");
int k2 = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите коэффициент b для второй прямой: ");
int b2 = Convert.ToInt32(Console.ReadLine());
if (k1 == k2)
{
    Console.WriteLine("Прямые параллельны");
}
else
{
    int minBorder = 0;
    int maxBorder = 1;

    double diffMin = YOfLine(minBorder, k1, b1) - YOfLine(minBorder, k2, b2);
    double diffMax = YOfLine(maxBorder, k1, b1) - YOfLine(maxBorder, k2, b2);
    Console.WriteLine($"{diffMin}, {diffMax}");

    // Находим координаты X участка, на котором прямые пересекаются
    while ((diffMax * diffMin) > 0)
    {
        if (Math.Abs(diffMax) > Math.Abs(diffMin))
        {
            maxBorder = minBorder;
            minBorder -= 1;
        }
        else
        {
            minBorder = maxBorder;
            maxBorder += 1;
        }
        diffMin = YOfLine(minBorder, k1, b1) - YOfLine(minBorder, k2, b2);
        diffMax = YOfLine(maxBorder, k1, b1) - YOfLine(maxBorder, k2, b2);
    }

    // Определяем координаты точки пересечения. За точку пересечения примем координаты X и Y первой прямой с минимальной разностью значений координат Y обеих прямых
    // Создаем двумерный массив [3,101]. Шаг координаты X примем равный 0,01
    double[,] newArray = FillArray(100, k1, b1, k2, b2, minBorder);
    double minValue = Math.Abs(newArray[1, 0] - newArray[2, 0]);
    double[] crossPoint = {newArray[0, 0], newArray[1, 0]};
    for (int i = 1; i < newArray.GetLength(1); i++)
    {
        if ((Math.Abs(newArray[1, i] - newArray[2, i])) < minValue)
        {
            crossPoint[0] = newArray[0, i];
            crossPoint[1] = newArray[1, i];
            minValue = Math.Abs(newArray[1, i] - newArray[2, i]);
        }
    }

    Console.WriteLine($"Координаты точки пересечения: ({crossPoint[0]:f2}, {crossPoint[1]:f2})");
    Console.WriteLine();
    
    // Произведем проверку определения точки пересечения математическим методом
    Console.WriteLine("Проверка координат точки перес ечения математическим методом:");
    double xCrossPoint = (double)(b2 - b1) / (k1 - k2);
    Console.WriteLine($"X = {xCrossPoint}, Y = {YOfLine(xCrossPoint, k2, b2)}");
}

