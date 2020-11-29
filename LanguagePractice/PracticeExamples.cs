using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ExampleDotnetCore.LanguagePractice
{
    class PracticeExamples
    {
        public ValueTuple<int, int, int> TupleProperty { get; private set; } = new ValueTuple<int, int, int>(0, 0, 0);

        private int _someValue;
        public int SomeStandardProperty
        {
            get
            {
                if (_someValue == 10)
                    return _someValue;
                return 3;
            }
            set
            {
                if (value == 4)
                    _someValue = 4;
            }
        }

        Dictionary<char, int> _frequency = new Dictionary<char, int>() { ['a'] = 0, ['b'] = 0 };


        [Test]
        public void CheckDictionaryMethods()
        {
            _frequency['a'] += 1;
            _frequency['c'] = 1;

            foreach (var entry in _frequency)
                Console.WriteLine(entry);
        }

        [Test]
        public void SomeTest()
        {
            TupleProperty = new ValueTuple<int, int, int>(0, 0, 0);
        }

        //================================================

        [Test]
        public void SwitchExamples()
        {
            object someVar = 0;

            switch (someVar)
            {
                case string s:
                    Console.WriteLine(s.Length);
                    break;
                case int integer when integer == 10:
                    Console.WriteLine("When int = 10");
                    break;
                case null:
                    break;
            }
        }
        // =============================================

        public void Deconstruct(out int someProperty, out Dictionary<char, int> dict)
        {
            someProperty = SomeStandardProperty;
            dict = _frequency;
        }

        [Test]
        public void DeconstructTest()
        {
            var example = new PracticeExamples();
            var (property, frequencies) = example;

            Console.WriteLine($"{property}, {frequencies}");
        }

        //======================================================

        public (int one, PracticeExamples two, string three) SomeTrickyMethod()
        {
            return (10, new PracticeExamples(), "bla bla");
        }


        [Test]
        public void TupleSupport()
        {
            var tuple = ("Shishka", new PracticeExamples());
            Console.WriteLine(tuple);

            var tupleWithNamedFields = (Name: "Shishka", Value: "22", NotValue: 3);
            Console.WriteLine(tupleWithNamedFields);

            (int one, PracticeExamples two, string three) = SomeTrickyMethod();
            Console.WriteLine($"{one} {two} {three}");
        }

        //=====================================================

        [Test]
        public void NullConditionalOperator()
        {
            string str = null;
            Console.WriteLine(str?[0]);
        }


        //=======================================================

        [Test]
        public void CatchException()
        {
            try
            {
                // throw new WebException(status: WebExceptionStatus.Timeout);
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
            {

            }

        }

        // =================================================================

        [Test]
        public void IntValues()
        {
            int value = int.MinValue;
            Assert.Throws<OverflowException>(() => { _ = checked(--value); });
        }

        // =================================================================

        [Test]
        public void ArrayMethods()
        {
            var array = new int[] { 3, 4, 5 };
            var list = new List<int> { 4, 5, 8 };
        }

        // ===========================

        [Test]
        public void NullCoalescing()
        {
            string str = null;
            // if null => return default value, otherwise - current value
            var anotherStr = str ?? "somestring";
        }

        // =========================================================

        private abstract class AbstractSuperClass
        {
            public abstract void MethodWithoutBody();
            public void ImplementedMethod()
            {
                var x = 0;
                x++;
            }
        }

        private class Subclass : AbstractSuperClass
        {
            public override void MethodWithoutBody()
            {
                ImplementedMethod();
            }
        }

        //===========================================================

        delegate int SomeParameterlessDelegate();
        delegate void SomeDelegate(int x);

        void TestDelegateMethod(int y)
        {
            y++;
            Console.WriteLine(y);
        }

        [Test]
        public void DelegateTest()
        {
            SomeDelegate method = new SomeDelegate(TestDelegateMethod);
            method.Invoke(3);
        }

        //============================================================

        delegate (string str, bool flag) DelegateWithMulticast(string input);

        (string output, bool flag) Method1(string input)
        {
            Console.WriteLine(nameof(Method1)); return ("Method1", true);
        }
        (string output, bool flag) Method2(string input)
        {
            Console.WriteLine(nameof(Method2)); return ("Method2", false);
        }

        [Test]
        public void CheckDelegateMulticasting()
        {
            DelegateWithMulticast multicastDelegate;
            multicastDelegate = Method1;
            multicastDelegate += Method2;
            (string output, bool flag) = multicastDelegate.Invoke("");
            Console.WriteLine($"{output}, {flag}");
        }

        //=======================================================================
        // Func and Actions - examples of delegates in System namespace

        void FuncMethod(string str)
        {
            str += str;
        }

        [Test]
        public void CheckFuncDelegate()
        {
            Action<string> action = FuncMethod;
            action("d");
        }

        // ============================================
        // Event Example

        public delegate void EventHandlerExample(int param);
        event EventHandlerExample someEvent;

        public void SubscriberForEvent(int x) { Console.WriteLine(x); }

        [Test]
        public void TestEvent()
        {
            someEvent += SubscriberForEvent;
            someEvent.Invoke(1);
        }

        public event EventHandler Event;
        public void Subscriber(object? sender, EventArgs args) { Console.WriteLine(sender); }

        [Test]
        public void AnotherEventTest()
        {
            Event += Subscriber;
            Event.Invoke(this, null);
        }

        // Anonymous class =================================
        [Test]
        public void CheckAnonymousClass()
        {
            var someObj = new { Field = "bla" , AnotherField = Event};
        }
    }

}
