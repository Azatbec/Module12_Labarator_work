using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12_Labarator_Work_2
{
    public abstract class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public abstract void PerformActions();
    }

    public class Student : User
    {
        public Student(string name, string email) : base(name, email) { }

        public override void PerformActions()
        {
            Console.WriteLine($"{Name} (Студент) может выполнять следующие действия:");
            Console.WriteLine("1. Посмотреть курсы.");
            Console.WriteLine("2. Зарегистрируйтесь на курс.");
            Console.WriteLine("3. Пройдите тесты.");
            Console.WriteLine("4. Оставляйте отзывы.");
        }
    }

    public class Instructor : User
    {
        public Instructor(string name, string email) : base(name, email) { }

        public override void PerformActions()
        {
            Console.WriteLine($"{Name} (Инструктор) может выполнять следующие действия:");
            Console.WriteLine("1.Создавайте и редактируйте курсы. ");
            Console.WriteLine("2.Добавьте материалы курса.");
            Console.WriteLine("3. Создавайте тесты.");
            Console.WriteLine("4. Посмотреть успеваемость учащихся.");
            Console.WriteLine("5. Умеренные отзывы.");
        }
    }

    public class Administrator : User
    {
        public Administrator(string name, string email) : base(name, email) { }

        public override void PerformActions()
        {
            Console.WriteLine($"{Name} (Администратор) может выполнять следующие действия:");
            Console.WriteLine("1.Управление учетными записями пользователей. ");
            Console.WriteLine("2. Manage course categories.");
            Console.WriteLine("3. Просмотр системной аналитики.");
        }
    }
    public class Course
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public Instructor Instructor { get; set; }
        public List<string> Materials { get; set; } = new List<string>();
        public List<Test> Tests { get; set; } = new List<Test>();

        public Course(string courseName, string description, Instructor instructor)
        {
            CourseName = courseName;
            Description = description;
            Instructor = instructor;
        }

        public void AddMaterial(string material)
        {
            Materials.Add(material);
            Console.WriteLine($"Материал '{material}' добавлено к курсу {CourseName}.");
        }

        public void AddTest(Test test)
        {
            Tests.Add(test);
            Console.WriteLine($"Тест '{test.TestName}' добавлено к курсу {CourseName}.");
        }
    }

    public class Test
    {
        public string TestName { get; set; }
        public List<string> Questions { get; set; } = new List<string>();

        public Test(string testName)
        {
            TestName = testName;
        }

        public void AddQuestion(string question)
        {
            Questions.Add(question);
            Console.WriteLine($" Вопрос добавлен в тест{TestName}: {question}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание пользователей
            Student student = new Student("John Doe", "john.doe@example.com");
            Instructor instructor = new Instructor("Jane Smith", "jane.smith@example.com");
            Administrator admin = new Administrator("Admin", "admin@example.com");

            // Просмотр доступных действий для студентов, преподавателей и администраторов
            student.PerformActions();
            instructor.PerformActions();
            admin.PerformActions();

            // Создание курсов и добавление материалов
            Course course1 = new Course("C# Programming", "Learn the basics of C#.", instructor);
            course1.AddMaterial("C# Basics Video");
            course1.AddTest(new Test("C# Basic Test") { Questions = new List<string> { "What is C#?", "What is an object in C#?" } });

            // Пример работы с курсами и тестами
            Console.WriteLine($"Course: {course1.CourseName}");
            Console.WriteLine("Materials:");
            foreach (var material in course1.Materials)
            {
                Console.WriteLine($"- {material}");
            }

            Console.WriteLine("Tests:");
            foreach (var test in course1.Tests)
            {
                Console.WriteLine($"- {test.TestName}");
                foreach (var question in test.Questions)
                {
                    Console.WriteLine($"  Question: {question}");
                }

                Console.ReadKey();
            }
        }
    }

}
