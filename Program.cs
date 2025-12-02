using System;
using System.Text;

namespace Decorator.Examples
{
    class MainApp
    {
        static void Main()
        {
            // 1. Створюємо Базовий Компонент (просту ялинку)
            SimpleChristmasTree simpleTree = new SimpleChristmasTree();
            
            // 2. Створюємо Декоратори
            OrnamentDecorator ornaments = new OrnamentDecorator();
            LightsDecorator lights = new LightsDecorator();

            // 3. Зв'язуємо Декоратори (обгортаємо)
            // Прикрашаємо ялинку кулями
            ornaments.SetComponent(simpleTree); 
            
            // Додаємо гірлянди до вже прикрашеної ялинки
            lights.SetComponent(ornaments); 

            Console.WriteLine("--- Ялинка, повністю декорована ---");
            // Викликаємо операції через останній декоратор у ланцюгу
            lights.GetDescription();
            lights.LightUp();

            Console.WriteLine("\n--- Тільки з прикрасами ---");
            // Можна викликати операції через проміжний декоратор
            ornaments.GetDescription();
            ornaments.LightUp(); // Метод LightUp() тут не декорований, поверне базовий стан.
            
            Console.Read();
        }
    }

    // 1. "Component" (IChristmasTree)
    // Визначає інтерфейс для об'єктів, які можуть бути декоровані.
    abstract class IChristmasTree
    {
        public abstract void GetDescription();
        public abstract void LightUp(); // Новий метод для свічення
    }

    // 2. "ConcreteComponent" (SimpleChristmasTree)
    class SimpleChristmasTree : IChristmasTree
    {
        public override void GetDescription()
        {
            Console.WriteLine("Базова жива Ялинка.");
        }
        
        public override void LightUp()
        {
            Console.WriteLine("Ялинка не світиться (немає гірлянд).");
        }
    }
    
    // 3. "Decorator" (TreeDecorator)
    abstract class TreeDecorator : IChristmasTree
    {
        protected IChristmasTree component;

        public void SetComponent(IChristmasTree component)
        {
            this.component = component;
        }
        
        // Базова поведінка: делегувати виклик обгорнутому об'єкту
        public override void GetDescription()
        {
            if (component != null)
            {
                component.GetDescription();
            }
        }
        
        // Базова поведінка: делегувати виклик обгорнутому об'єкту
        public override void LightUp()
        {
            if (component != null)
            {
                component.LightUp();
            }
        }
    }

    // 4. "ConcreteDecoratorA" (OrnamentDecorator)
    // Декоратор, що додає "стан" (опис прикрас)
    class OrnamentDecorator : TreeDecorator
    {
        // Поле/стан, що імітує додавання прикрас
        private string addedOrnaments = " (Прикрашена **золотими кулями** та **бусами**)";

        public override void GetDescription()
        {
            // Викликаємо метод обгорнутого компонента
            base.GetDescription();
            // Додаємо власну функціональність (прикраси)
            Console.Write(addedOrnaments); 
        }
        
        // Не декоруємо LightUp, делегуємо його далі
        public override void LightUp()
        {
            base.LightUp();
        }
    }

    // 5. "ConcreteDecoratorB" (LightsDecorator)
    // Декоратор, що додає "поведінку" (світіння)
    class LightsDecorator : TreeDecorator
    {
        public override void GetDescription()
        {
            // Викликаємо метод обгорнутого компонента (дозволяючи відобразити прикраси)
            base.GetDescription();
            Console.Write(" (З **електричною гірляндою**)");
        }
        
        public override void LightUp()
        {
            // Ми перевантажуємо поведінку LightUp()
            Console.WriteLine("Ялинка **яскраво світиться** різнокольоровими вогниками!");
        }
    }
}
