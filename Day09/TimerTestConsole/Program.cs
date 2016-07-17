using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventTimer;
using System.IO;

namespace TimerTestConsole
{
    class Program
    {
        public class Person1
        {
            public Person1(Timer timer)
            {
                Register(timer);
            }
            public Person1()
            {

            }
            private void Reaction(Object sender,TimerEventData ted)
            {
                Console.WriteLine($"I'm person 1");
                Console.WriteLine($"Data message:{ted?.Message}");
                Console.WriteLine($"Data extra info:{ted?.Info}");
                Console.WriteLine($"--------------------------");
            }
            public void Unregister(Timer timer)
            {
                if (ReferenceEquals(timer, null)) throw new ArgumentNullException();
                timer.Events -= Reaction;
            }
            public void Register(Timer timer)
            {
                if (ReferenceEquals(timer, null)) throw new ArgumentNullException();
                timer.Events += Reaction;
            }
        }
        public class Person2
        {
            public Person2(Timer timer)
            {
                Register(timer);
            }
            public Person2()
            {

            }
            private void Reaction(Object sender, TimerEventData ted)
            {
                Console.WriteLine($"I'm person 2");
                Console.WriteLine($"Data message:{ted?.Message}");
                Console.WriteLine($"Data extra info:{ted?.Info}");
                Console.WriteLine($"--------------------------");
            }
            public void Unregister(Timer timer)
            {
                timer.Events -= Reaction;
            }
            public void Register(Timer timer)
            {
                timer.Events += Reaction;
            }
        }
        static void Main(string[] args)
        {
            var t = new Timer(200);
            var t2 = new Timer(1000); t2.SetEventData(new TimerEventData("RARELY", "WoooW"));
            var p1 = new Person1(t);
            var p2 = new Person2(t);
            p1.Register(t2);
            p2.Register(t2);
            t.StartTimer();
            t2.StartTimer();
            Console.ReadKey();
            t.SetEventData(new TimerEventData("New DDOS!!", "info"));
            p1.Unregister(t);
            Console.ReadKey();
            t.StopTimer();
            Console.ReadKey();
        }
    }
}
