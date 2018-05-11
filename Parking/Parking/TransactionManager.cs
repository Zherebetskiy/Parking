using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Parking
{
    public class TransactionManager
    {
        const int transactionLogTimeout = 10000;
        const string dateTimeFormatPattern = "HH:mm:ss";
        const string logLevel = "INFO";
        const string path = @"Transaction.log";
        static TimerCallback timerCallbackLog;
        static Timer timerLog;
        public static volatile float feePerMinute = 0;
        string _carId;
        public string CarId
        {
            get { return _carId; }
        }

        public TransactionManager(string carId)
        {
            _carId = carId;
        }

        static TransactionManager()
        {
            timerCallbackLog = new TimerCallback(Log);
            timerLog = new Timer(timerCallbackLog, new object(), 0, transactionLogTimeout);
        }

        public static void Log(object obj)
        {
            float tempFee = feePerMinute;
            feePerMinute = 0;

            string content = $"{ DateTime.Now.ToString(dateTimeFormatPattern)}\t{logLevel}\t income per minute {tempFee}$\n";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, content, Encoding.UTF8);
            }
            File.AppendAllText(path, content, Encoding.UTF8);
        }

        public static void GetTransactoinLog()
        {
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);
        }

        public void TransactionTimer()
        {
            var parking = Parking.Instance;
           
            TimerCallback tc = new TimerCallback(DoTransaction);
            Timer timer = new Timer(tc, parking, 0, parking.Timeout * 1000);
        }

        void DoTransaction(object obj)
        {
            Transaction transaction = new Transaction(DateTime.Now, _carId);
            transaction.DoTransaction();
        }
    }
}
