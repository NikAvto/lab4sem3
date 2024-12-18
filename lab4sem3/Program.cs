using System;

//Классы для задания 1
public class MyMatrix
{
    private int[,] matrix;
    public int Rows { get; }
    public int Cols { get; }

    // Конструктор
    public MyMatrix(int rows, int cols, int minVal, int maxVal)
    {
        Rows = rows;
        Cols = cols;
        matrix = new int[rows, cols];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(minVal, maxVal + 1);
            }
        }
    }

    // Индексатор для доступа к элементам матрицы
    public int this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                throw new IndexOutOfRangeException("Индекс вне границ матрицы.");
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                throw new IndexOutOfRangeException("Индекс вне границ матрицы.");
            matrix[row, col] = value;
        }
    }

    // Метод для сложения матриц
    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new InvalidOperationException("Матрицы должны иметь одинаковые размеры для сложения.");

        MyMatrix result = new MyMatrix(a.Rows, a.Cols, 0, 0);

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Cols; j++)
                result[i, j] = a[i, j] + b[i, j];

        return result;
    }

    // Метод для вычитания матриц
    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new InvalidOperationException("Матрицы должны иметь одинаковые размеры для вычитания.");

        MyMatrix result = new MyMatrix(a.Rows, a.Cols, 0, 0);

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Cols; j++)
                result[i, j] = a[i, j] - b[i, j];

        return result;
    }

    // Метод для умножения матриц
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a.Cols != b.Rows)
            throw new InvalidOperationException("Число столбцов первой матрицы должно быть равно числу строк второй матрицы.");

        MyMatrix result = new MyMatrix(a.Rows, b.Cols, 0, 0);

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < b.Cols; j++)
                for (int k = 0; k < a.Cols; k++)
                    result[i, j] += a[i, k] * b[k, j];

        return result;
    }

    // Метод для умножения матрицы на число
    public static MyMatrix operator *(MyMatrix matrix, int scalar)
    {
        MyMatrix result = new MyMatrix(matrix.Rows, matrix.Cols, 0, 0);

        for (int i = 0; i < matrix.Rows; i++)
            for (int j = 0; j < matrix.Cols; j++)
                result[i, j] = matrix[i, j] * scalar;

        return result;
    }

    // Метод для деления матрицы на число
    public static MyMatrix operator /(MyMatrix matrix, int scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Деление на ноль недопустимо.");

        MyMatrix result = new MyMatrix(matrix.Rows, matrix.Cols, 0, 0);

        for (int i = 0; i < matrix.Rows; i++)
            for (int j = 0; j < matrix.Cols; j++)
                result[i, j] = matrix[i, j] / scalar;

        return result;
    }

    // Метод для отображения матрицы
    public override string ToString()
    {
        string output = "";

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                output += $"{matrix[i, j]} ";
            output += "\n";
        }

        return output.Trim();
    }
}
//Классы для задания 2
public class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return $"{Name}, Year: {ProductionYear}, Max Speed: {MaxSpeed} km/h";
    }
}

public class CarComparer : IComparer<Car>
{
    public enum SortCriteria
    {
        Name, ProductionYear, MaxSpeed
    }

    private readonly SortCriteria _criteria;

    public CarComparer(SortCriteria criteria)
    {
        _criteria = criteria;
    }

    public int Compare(Car x, Car y)
    {
        switch (_criteria)
        {
            case SortCriteria.Name:
                return string.Compare(x.Name, y.Name);
            case SortCriteria.ProductionYear:
                return x.ProductionYear.CompareTo(y.ProductionYear);
            case SortCriteria.MaxSpeed:
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            default:
                return 0;
        }
    }
}

//Классы для задания 3
public class CarCatalog
{
    private List<Car> _cars;

    public CarCatalog()
    {
        _cars = new List<Car>();
    }

    public void AddCar(Car car)
    {
        _cars.Add(car);
    }

    // Прямой проход с первого элемента до последнего
    public IEnumerable<Car> GetCars()
    {
        foreach (var car in _cars)
        {
            yield return car;
        }
    }

    // Обратный проход от последнего к первому
    public IEnumerable<Car> GetCarsReverse()
    {
        for (int i = _cars.Count - 1; i >= 0; i--)
        {
            yield return _cars[i];
        }
    }

