using System;
using System.Collections;

namespace task_1
{
    class FactoryWorker : IEnumerable, IEnumerator
    {
        Worker[] workers;

        public FactoryWorker()
        {
            workers = new Worker[4];
            workers[0] = new HourlyRateWorker("Софья", "Ковалевская", 70);
            workers[1] = new HourlyRateWorker("Юзеф", "Вронский", 100);
            workers[2] = new FixedRateWorker("Ада", "Лавлейс", 3000);
            workers[3] = new FixedRateWorker("Моше", "Бар", 1000);
        }

        private int _i = -1;

        public object Current => workers[_i];

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_i == workers.Length - 1)
            {
                Reset();
                return false;
            }
            _i++;
            return true;           
        }

        public void Reset()
        {
            _i = -1;
        }       

    }   

}
