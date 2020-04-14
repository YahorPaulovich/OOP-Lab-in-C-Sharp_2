using System;
using System.Collections;

namespace ClassSet //Вариант 14
{/*№3 Проектирование типов. Классы.
  Задание
  1) Определить класс, указанный в варианте, содержащий:
       Не менее трех конструкторов (с параметрами и без, а также с параметрами по умолчанию);
       статический конструктор (конструктор типа);
       определите закрытый конструктор; предложите варианты его вызова;
       поле - только для чтения (например, для каждого экземпляра сделайте поле только для чтения ID - равно некоторому уникальному номеру
     (хэшу) вычисляемому автоматически на основе инициализаторов объекта);
      поле- константу;
      свойства (get, set) – для всех поле класса (поля класса должны быть закрытыми); Для одного из свойств ограните доступ по set
      в одном из методов класса для работы с аргументами используйте ref-параметры и out-параметры.
      создайте в классе статическое поле, хранящее количество созданных объектов (инкрементируется в конструкторе) 
     и статический метод вывода информации о классе.
      сделайте класс partial
      переопределяете методы класса Object: Equals, для сравнения объектов, GetHashCode; для алгоритма вычисления хэша руководствуйтесь
     стандартными рекомендациями, ToString – вывода строки – информации об объекте.
  2) Создайте несколько объектов вашего типа. Выполните вызов конструкторов, свойств, методов, сравнение объекты, проверьте тип созданного объекта и т.п.
  3) Создайте массив объектов вашего типа. И выполните задание, выделенное курсивом в таблице.
  4) Создайте и выведите анонимный тип (по образцу вашего класса).
  5) Ответьте на вопросы, приведенные ниже*/

    /*Создать класс - Множество. Методы класса реализуют добавление и удаление элемента, пересечение и разность множеств.
      Создать массив объектов. 
      Вывести:
        a) множества только с четными/нечетными элементами;
        b) множества, содержащие отрицательные элементы.*/
    class Program  
    {
        public class CSet
        {
            private const int value = 10;           
            private int _num;          
            public int num { get { return _num; } private set { _num = value; } }

            private string Title;
            public int quantity;
            public static int Count { get; set; } = 0;
            public static CSet CSet_ = new CSet();

            public CSet(int q) { Title = "безымянное множество"; quantity = q; Count++; }
            private CSet(string t) { Title = t; quantity = value; Count++; }
            public CSet(string Title, int quantity)
            {
                this.Title = Title;
                this.quantity = quantity;
                Count++;
            }
            public CSet()
            {
                data = new Hashtable();

                quantity = value;
                Count++;
                Title = $"безымянное множество{Count}";               
            }
            static CSet(){ }          
            //---------------------------------------------------------------------------------------------------------------------------------------------  
            private Hashtable data;           
            public void Add(object item)
            {
                if (!data.ContainsValue(item))
                {
                    data.Add(Hash(item), item);
                }
            }

            public override string ToString()
            {
                string s = null;
                foreach (Object key in data.Keys)
                    s += data[key] + " ";
                return s;
            }

            public string Hash(Object item)
            {
                int hashValue = 0;
                char[] chars;
                string s = item.ToString();
                chars = s.ToCharArray();
                for (int i = 0; i <= chars.GetUpperBound(0); i++)
                    hashValue += (int)chars[i];
                return hashValue.ToString();
            }

            public void Remove(Object item)
            {
                data.Remove(Hash(item));
            }

            public int Size()
            {
                return data.Count;
            }
         
            public override bool Equals(Object item)
            {
                if (item == null || GetType() != item.GetType()) return false;

                CSet temp = (CSet)item;               
                return Title == temp.Title && quantity == temp.quantity;
            }

            public override int GetHashCode()
            {
                return data.GetHashCode();                          
            }

            public CSet Union(CSet aSet)
            {
                CSet tempSet = new CSet();
                foreach (Object hashObject in data.Keys)
                    tempSet.Add(this.data[hashObject]);
                foreach (Object hashObject in aSet.data.Keys)
                    if (!(this.data.ContainsKey(hashObject)))
                    {
                        tempSet.Add(aSet.data[hashObject]);
                    }
                return tempSet;
            }

            public CSet Intersection(CSet aSet)
            {
                CSet tempSet = new CSet();
                foreach (Object hashObject in data.Keys)
                    if (aSet.data.Contains(hashObject))
                    {
                        tempSet.Add(aSet.data[hashObject]);
                    }
                return tempSet;
            }