    // Проход по элементам массива с фильтром по году выпуска
    public IEnumerable<Car> GetCarsByYear(int year)
    {
        foreach (var car in _cars)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    // Проход по элементам массива с фильтром по максимальной скорости
    public IEnumerable<Car> GetCarsByMaxSpeed(int maxSpeed)
    {
        foreach (var car in _cars)
        {
            if (car.MaxSpeed >= maxSpeed)
            {
                yield return car;
            }
        }
    }
}

// Пример использования класса MyMatrix
class Program
{
    static void Main(string[] args)
    {
        //Задание 1
        Console.WriteLine("Задание 1\n");
        Console.Write("Введите количество строк: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Введите количество столбцов: ");
        int cols = int.Parse(Console.ReadLine());

        Console.Write("Введите минимальное значение: ");
        int minVal = int.Parse(Console.ReadLine());

        Console.Write("Введите максимальное значение: ");
        int maxVal = int.Parse(Console.ReadLine());

        MyMatrix matrix1 = new MyMatrix(rows, cols, minVal, maxVal);
        Console.WriteLine("Первая матрица:");
        Console.WriteLine(matrix1);

        MyMatrix matrix2 = new MyMatrix(rows, cols, minVal, maxVal);
        Console.WriteLine("\nВторая матрица:");
        Console.WriteLine(matrix2);

        // Сложение матриц
        var sumMatrix = matrix1 + matrix2;
        Console.WriteLine("\nСумма матриц:");
        Console.WriteLine(sumMatrix);

        // Вычитание матриц
        var diffMatrix = matrix1 - matrix2;
        Console.WriteLine("\nРазность матриц:");
        Console.WriteLine(diffMatrix);

        // Умножение матриц
        var productMatrix = matrix1 * matrix2;

        // Умножение первой матрицы на вторую требует совместимости по размеру.
        // Для демонстрации создадим новую матрицу с подходящими размерами.
        var compatibleMatrix2 = new MyMatrix(cols, rows, minVal, maxVal);
        productMatrix = matrix1 * compatibleMatrix2;

        Console.WriteLine("\nПроизведение матриц:");
        Console.WriteLine(productMatrix);

        // Умножение матрицы на число
        var scalarProduct = matrix1 * 2;
        Console.WriteLine("\nУмножение первой матрицы на 2:");
        Console.WriteLine(scalarProduct);

        // Деление матрицы на число
        var scalarDivision = matrix1 / 2;
        Console.WriteLine("\nДеление первой матрицы на 2:");
        Console.WriteLine(scalarDivision);

        // Доступ к элементам матрицы через индексатор
        var element = matrix1[0, 0];
        Console.WriteLine($"\nЭлемент в первой строке и первом столбце: {element}");
        
        //Задание 2
        Console.WriteLine("\nЗадание 2\n");
        // Создаем массив автомобилей
        List<Car> cars = new List<Car>
        {
            new Car("Toyota Corolla", 2020, 180),
            new Car("Ford Mustang", 2018, 250),
            new Car("Honda Civic", 2021, 200),
            new Car("BMW M3", 2019, 280),
            new Car("Audi A4", 2022, 240)
        };

        // Сортировка по имени
        Console.WriteLine("Сортировка по имени:");
        cars.Sort(new CarComparer(CarComparer.SortCriteria.Name));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        // Сортировка по году выпуска
        Console.WriteLine("\nСортировка по году выпуска:");
        cars.Sort(new CarComparer(CarComparer.SortCriteria.ProductionYear));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        // Сортировка по максимальной скорости
        Console.WriteLine("\nСортировка по максимальной скорости:");
        cars.Sort(new CarComparer(CarComparer.SortCriteria.MaxSpeed));
        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }

        // Задание 3
        Console.WriteLine("\nЗадание 3\n");
        CarCatalog catalog = new CarCatalog();

        // Добавление автомобилей в каталог
        catalog.AddCar(new Car("Toyota", 2020, 180));
        catalog.AddCar(new Car("Ford", 2018, 200));
        catalog.AddCar(new Car("BMW", 2021, 240));
        catalog.AddCar(new Car("Audi", 2019, 220));

        Console.WriteLine("Прямой проход по автомобилям:");
        foreach (var car in catalog.GetCars())
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nОбратный проход по автомобилям:");
        foreach (var car in catalog.GetCarsReverse())
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nАвтомобили, выпущенные в 2019 году:");
        foreach (var car in catalog.GetCarsByYear(2019))
        {
            Console.WriteLine(car);
        }

        Console.WriteLine("\nАвтомобили с максимальной скоростью не менее 200 км/ч:");
        foreach (var car in catalog.GetCarsByMaxSpeed(200))
        {
            Console.WriteLine(car);
        }
    }
}