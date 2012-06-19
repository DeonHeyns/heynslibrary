using System;
using System.Collections.Generic;
using System.Linq;

namespace HeynsLibrary.IoC
{
    public class IoC
    {
        private readonly static IDictionary<Type, Type> types =
            new Dictionary<Type, Type>();

        public static void Register<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private static object Resolve(Type contract)
        {
            var implementaion = types[contract];

            var constructor = implementaion.GetConstructors()[0];
            var constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
                return Activator.CreateInstance(implementaion);
            var parameters = new List<object>(constructorParameters.Length);
            foreach (var parameterInfo in constructorParameters)
                parameters.Add(parameterInfo.ParameterType);
            return constructor.Invoke(constructorParameters.Select(parameterInfo => Resolve(parameterInfo.ParameterType)).ToArray());
        }
    }


    // Usage:

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        IoC.Register<ICalcService, ScientificCalculatorService>();
    //        IoC.Register<ICalculator, ScientificCalculator>();
    //        ICalculator calculator = IoC.Resolve<ICalculator>();
    //        Console.WriteLine(calculator.Add(1, 2));
    //        Console.Read();
    //    }
    //}
}