            public bool isSubset(CSet aSet)
            {
                if (this.Size() > aSet.Size())
                {
                    return false;
                }
                else
                {
                    foreach (Object key in this.data.Keys)
                        if (!(aSet.data.Contains(key)))
                        {
                            return false;
                        }
                    return true;
                }
            }

            public CSet Difference(CSet aSet)
            {
                CSet tempSet = new CSet();
                foreach (Object hashObject in data.Keys)
                    if (!(aSet.data.Contains(hashObject)))
                        tempSet.Add(data[hashObject]);
                return tempSet;
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------                    
            public static void GetInfo(ref CSet cSet)
            {
                CSet_ = cSet;
                Console.WriteLine($"Множество: {CSet_.Title}, количество элементов множества: {CSet_.quantity}; Всего создано объектов: {Count}");
            }
            public static void GetCount(out int C)
            {               
                C = Count;               
            }

            public string ToStringEven()
            {
                Console.Write("Вывод чётных элементов множества:");
                //вывод чётных элементов множества
                string str = null;
                foreach (Object key in data.Keys)                 
                        if (Convert.ToInt32(key) % 2 == 0)
                        {
                            str += data[key] + " ";
                        }                               
                return str;
            }

            public string ToStringUneven()
            {
                Console.Write("Вывод нечётных элементов множества:");
                //вывод нечётных элементов множества
                string str = null;
                foreach (Object key in data.Keys)                                 
                    if (Convert.ToInt32(key) % 2 != 0)
                    {
                        str += data[key] + " ";
                    }                         
                return str;
            }

            public string ToStringNegative()
            {
                Console.Write("Вывод отрицательных элементов множества:");
                //вывод отрицательных элементов множества 
                string str = null;
                foreach (Object key in data.Keys)
                    if (Convert.ToInt32(key) % -1 == Convert.ToInt32(key))
                    {
                        str += data[key] + " ";
                    }
                return str;
            }
        }
        public partial class MyPartialClass
        {
            public void Info()
            {
                Console.WriteLine("MyPartialClass in CSet");
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {         
            const int average = 6;
            CSet setA = new CSet();
            CSet setB = new CSet();

            for (int i = 0; i <= average; i++)           
                setA.Add(i);

            for (int i = 5; i <= 10; i++)           
                setB.Add(i);

            CSet.GetInfo(ref setA);
            Console.WriteLine(setA.ToString());
            Console.WriteLine();
            int c;
            CSet.GetCount(out c);
            Console.WriteLine($"Всего создано объектов: {c}");

            Console.WriteLine(setA.ToStringEven());
            Console.WriteLine(setA.ToStringUneven());
            Console.WriteLine(setA.ToStringNegative());
            Console.WriteLine();

            CSet.GetInfo(ref setB);
            Console.WriteLine(setB.ToString());

            CSet setC = setA.Union(setB);
            Console.WriteLine();
            Console.WriteLine("множество A объединённо с множеством B: " + setC.ToString());

            setC = setA.Intersection(setB);
            Console.WriteLine("пересечение множества A с множеством B: " + setC.ToString());

            setC = setA.Difference(setB);
            Console.WriteLine("симметрическая разница множества A с множеством B: " + setC.ToString());

            setC = setB.Difference(setA);
            Console.WriteLine("симметрическая разница множества B с множеством A: " + setC.ToString());

            if (setB.isSubset(setA))
                Console.WriteLine("множество B является подмножеством множества A !");
            else
                Console.WriteLine("множество B не является подмножеством множества A !");

            Console.WriteLine(new string('-',20));

            Console.WriteLine("Сравнение двух множеств...");
            if (setA.Equals(setB))
                Console.WriteLine("Множество A равносильно множеству B");
            else
                Console.WriteLine("Множество A не равносильно множеству B");

            Console.WriteLine($"\nХэш множества A: {setA.GetHashCode()} \nХэш множества B: {setB.GetHashCode()}");

            // Проверка, является ли переменная setA типа CSet
            Console.WriteLine("\nПроверка, является ли переменная setA типа CSet...");
            if (setA is CSet)
                Console.WriteLine("Переменная setA имеет тип CSet");
            else
                Console.WriteLine("Переменная setA не имеет тип CSet");

            Console.WriteLine(new string('-', 20));
                      
            var SET = new CSet("SET", 7);
            CSet.GetInfo(ref SET);


            Console.ReadKey();
        }
    }
}

